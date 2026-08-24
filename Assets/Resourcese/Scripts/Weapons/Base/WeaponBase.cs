using System;
using System.Collections;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public abstract class WeaponBase : MonoBehaviour
{
    private WeaponStateMachine _machine;
    public LockonController LockonController { get; set; }

    #region Context 관련
    // Context관련
    public WeaponContext Context { get; private set; } = new WeaponContext();
    private WeaponInput _input = new WeaponInput();
    private WeaponFlag _flag = new WeaponFlag();
    #endregion

    #region 모델 관련
    private Vector3 _weaponLocalOffset = new Vector3(0.6f, 0, 0);

    private WeaponModel _model;
    public bool UseRecoil;
    #endregion

    #region 사격 관련
    private Transform _firePosition;
    private Vector3 _baseLocalPos;
    private Quaternion _baseLocalRot;

    private Vector3 _recoilVelocity = Vector3.zero;

    private float _maxFireRate = 0f;
    private float _fireRate = 0f;
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

    #region 이벤트
    public Action OnWeaponChanged;

    public Action OnIdleStart;
    public Action OnIdleExit;

    public Action OnAimEnter;
    public Action OnAimExit;

    public Action OnFireStart;
    public Action OnFireExit;

    public Action OnReloadStart;
    public Action OnReloadExit;
    #endregion

    AudioSource _audioSource;

    #region Context Debug

    [Header("Context Debug")]

    [Tooltip("입력")]
    public bool AttackPressed;
    public bool AttackHeld;
    public bool AttackReleased;
    public bool InteractionPressed;

    [Space]
    [Tooltip("플래그")]
    public bool IsAlive = true;
    public bool CanFire = true;
    public bool CanReload;
    public bool IsAimComplete;
    public bool IsAiming;
    public bool IsFireComplete;
    public bool IsRecoilComplete;
    public bool IsReloadComplete;

    [Space]
    [Tooltip("탄약")]
    public int CurrentCapacity = 0;
    public int AmmoRemaining = 0;
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
        _audioSource = GetComponent<AudioSource>();
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
        if (!Context.WeaponFlag.IsAlive)
            return;

        _machine.Update(Time.deltaTime);

        if (_fireRate <= _maxFireRate)
        {
            _fireRate += Time.deltaTime;
        }

        RecoverRecoil();
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
    public void Fire()
    {
        if (!_flag.CanFire || _fireRate < _maxFireRate) return;

        if (Context.CurrentCapacity < 0 || _weaponData.BulletModel == null || _model.FireTransform == null)
        {
            Debug.Log("총알 부족 혹은 BulletModel 혹은 FirePosition 이 null");
            return;
        }

        GameObject go = GameObject.Instantiate(_weaponData.BulletModel, _model.FireTransform.position, _model.FireTransform.rotation);
        BulletTest bullet = go.GetComponent<BulletTest>();
        if (bullet == null) bullet = go.AddComponent<BulletTest>();

        bullet.SetData(WeaponData);

        bullet.SetTarget(LockonController?.TrackingTargetTransform);
        bullet.SetFollowTarget(LockonController?.CurrentTargetTransform);

        bullet.SetFireTrasnform(_model.FireTransform);

        OnFireStart?.Invoke();
        if (_audioSource != null)
        {
            _audioSource.clip = _weaponData?.GunFireAudioClip;
            _audioSource.Play();
        }

        _fireRate = 0f;
        Context.LastFireTime = 0f;

        switch (Context.WeaponData.FireMode)
        {
            case WeaponFireMode.SemiAuto:
                Context.WeaponFlag.AttackSequenceStarted = false;
                break;
            case WeaponFireMode.FullAuto:
                Context.WeaponFlag.AttackSequenceStarted = false;
                break;
            case WeaponFireMode.Burst:
                if (Context.BurstRemaining <= 0)
                {
                    Context.WeaponFlag.AttackSequenceStarted = false;
                }
                break;
        }


        Context.CurrentCapacity--;
        Context.BurstRemaining--;

        Recoil();
    }

    private void Recoil()
    {
        if (_weaponData.RecoilData == null)
            return;

        float kickBack = _weaponData.RecoilData.KickBack;
        float kickUp = _weaponData.RecoilData.KickUp;

        float min = Mathf.Min(kickBack, kickUp);
        float max = Mathf.Max(kickBack, kickUp);

        float recoilX = UnityEngine.Random.Range(min, max);

        Vector2 recoil = Vector2.right * recoilX;

        transform.localPosition -= new Vector3(recoil.x, 0f, 0f);
    }

    private void RecoverRecoil()
    {
        if (!UseRecoil) return;
        transform.localPosition = Vector3.SmoothDamp(transform.localPosition, _baseLocalPos, ref _recoilVelocity, 0.2f);
    }

    public void ReloadAmmo()
    {
        // 필요 탄약량
        int requiredAmmo = Context.WeaponData.MaxCapacity - Context.CurrentCapacity;
        // 필요탄약량 혹은 현재 탄약량중 더 적은것을 반환
        int reloadAmount = Mathf.Min(requiredAmmo, Context.AmmoRemaining);

        Context.CurrentCapacity += reloadAmount;
        Context.AmmoRemaining -= reloadAmount;

        bool dataNotNull = Context.WeaponData != null && Context.WeaponData.BulletModel != null;
        Context.WeaponFlag.CanFire = dataNotNull && Context.CurrentCapacity > 0; // 현재 탄약이 0 이상, 
        Context.WeaponFlag.IsReloadComplete = true;
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

        _firePosition = _model ? _model.FireTransform : transform;

        SetContext();
        InitAmmo();
        OnWeaponChanged?.Invoke();
    }

    private void SetContext()
    {
        Context.Weapon = this;
        Context.WeaponPos = transform;

        Context.WeaponInput = _input;
        Context.WeaponFlag = _flag;

        Context.WeaponData = _weaponData == null ? _baseData : _weaponData;
        Context.FirePosition = _firePosition == null ? transform : _firePosition;
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
        Context.BurstRemaining = Context.WeaponData.BurstCount;

        _maxFireRate = 1f / Context.WeaponData.FireRatePerSecond;
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
        IsAlive = Context.Weapon.IsAlive;
        CanFire = Context.WeaponFlag.CanFire;
        CanReload = Context.WeaponFlag.CanReload;
        AttackSequenceStarted = Context.WeaponFlag.AttackSequenceStarted;

        IsInterrupted = Context.IsInterrupted;
        IsAiming = Context.WeaponFlag.IsAiming;
        IsAimComplete = Context.WeaponFlag.IsAimComplete;
        IsRecoilComplete = Context.WeaponFlag.IsRecoilComplete;
        IsReloadComplete = Context.WeaponFlag.IsReloadComplete;

        // 탄약
        CurrentCapacity = Context.CurrentCapacity;
        AmmoRemaining = Context.AmmoRemaining;
    }
}