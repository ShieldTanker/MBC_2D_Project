using System.Collections;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    private WeaponStateMachine _machine;
    private WeaponContext _context;
    private WeaponInput _input = new WeaponInput();
    private WeaponFlag _flag = new WeaponFlag();

    private Vector3 weaponLocalOffset = new Vector3(0.6f, 0, 0);

    private WeaponModel _model;
    public Transform FirePosition;
    public Transform WeaponAnchor;

    private Vector3 _baseLocalPos;
    private Quaternion _baseLocalRot;

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

    #region 컨텍스트 디버그
    [Header("Context Debug")]
    public bool AttackPressed;      // 공격 입력
    public bool AttackHeld;         // 
    public bool AttackReleased;     // 

    public bool ReloadPressed;      // 재장전 눌림
    public bool CanFire = true;            // 사격 가능 여부
    public bool CanReload;          // 재장전 가능

    // 완료 플래그
    public bool IsAimComplete;      // 조준 완료 플래그
    public bool IsFireComplete;     // 발사 완료 플래그
    public bool IsRecoilComplete;   // 반동 완료 플래그
    public bool IsReloadComplete;   // 재장전 완료 플래그 

    // 탄약
    public int CurrentCapacity;         // 현재 장탄수
    public int AmmoRemaining;           // 남은 탄약수

    public bool AttackSequenceStarted;  // 공격 시작
    public bool IsInterrupted = false;  // 강제행동 여부

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
        DebugContext();
    }

    public void TryFire()
    {
        if (_weaponData == null) { return; }
        Debug.Log($"{this.gameObject.name} : 사격 시도");
        _input.AttackPressed = true;
    }

    void SetWeapon()
    {
        if (_weaponData == null) { _weaponData = _baseData; }

        if (_model != null)
        { Destroy(_model); } // 나중에 아이템DB 같은데에 반환하기

        if (_weaponData.Model != null)
        {
            // 나중에 아이템DB 같은데에서 가져오기
            _model = Instantiate(_weaponData.Model, transform);
            _model.transform.localPosition = weaponLocalOffset;
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

        _context.WeaponInput = _input;
        _context.WeaponFlag = _flag;
        _context.WeaponAnchorPos = WeaponAnchor;

        _context.WeaponData = _weaponData == null ? _baseData : _weaponData;
        _context.FirePosition = FirePosition == null ? transform : FirePosition;
    }

    private void InitAmmo()
    {
        if (_context.WeaponData == null)
        {
            _context.CurrentCapacity = 0;
            _context.AmmoRemaining = 0;
            return;
        }

        _context.CurrentCapacity = _context.WeaponData.MaxCapacity;

        _context.AmmoRemaining = _context.WeaponData.MaxAmmo;
    }

    void DebugContext()
    {
        AttackPressed = _context.WeaponInput.AttackPressed;      // 공격 입력
        AttackHeld = _context.WeaponInput.AttackHeld;         // 
        AttackReleased = _context.WeaponInput.AttackReleased;     // 

        ReloadPressed = _context.WeaponInput.ReloadPressed;      // 재장전 눌림
        CanFire = _context.WeaponFlag.CanFire;            // 사격 가능 여부
        CanReload = _context.WeaponFlag.CanReload;          // 재장전 가능

        // 완료 플래그
        IsAimComplete = _context.WeaponFlag.IsAimComplete;      // 조준 완료 플래그
        IsFireComplete = _context.WeaponFlag.IsFireComplete;     // 발사 완료 플래그
        IsRecoilComplete = _context.WeaponFlag.IsRecoilComplete;   // 반동 완료 플래그
        IsReloadComplete = _context.WeaponFlag.IsReloadComplete;   // 재장전 완료 플래그 

        // 탄약
        CurrentCapacity = _context.CurrentCapacity;         // 현재 장탄수
        AmmoRemaining = _context.AmmoRemaining;           // 남은 탄약수

        AttackSequenceStarted = _context.AttackSequenceStarted;  // 공격 시작
        IsInterrupted = _context.IsInterrupted;  // 강제행동 여부

        AimIdleTimer = _context.AimIdleTimer;
    }
}