using UnityEngine;

public class Movement2D : MonoBehaviour
{
    private Vector2 _moveInput = Vector2.zero;
    private Rigidbody2D _rb;

    [SerializeField] private float currentSpeed;
    private float speed = 20f;      // 움직이는 속력
    private float accelation = 20f;  // 가속력
    private bool _canMove = true;

    public bool CanMove { get { return _canMove; } }
    public float MoveSpeed { get { return speed; } set { speed = value ; } }
    public float Acceleration { get { return accelation; } set { accelation = value ; } }

    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        Move();
        currentSpeed = _rb.linearVelocity.magnitude;
    }

    private void Move()
    {
        if (_rb == null) return;

        if (_canMove)
        {
            Vector2 dest = _moveInput * speed;
            // _rb.linearVelocity = Vector2.Lerp(_rb.linearVelocity, dest, accelation * Time.deltaTime);
            _rb.linearVelocity = Vector2.MoveTowards(_rb.linearVelocity, dest, accelation * Time.deltaTime);
        }
    }

    public void MoveInput(Vector2 input)
    {
        if(input.sqrMagnitude > 0)
            _moveInput = input;
        else
            _moveInput = Vector2.zero;
    }
}
