using UnityEngine;

public class Player : MonoBehaviour
{
    private AgentStateMachine _agent;
    private AgentContext _context;

    private Movement2D _move;
    private InputController _input;
    private AnimController _animCon;
    public AgentStateType currentState;

    private void Awake()
    {
        Init();
        SetContext();
    }

    void Start()
    {
        // TODO : _context 초기화할것
        _agent = new AgentStateMachine(_context);
        _agent.ChangeState(AgentStateType.Idle);
    }

    void Update()
    {
        _agent?.Update(Time.deltaTime);
        currentState= _agent.CurrentStateType;
    }

    void Init()
    {
        _input = GetComponent<InputController>();
        _move = GetComponent<Movement2D>();
        _animCon = GetComponent<AnimController>();
    }

    void SetContext()
    {
        _context = new AgentContext();
        _context.InputCon = _input;
        _context.Move = _move;
        _context.MoveInput = _input;
        _context.JumpInput = _input;
        _context.AnimCon = _animCon;
    }
}
