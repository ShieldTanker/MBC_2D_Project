using System.Collections;
using UnityEngine;

public abstract class WeaponBase : MonoBehaviour
{
    private WeaponStateMachine _machine;
    public LockOnController LockonController;
    // Context관련
    public WeaponContext Context { get; private set; } = new WeaponContext();
    private WeaponInput _input = new WeaponInput();
    private WeaponFlag _flag = new WeaponFlag();

    #region 모델 관련
    private Vector3 _weaponLocalOffset = new Vector3(0.6f, 0, 0);

    private WeaponModel _model;
    #endregion

    #region 사격 관련
    public Transform FirePosition { get; set; }
    private Vector3 _baseLocalPos;
    private Quaternion _baseLocalRot;

    private Vector3 _recoilVelocity = Vector3.zero;
    #endregion

    #region 무기 데이터
    private WeaponData _weaponData;

    public WeaponData WeaponData
    {
        get { return _weaponData; }
        set
        {
            _weaponData = value;
            SetWeapon();
        }
    }

    public WeaponData _baseData;
    #endregion

    #region Context Debug

    [Header("Context Debug")]

    [Tooltip("입력")]
    public bool AttackPressed;
    public bool AttackHeld;
    public bool AttackReleased;
    public bool InteractionPressed;
    
    [Space]
    [Tooltip("플래그")]
    public bool CanFire = true;
    public bool CanReload;
    public bool IsAimComplete;
    public bool IsAiming;
    public bool IsFireComplete;
    public bool IsRecoilComplete;
    public bool IsReloadComplete;
    
    [Space]
    [Tooltip("탄약")]
    public int CurrentCapacity;
    public int AmmoRemaining;
    public float FireRate = 0f;

    [Space]
    [Tooltip("공격 입력 이벤트")]
    public bool AttackSequenceStarted;

    [Space]
    [Tooltip("강제행동")]
    public bool IsInterrupted;

    public float AimIdleTimer;

    #endregion

    private void Awake()
    {
        _baseLocalPos = transform.localPosition;
        _baseLocalRot = transform.localRotation;

        SetContext();

        _machine = new WeaponStateMachine(Context);
    }

    private void Start()
    {
        SetWeapon();
        InitAmmo();

        _machine.ChangeState(WeaponStateType.Idle);
    }

    private void Update()
    {
        _machine.Update(Time.deltaTime);

        // 연사력 제한
        if (Context.FireRate <= 1f / Context.WeaponData?.FireRatePerSecond)
        {
            Context.FireRate += Time.deltaTime;
        }
        RecoilExample();
        DebugContext();
    }

    #region 무기 입력
    /// <summary>
    /// 공격 버튼을 눌렀을 때 호출
    /// </summary>
    public void PerformedFire()
    {
        if (_weaponData == null)
            return;

        _input.AttackPressed = true;
        _input.AttackHold = true;
        _flag.AttackSequenceStarted = true;
    }

    /// <summary>
    /// 공격 버튼을 뗐을 때 호출
    /// </summary>
    public void CanceledFire()
    {
        if (_weaponData == null)
            return;

        _input.AttackPressed = false;
        _input.AttackHold = false;
    }
    #endregion

    #region 무기 조작
    // 무기의 방향을 목표로 회전
    //private void WeaponAim()
    //{
    //    if (_context.WeaponFlag.IsAiming)
    //    {
    //        Vector3 dir = AimTarget.position - WeaponAnchor.transform.position;

    //        angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
    //        angle = Mathf.LerpAngle(WeaponAnchor.transform.eulerAngles.z, angle, AimSpeed * Time.deltaTime);

    //        WeaponAnchor.transform.rotation = Quaternion.Euler(0, 0, angle);
    //    }
    //}

    private void RecoilExample()
    {
        transform.localPosition =
            Vector3.SmoothDamp(transform.localPosition, _baseLocalPos,
            ref _recoilVelocity, 0.2f);
    }
    #endregion

    #region 초기화
    private void SetWeapon()
    {
        // 데이터가 없으면 기본 데이터로
        if (_weaponData == null) _weaponData = _baseData;

        // 모델이 있으면 파괴
        if (_model != null) Destroy(_model);

        if (_weaponData != null && _weaponData.Model != null)
        {
            _model = Instantiate(_weaponData.Model, transform);
            _model.transform.localPosition = _weaponLocalOffset;
            _model.transform.rotation = transform.rotation;
        }

        FirePosition = _model ? _model.FirePosition : transform;

        SetContext();
        InitAmmo();
    }

    private void SetContext()
    {
        Context.WeaponPos = transform;

        Context.WeaponInput = _input;
        Context.WeaponFlag = _flag;

        Context.WeaponData = _weaponData == null ? _baseData : _weaponData;
        Context.FirePosition = FirePosition == null ? transform : FirePosition;
    }

    private void InitAmmo()
    {
        if (Context.WeaponData == null)
        {
            Context.CurrentCapacity = 0;
            Context.AmmoRemaining = 0;
            return;
        }

        Context.CurrentCapacity = Context.WeaponData.MaxCapacity;
        Context.AmmoRemaining = Context.WeaponData.MaxAmmo;
        Context.FireRate = 1f / Context.WeaponData.FireRatePerSecond;
    }

    #endregion

    // 디버깅
    private void DebugContext()
    {
        // 입력
        AttackPressed = Context.WeaponInput.AttackPressed;
        AttackHeld = Context.WeaponInput.AttackHold;
        InteractionPressed = Context.WeaponInput.InteractionPressed;

        // 플래그
        AttackSequenceStarted = Context.WeaponFlag.AttackSequenceStarted;
        CanFire = Context.WeaponFlag.CanFire;
        CanReload = Context.WeaponFlag.CanReload;

        IsInterrupted = Context.IsInterrupted;
        IsAiming = Context.WeaponFlag.IsAiming;
        IsAimComplete = Context.WeaponFlag.IsAimComplete;
        IsRecoilComplete = Context.WeaponFlag.IsRecoilComplete;
        IsReloadComplete = Context.WeaponFlag.IsReloadComplete;

        // 탄약
        CurrentCapacity = Context.CurrentCapacity;
        AmmoRemaining = Context.AmmoRemaining;
        FireRate = Context.FireRate;
    }
}
