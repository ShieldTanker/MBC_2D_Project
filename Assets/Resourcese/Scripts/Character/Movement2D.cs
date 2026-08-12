using UnityEngine;
public class Movement2D : MonoBehaviour
{
    private Rigidbody2D _rb;
    private Vector2 _moveInput = Vector2.zero;
    [SerializeField] private float currentSpeed; // 속도 표시용

    [SerializeField] private float _speed = 20f;          // 움직이는 속력
    [SerializeField] private float _acceleration = 5f;    // 가속력
    [SerializeField] private float _deceleration = 10f;

    [SerializeField] private bool _isGrounded = false;
    [SerializeField] private float _grdLength = 0.2f;    // 지면 감지 거리
    [SerializeField] private bool _canMove = true;       // 이동 가능 여부
    private Vector2 _grdNomal = Vector2.up;
    
    public LayerMask _grdLayer;

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
    }

    private void FixedUpdate()
    {
        GroundDetect();
        Move();
        currentSpeed = _rb.linearVelocity.magnitude;
    }

    private void Move()
    {
        if (_rb == null) return;
        float accel = _moveInput == Vector2.zero ? _deceleration : _acceleration;
        Vector2 dest;
        if (_isGrounded)
            dest = Vector3.ProjectOnPlane(_moveInput, _grdNomal).normalized * _speed;
        else
            dest = new (_moveInput.x * _speed, _rb.linearVelocity.y);

        _rb.linearVelocity = Vector2.Lerp(_rb.linearVelocity, dest, accel * Time.deltaTime);
    }

    public void Jump(float force)
    {
        _rb.AddForce(Vector3.up * force, ForceMode2D.Impulse);
    }

    public void MoveInput(Vector2 input)
    {
        if (_canMove)
            _moveInput.x = input.x;
    }

    void GroundDetect()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down, _grdLength, _grdLayer);
        if(hit.collider != null)
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