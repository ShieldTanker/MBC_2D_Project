using UnityEngine;
using UnityServiceLocator;

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
    public AgentStat _stat;
    private AgentFlag _flag = new AgentFlag();
    #endregion

    #region 움직임 관련
    private Movement2D _move;
    private Rotation2D _rotation;

    private InputController _input;
    private ModelController _model;
    #endregion

    #region 전투 관련
    private Health _health;

    private LockonController _lockonCon;
    public LockonController LockonCon {  get { return _lockonCon; } }
    #endregion

    #region 무기관련
    private WeaponController _weaponCon;
    public WeaponController WeaponController {  get { return _weaponCon; } }
    #endregion

    public AgentStateType CurrentState { get; private set; }

    private void Awake()
    {
        Init();
        SetContext();
    }

    private void OnEnable()
    {
        _weaponCon.F_HandAnchor.Weapon.OnFireStart += SetAmmo;
        _weaponCon.B_HandAnchor.Weapon.OnFireStart += SetAmmo;
        _weaponCon.F_ShoulderAnchor.Weapon.OnFireStart += SetAmmo;
        _weaponCon.B_ShoulderAnchor.Weapon.OnFireStart += SetAmmo;

        _health.OnDamageAction += SetHp;
        _input.BoostActionPerformed += OnBoostInput;
    }

    private void OnDisable()
    {
        _weaponCon.F_HandAnchor.Weapon.OnFireStart -= SetAmmo;
        _weaponCon.B_HandAnchor.Weapon.OnFireStart -= SetAmmo;
        _weaponCon.F_ShoulderAnchor.Weapon.OnFireStart -= SetAmmo;
        _weaponCon.B_ShoulderAnchor.Weapon.OnFireStart -= SetAmmo;

        _health.OnDamageAction -= SetHp;
        _input.BoostActionPerformed -= OnBoostInput;
    }

    void OnBoostInput()
    {
        _stat.IsBoost = true;
    }

    void SetAmmo()
    {
        BattleUIEventBus.Publish(BattleUIEventType.PlayerAmmoSet, this);
    }

    private void SetHp(DamageInfo _)
    {
        _stat.CurrentHp = _health.CurrentHealth;
        BattleUIEventBus.Publish(BattleUIEventType.PlayerHpSet, this);
        Debug.Log("OnDamaged");
    }

    void Start()
    {
        // TODO : _context 초기화할것
        _agent = new AgentStateMachine(_context);
        _agent.ChangeState(AgentStateType.Idle);

        // 장비 설정
        _bodyLoadout.Stat = _stat;
        _bodyLoadout.SetLoadoutData(LoadoutData);
        _weaponLoadout.SetLoadoutData(LoadoutData);

        _health.MaxHealth = _stat.MaxHp;
        _health.Init();

        // 락온 설정
        _lockonCon.Init(_stat, new DistanceTargetSelector());

        // 무기 설정
        _weaponCon.SetLockonController(_lockonCon);

        BattleUIEventBus.Publish(BattleUIEventType.PlayerHpSet, this);
        SetAmmo();
    }

    void Update()
    {
        _agent?.Update(Time.deltaTime);
        UpdateFlag();
        CurrentState = _agent.CurrentStateType;

        _rotation.dir = _lockonCon.PredictedPosition.x > transform.position.x ? CharDirection.Right : CharDirection.Left;
        _stat.CurrentDirection = _rotation.dir;
    }

    void Init()
    {
        // 캐릭터 관련
        _stat = GetComponent<AgentStat>();
        _input = GetComponent<InputController>();
        _move = GetComponent<Movement2D>();
        _rotation = GetComponent<Rotation2D>();
        _health = GetComponent<Health>();
        _model = GetComponentInChildren<ModelController>();

        // 락온
        _lockonCon = GetComponentInChildren<LockonController>();

        // 장비 관련
        _bodyLoadout = GetComponent<BodyLoadout>();
        _weaponLoadout = GetComponent<WeaponLoadout>();
        _weaponCon = GetComponent<WeaponController>();

        ServiceLocator sl = ServiceLocator.ForSceneOfLocal(this);
        sl.Register<Player>(this);
    }

    void SetContext()
    {
        _context = new AgentContext();
        _context.Player = this;
        _context.AgentFlag = _flag;

        _context.AgentStat = _stat;
        _context.InputCon = _input;
        _context.Move = _move;
        _context.MoveInput = _input;
        _context.JumpInput = _input;
        _context.ModelCon = _model;
        _context.WeaponController = _weaponCon;
    }

    void UpdateFlag()
    {
        _flag.OnGround = _move.IsGround;
        _model.Anim.SetBool("OnGround", _flag.OnGround);
    }
}
