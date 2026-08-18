using System.Collections;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    private WeaponStateMachine _machine;
    private WeaponContext _context;

    private WeaponInput _input = new WeaponInput();
    private WeaponFlag _flag = new WeaponFlag();

    private Vector3 _weaponLocalOffset = new Vector3(0.6f, 0, 0);

    private WeaponModel _model;

    public Transform FirePosition;
    public Transform WeaponAnchor;

    private Vector3 _baseLocalPos;
    private Quaternion _baseLocalRot;

    private Vector3 _recoilVelocity = Vector3.zero;

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

    [Tooltip("플래그")]
    public bool CanFire = true;
    public bool CanReload;
    public bool IsAimComplete;
    public bool IsFireComplete;
    public bool IsRecoilComplete;
    public bool IsReloadComplete;

    [Tooltip("탄약")]
    public int CurrentCapacity;
    public int AmmoRemaining;

    [Tooltip("공격 입력 이벤트")]
    public bool AttackSequenceStarted;

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

        RecoilExample();
        DebugContext();
    }

    /// <summary>
    /// 공격 버튼을 눌렀을 때 호출됩니다.
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
    /// 공격 버튼을 뗐을 때 호출됩니다.
    /// </summary>
    public void CanceledFire()
    {
        if (_weaponData == null)
            return;

        // 실제 버튼 상태만 변경합니다.
        _input.AttackPressed = false;
        _input.AttackHold = false;
    }

    private void RecoilExample()
    {
        transform.localPosition = Vector3.SmoothDamp(
            transform.localPosition,
            _context.WeaponBaseLocalPos,
            ref _context.recoilVelocity,
            0.5f);
    }

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
        IsAimComplete = _context.WeaponFlag.IsAimComplete;
        IsRecoilComplete = _context.WeaponFlag.IsRecoilComplete;
        IsReloadComplete = _context.WeaponFlag.IsReloadComplete;

        // 탄약
        CurrentCapacity = _context.CurrentCapacity;
        AmmoRemaining = _context.AmmoRemaining;

    }
}