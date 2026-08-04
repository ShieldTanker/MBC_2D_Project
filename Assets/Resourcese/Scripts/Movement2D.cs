using UnityEngine;
public class Movement2D : MonoBehaviour
{
    private Vector2 _moveInput = Vector2.zero;
    private Rigidbody2D _rb;
    [SerializeField] private float currentSpeed;
    public float speed = 20f;      // 움직이는 속력
    public float accelation = 5f;  // 가속력
    public float _break = 10f;

    private bool _canMove = true;
    private bool _isMoving = false;
    [SerializeField] private bool _isGrounded = false;
    [SerializeField] private LayerMask grdLayer;

    public bool CanMove { get { return _canMove; } }
    public float MoveSpeed { get { return speed; } set { speed = value; } }
    public float Acceleration { get { return accelation; } set { accelation = value; } }
    public float Gravity { get; set; } = 9.8f;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        GroundDetect();
        Move();
        currentSpeed = _rb.linearVelocity.magnitude;
    }

    private void Move()
    {
        if (_rb == null) return;

        Vector2 dest = _isMoving ? _moveInput * speed : Vector2.zero;

        dest.y -= _isGrounded ? 0.2f : Gravity;
        _rb.linearVelocity = Vector2.Lerp(_rb.linearVelocity, dest, accelation * Time.deltaTime);
    }

    public void MoveInput(Vector2 input)
    {
        if (_canMove)
        {
            _moveInput.x = input.x;
            _isMoving = _moveInput.x != 0;
        }
    }

    void GroundDetect()
    {
        _isGrounded = Physics2D.Raycast(transform.position, Vector2.down, 0.2f, grdLayer);
    }
}