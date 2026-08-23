using UnityEngine;
public class Movement2D : MonoBehaviour
{
    private Rigidbody2D _rb;
    private Vector2 _moveInput = Vector2.zero;
    [SerializeField] private float currentSpeed; // 속도 표시용
    [SerializeField] public float moveX; // 속도 표시용

    [SerializeField] private float _speed = 116f;          // 움직이는 속력
    [SerializeField] private float _acceleration = 13.5f; // 가속력 (지수 감쇠 k값, 1/초)
    [SerializeField] private float _deceleration = 54f;

    [SerializeField] private bool _isGrounded = false;
    [SerializeField] private float _grdLength = 0.2f;    // 지면 감지 거리
    [SerializeField] private bool _canMove = true;       // 이동 가능 여부
    private Vector2 _grdNomal = Vector2.up;
    private bool _suppressGroundNextFixedUpdate = false;

    public LayerMask _grdLayer;

    [Header("중력 / 공중 물리")]
    [Tooltip("기본 중력가속도 (일반/부스트 정지낙하 공통 - 높이531 낙하 약3.2초 기준 역산값)")]
    [SerializeField] private float _gravity = 103.7f;
    [Tooltip("부스트 상태로 이동하며 하강할 때만 적용되는 낙하 종단속도 캡 (높이531, 약5.3초 기준 역산값)")]
    [SerializeField] private float _boostGlideFallSpeed = 111.5f;
    [Tooltip("부스트로 이동 중 경사면을 벗어나는 순간 스냅되는 발사 수직속도 (경사각30도 기준 역산값 - 슬로프 추적 속도를 그대로 이어받지 않음)")]
    [SerializeField] private float _rampLaunchVerticalSpeed = 64.4f;

    public bool IsBoosting { get; set; }

    #region 속성
    public bool CanMove { get { return _canMove; } }
    public bool IsGround { get { return _isGrounded; } }
    public float MoveSpeed { get { return _speed; } set { _speed = value; } }
    public float Acceleration { get { return _acceleration; } set { _acceleration = value; } }
    public float Deceleration { get { return _deceleration; } set { _deceleration = value; } }
    #endregion

    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
        _rb.gravityScale = 0f; // 중력은 ApplyGravity()에서 직접 적분 (엔진 기본 중력과 이중 적용 방지)
    }

    private void FixedUpdate()
    {
        bool wasGrounded = _isGrounded;
        Vector2 prevGrdNomal = _grdNomal;

        GroundDetect();

        bool boostFlying = IsBoosting && _moveInput.sqrMagnitude >= 0.01f;

        if (wasGrounded && !_isGrounded && boostFlying && IsSlope(prevGrdNomal))
            LaunchFromRamp();

        ApplyGravity(boostFlying);
        Move();

        currentSpeed = _rb.linearVelocity.magnitude;
        moveX = _moveInput.x;
    }

    private void Move()
    {
        if (_rb == null)
            return;

        Vector2 dest;
        float accel = _moveInput.sqrMagnitude >= 0.01f ? _acceleration : _deceleration;

        if (_isGrounded)
        {
            // 수평속도는 그대로 유지하고, 경사에 tan(각도)만큼 수직속도를 더한다
            // (project-onto-plane 후 normalize하면 경사에서 오히려 속력이 고정/감소해버려서 실측치인
            // "경사에서 수평 유지 + 속력 증가" 거동과 안 맞음)
            float horizontalVel = _moveInput.x * _speed;
            float slopeTan = _grdNomal.y > 0.0001f ? -_grdNomal.x / _grdNomal.y : 0f;
            dest = new Vector2(horizontalVel, horizontalVel * slopeTan);
        }
        else
            dest = new(_moveInput.x * _speed, _rb.linearVelocity.y);

        // 프레임 독립적인 지수 감쇠: t = 1 - e^(-accel * dt) (accel*dt가 1을 넘어가도 안정적으로 수렴)
        float t = 1f - Mathf.Exp(-accel * Time.fixedDeltaTime);
        _rb.linearVelocity = Vector2.Lerp(_rb.linearVelocity, dest, t);
    }

    void ApplyGravity(bool boostFlying)
    {
        if (_isGrounded)
            return; // 지면에서는 Move()의 슬로프 추적이 수직속도를 담당

        Vector2 v = _rb.linearVelocity;
        v.y -= _gravity * Time.fixedDeltaTime;

        // 부스트로 이동하며 하강할 때만 낙하 종단속도를 캡 (정지 낙하/일반모드는 캡 없이 그대로 가속)
        if (boostFlying && v.y < -_boostGlideFallSpeed)
            v.y = -_boostGlideFallSpeed;

        _rb.linearVelocity = v;
    }

    void LaunchFromRamp()
    {
        _rb.linearVelocity = new Vector2(_rb.linearVelocity.x, _rampLaunchVerticalSpeed);
    }

    static bool IsSlope(Vector2 normal) => Vector2.Angle(Vector2.up, normal) > 0.1f;

    /// <summary>
    /// 목표 높이(height)를 정자세(수직) 기준 발사속도 v0=√(2·g·h)로 역산하고,
    /// horizontalDir(-1~1)만큼 그 방향으로 벡터를 기울인다 - 벡터 크기(=점프력)는 그대로 유지한 채 방향만 바뀌므로
    /// 대각선으로 갈수록 수직성분이 줄어들어 실제 도달 높이는 낮아진다 (에너지 보존처럼 동작).
    /// </summary>
    public void Jump(float height)
    {
        float launchSpeed = Mathf.Sqrt(2f * _gravity * height);
        float clampedX = Mathf.Clamp(_moveInput.x, -0.7f, 0.7f);

        Vector2 dir = new Vector2(clampedX, 1f).normalized;
        _rb.linearVelocity = dir * launchSpeed;

        _suppressGroundNextFixedUpdate = true;
    }

    /// <summary>
    /// height/horizontalDir로 Jump()를 쐈을 때 실제 정점까지 걸리는 시간을 계산해서 반환한다.
    /// 대각선일수록 수직성분이 줄어드는 만큼 시간도 Jump()와 같은 비율(dir.y)로 짧아진다.
    /// </summary>
    public float CalculateJumpDuration(float height)
    {
        float clampedX = Mathf.Clamp(_moveInput.x, -0.7f, 0.7f);
        float dirY = new Vector2(clampedX, 1f).normalized.y;
        return dirY * Mathf.Sqrt(2f * height / _gravity);
    }

    public void MoveInput(Vector2 input)
    {
        if (_canMove)
            _moveInput.x = input.x;
    }

    void GroundDetect()
    {
        if (_suppressGroundNextFixedUpdate)
        {
            _suppressGroundNextFixedUpdate = false;
            _isGrounded = false;
            _grdNomal = Vector2.up;
            return;
        }

        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down, _grdLength, _grdLayer);
        if (hit.collider != null)
        {
            _isGrounded = true;
            _grdNomal = hit.normal;
        }
        else
        {
            _isGrounded = false;
            _grdNomal = Vector2.up;
        }
    }
}