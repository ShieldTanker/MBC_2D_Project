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
    private Rotation2D _rotation;
    private InputController _input;
    private AnimController _animCon;
    private ModelController _model;
    #endregion

    #region 무기관련
    private WeaponController _weaponCon;
    #endregion

    public AgentStateType CurrentState { get; private set; }

    private void Awake()
    {
        Init();
        SetContext();
    }

    private void OnEnable()
    {
        _weaponCon.F_HandAnchor.Weapon.OnReloadStart += FHandOnReloadStart;
        _weaponCon.B_HandAnchor.Weapon.OnReloadStart += BHandOnReloadStart;
        _weaponCon.F_HandAnchor.Weapon.OnReloadExit += FHandOnReloadExit;
        _weaponCon.B_HandAnchor.Weapon.OnReloadExit += BHandOnReloadExit;
        
        _weaponCon.F_ShoulderAnchor.Weapon.OnReloadStart += FShoulderOnReloadStart;
        _weaponCon.B_ShoulderAnchor.Weapon.OnReloadStart += BShoulderOnReloadStart;
        _weaponCon.F_ShoulderAnchor.Weapon.OnReloadExit += FShoulderOnReloadExit;
        _weaponCon.B_ShoulderAnchor.Weapon.OnReloadExit += BShoulderOnReloadExit;
    }

    private void OnDisable()
    {
        _weaponCon.F_HandAnchor.Weapon.OnReloadStart -= FHandOnReloadExit;
        _weaponCon.B_HandAnchor.Weapon.OnReloadStart -= BHandOnReloadExit;
        _weaponCon.F_HandAnchor.Weapon.OnReloadExit -= FHandOnReloadExit;
        _weaponCon.B_HandAnchor.Weapon.OnReloadExit -= BHandOnReloadExit;

        _weaponCon.F_ShoulderAnchor.Weapon.OnReloadStart -= FShoulderOnReloadStart;
        _weaponCon.B_ShoulderAnchor.Weapon.OnReloadStart -= BShoulderOnReloadStart;
        _weaponCon.F_ShoulderAnchor.Weapon.OnReloadExit -= FShoulderOnReloadExit;
        _weaponCon.B_ShoulderAnchor.Weapon.OnReloadExit -= BShoulderOnReloadExit;
    }

    #region OnReloadStart
    void BHandOnReloadStart()
    {
        _model.Anim.SetLayerWeight(1, 1);
        _model.Anim.Play("Reload", 1);
    }

    void FHandOnReloadStart()
    {
        _model.Anim.SetLayerWeight(2, 1);
        _model.Anim.Play("Reload", 2);
    }

    void BShoulderOnReloadStart()
    {
        _model.Anim.SetLayerWeight(3, 1);
        _model.Anim.Play("Reload", 4);
    }

    void FShoulderOnReloadStart()
    {
        _model.Anim.SetLayerWeight(4, 1);
        _model.Anim.Play("Reload", 3);
    }

    #endregion

    #region OnReloadExit
    void BHandOnReloadExit()
    {
        _model.Anim.SetLayerWeight(1, 0);
    }

    void FHandOnReloadExit()
    {
        _model.Anim.SetLayerWeight(2, 0);
    }

    void BShoulderOnReloadExit()
    {
        _model.Anim.SetLayerWeight(3, 0);
    }

    void FShoulderOnReloadExit()
    {
        _model.Anim.SetLayerWeight(4, 0);
    }
    #endregion

    void Start()
    {
        // TODO : _context 초기화할것
        _agent = new AgentStateMachine(_context);
        _agent.ChangeState(AgentStateType.Idle);

        // 장비 설정
        _bodyLoadout.SetLoadoutData(LoadoutData);
        _weaponLoadout.SetLoadoutData(LoadoutData);

        _model.Anim.SetLayerWeight(1, 1);
        _model.Anim.Play("None", 1);

        _model.Anim.SetLayerWeight(2, 1);
        _model.Anim.Play("None", 2);
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
        _rotation = GetComponent<Rotation2D>();
        _animCon = GetComponent<AnimController>();

        _bodyLoadout = GetComponent<BodyLoadout>();
        _weaponLoadout = GetComponent<WeaponLoadout>();

        _weaponCon = GetComponent<WeaponController>();
        _model = GetComponentInChildren<ModelController>();
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
        _animCon.model.Anim.SetBool("OnGround", _flag.OnGround);
    }
}
