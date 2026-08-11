using UnityEngine;

public class Player : MonoBehaviour
{
    private AgentStateMachine _agent;
    private AgentContext _context;

    private Movement2D _move;
    private InputController _input;
    private AnimController _animCon;
    public AgentStateType CurrentState { get; private set; }

    private WeaponAimController _weaponAimCon;
    private ModelController _model;
    private LoadOut _loadOut;

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
        _input = GetComponent<InputController>();
        _move = GetComponent<Movement2D>();
        _animCon = GetComponent<AnimController>();
        _model = GetComponentInChildren<ModelController>();
        _weaponAimCon = GetComponentInChildren<WeaponAimController>();

        // Todo : 나중에 분리할것
        _weaponAimCon.SetAnchorPosition(_model.F_Shoudler, WeaponAimType.F_Hand);
        _weaponAimCon.SetAnchorPosition(_model.B_Shoudler, WeaponAimType.B_Hand);
        _weaponAimCon.SetAnchorPosition(_model.BackWeaponPos, WeaponAimType.F_Shoulder);
        _weaponAimCon.SetAnchorPosition(_model.BackWeaponPos, WeaponAimType.B_Shoulder);

        // Todo : IKTarget 설정
        _weaponAimCon.SetEffectTarget(_model.F_HandIKTarget, WeaponAimType.F_Hand);
        _weaponAimCon.SetEffectTarget(_model.B_HandIKTarget, WeaponAimType.B_Hand);
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
