using UnityEngine;

public class Player : MonoBehaviour
{
    // 상태 및 수치 관련
    private AgentStateMachine _agent;
    private AgentContext _context;
    private AgentStat _stat;

    // 움직임 관련
    private Movement2D _move;
    private InputController _input;
    private AnimController _animCon;

    private WeaponAimController _weaponAimCon;
    private ModelController _model;

    private LoadOut _loadOut;

    public AgentStateType CurrentState { get; private set; }

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
        CurrentState = _agent.CurrentStateType;
    }

    void Init()
    {
        _stat = GetComponent<AgentStat>();
        _input = GetComponent<InputController>();
        _move = GetComponent<Movement2D>();
        _animCon = GetComponent<AnimController>();
        // _model = GetComponentInChildren<ModelController>();
        // _weaponAimCon = GetComponentInChildren<WeaponAimController>();
    }

    void SetContext()
    {
        _context = new AgentContext();

        _context.AgentStat = _stat;
        _context.InputCon = _input;
        _context.Move = _move;
        _context.MoveInput = _input;
        _context.JumpInput = _input;
        _context.AnimCon = _animCon;
    }
}
