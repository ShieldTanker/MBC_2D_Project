using System.Collections;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    private WeaponStateMachine _machine;

    // Context관련
    private WeaponContext _context;
    private WeaponInput _input = new WeaponInput();
    private WeaponFlag _flag = new WeaponFlag();

    private Vector3 _weaponLocalOffset = new Vector3(0.6f, 0, 0);

    private WeaponModel _model;

    public Transform FirePosition;
    public Transform WeaponAnchor;

    // 조준 관련
    public Transform AimTarget;
    public float AimSpeed = 10f;
    float angle = 0f;

    private Vector3 _baseLocalPos;
    private Quaternion _baseLocalRot;

    private Vector3 _recoilVelocity = Vector3.zero;

    // 무기 데이터
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

        _machine = new WeaponStateMachine(_context);
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
        if (_context.FireRate <= 1f / _context.WeaponData.FireRateSecond)
        {
            _context.FireRate += Time.deltaTime;
        }

        WeaponAim();
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

        // 현재 버튼을 누르고 있는 상태
        _input.AttackPressed = true;

        // 새로운 공격 입력이 시작되었다는 1회성 이벤트
        _flag.AttackSequenceStarted = true;

        // 현재 입력을 Hold 상태로 기록
        _input.AttackHold = true;
    }

    /// <summary>
    /// 공격 버튼을 뗐을 때 호출
    /// </summary>
    public void CanceledFire()
    {
        if (_weaponData == null)
            return;

        // 실제 버튼 상태만 변경합니다.
        _input.AttackPressed = false;
        _input.AttackHold = false;
    }
    #endregion

    #region 무기 조작
    // 무기의 방향을 목표로 회전
    private void WeaponAim()
    {
        if (_context.WeaponFlag.IsAiming)
        {
            Vector3 dir = AimTarget.position - WeaponAnchor.position;

            angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
            angle = Mathf.LerpAngle(WeaponAnchor.eulerAngles.z, angle, AimSpeed * Time.deltaTime);

            WeaponAnchor.rotation = Quaternion.Euler(0, 0, angle);
        }
    }

    private void RecoilExample()
    {
        transform.localPosition =
            Vector3.SmoothDamp(transform.localPosition, _context.WeaponBaseLocalPos,
            ref _context.recoilVelocity, 0.5f);
    }
    #endregion

    #region 초기화
    private void SetWeapon()
    {
        if (_weaponData == null)
            _weaponData = _baseData;

        if (_model != null)
        {
            Destroy(_model);
        }

        if (_weaponData != null && _weaponData.Model != null)
        {
            _model = Instantiate(_weaponData.Model, transform);

            _model.transform.localPosition = _weaponLocalOffset;
            _model.transform.rotation = transform.rotation;
        }

        FirePosition = _model?.FirePosition;

        SetContext();
    }

    private void SetContext()
    {
        _context ??= new WeaponContext();

        _context.WeaponBaseLocalPos = _baseLocalPos;
        _context.WeaponBaseLocalRot = _baseLocalRot;
        _context.WeaponPos = transform;
        _context.AimTarget = AimTarget;

        _context.recoilVelocity = _recoilVelocity;

        _context.WeaponInput = _input;
        _context.WeaponFlag = _flag;

        _context.WeaponAnchorPos = WeaponAnchor;

        _context.WeaponData =
            _weaponData == null ? _baseData : _weaponData;

        _context.FirePosition =
            FirePosition == null ? transform : FirePosition;
    }

    private void InitAmmo()
    {
        if (_context.WeaponData == null)
        {
            _context.CurrentCapacity = 0;
            _context.AmmoRemaining = 0;
            return;
        }

        _context.CurrentCapacity =
            _context.WeaponData.MaxCapacity;

        _context.AmmoRemaining =
            _context.WeaponData.MaxAmmo;
    }

    #endregion

    // 디버깅
    private void DebugContext()
    {
        // 입력
        AttackPressed = _context.WeaponInput.AttackPressed;
        AttackHeld = _context.WeaponInput.AttackHold;
        InteractionPressed = _context.WeaponInput.InteractionPressed;

        // 플래그
        AttackSequenceStarted = _context.WeaponFlag.AttackSequenceStarted;
        CanFire = _context.WeaponFlag.CanFire;
        CanReload = _context.WeaponFlag.CanReload;

        IsInterrupted = _context.IsInterrupted;
        IsAiming = _context.WeaponFlag.IsAiming;
        IsAimComplete = _context.WeaponFlag.IsAimComplete;
        IsRecoilComplete = _context.WeaponFlag.IsRecoilComplete;
        IsReloadComplete = _context.WeaponFlag.IsReloadComplete;

        // 탄약
        CurrentCapacity = _context.CurrentCapacity;
        AmmoRemaining = _context.AmmoRemaining;
        FireRate = _context.FireRate;
    }
}