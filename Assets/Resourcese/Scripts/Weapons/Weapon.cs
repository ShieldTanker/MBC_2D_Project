using System.Collections;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    private WeaponStateMachine _machine;
    private WeaponContext _context;
    private Vector3 weaponLocalOffset = new Vector3(0.6f, 0, 0);

    private WeaponModel _model;
    private RecoilData _recoilData;
    public Transform FirePosition;

    private Vector3 _baseLocalPos;
    private Quaternion _baseLocalRot;

    private bool _isRecoil;

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

    private void Awake()
    {
        _baseLocalPos = transform.position;
        _baseLocalRot = transform.localRotation;
        SetContext();
    }

    private void Start()
    {
        SetWeapon();
    }

    public void TryFire()
    {
        if (_weaponData == null) { return; }
        _isRecoil = true;
        Debug.Log($"{this.gameObject.name} : 사격 시도");
    }

    void SetWeapon()
    {
        if (_weaponData == null) { _weaponData = _baseData; }

        _recoilData = _weaponData.RecoilData;

        if (_model != null) { Destroy(_model); } // 나중에 오브젝트 풀 같은데에 반환하기

        FirePosition = transform;
        if (_weaponData.Model != null)
        {
            _model = Instantiate(_weaponData.Model, transform);
            _model.transform.localPosition = weaponLocalOffset;
            _model.transform.rotation = transform.rotation;

            FirePosition = _model.FirePosition;
        }
    }

    private void SetContext()
    {
        _context = new WeaponContext();
        if(FirePosition == null) { FirePosition = transform; }
        _context.FirePosition = FirePosition;
    }
}