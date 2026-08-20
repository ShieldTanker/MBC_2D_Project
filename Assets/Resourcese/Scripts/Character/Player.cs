using UnityEngine;

[DisallowMultipleComponent]
public class Player : MonoBehaviour
{
    #region 로드아웃
    public LoadoutData LoadoutData;
    private BodyLoadout _bodyLoadout;
    private WeaponLoadout _weaponLoadout;
    #endregion

    #region 상태머신 및 수치 관련
    private AgentStateMachine _agent;
    private AgentContext _context;
    private AgentStat _stat;
    private AgentFlag _flag = new AgentFlag();
    #endregion

    #region 움직임 관련
    private Movement2D _move;
    private InputController _input;
    private AnimController _animCon;
    private ModelController _model;
    #endregion

    #region 무기관련
    private WeaponAimController _weaponAimCon;
    #endregion

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

        // 장비 설정
        _bodyLoadout.SetLoadoutData(LoadoutData);
        _weaponLoadout.SetLoadoutData(LoadoutData);
    }

    void Update()
    {
        _agent?.Update(Time.deltaTime);
        UpdateFlag();
        CurrentState = _agent.CurrentStateType;
    }

    void Init()
    {
        _stat = GetComponent<AgentStat>();
        _input = GetComponent<InputController>();
        _move = GetComponent<Movement2D>();
        _animCon = GetComponent<AnimController>();

        _bodyLoadout = GetComponent<BodyLoadout>();
        _weaponLoadout = GetComponent<WeaponLoadout>();

        // _model = GetComponentInChildren<ModelController>();
        // _weaponAimCon = GetComponentInChildren<WeaponAimController>();
    }

    void SetContext()
    {
        _context = new AgentContext();
        _context.AgentFlag = _flag;

        _context.AgentStat = _stat;
        _context.InputCon = _input;
        _context.Move = _move;
        _context.MoveInput = _input;
        _context.JumpInput = _input;
        _context.AnimCon = _animCon;
    }

    void UpdateFlag()
    {
        _flag.OnGround = _move.IsGround;
    }
}
