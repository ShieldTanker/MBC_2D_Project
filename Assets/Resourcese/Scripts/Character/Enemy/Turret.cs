using UnityEngine;
using UnityEngine.Splines;

[RequireComponent(typeof(AgentStat))]
[RequireComponent(typeof(WeaponController))]
[RequireComponent(typeof(Health))]
public class Turret : MonoBehaviour
{
    LockonController _lockon;
    WeaponController _weaponCon;
    Health _health;
    AgentStat _stat;
    TargetableEntity _targetEntity;

    private void Awake()
    {
        _stat = GetComponent<AgentStat>();
        _lockon = GetComponentInChildren<LockonController>();
        _targetEntity = GetComponentInChildren<TargetableEntity>();
        _weaponCon = GetComponent<WeaponController>();
        _health = GetComponent<Health>();
    }

    private void Start()
    {
        _lockon.Init(_stat, new DistanceTargetSelector());
        _weaponCon.SetLockonController(_lockon);
        _health.MaxHealth = 300;
        _health.Init();
        _health.OnDieAction += OnDie;
    }

    private void Update()
    {
        if (!_stat.IsAlive)
            return;

        if(_lockon.CurrentTargetTransform != null)
        {
            _weaponCon.F_HandAnchor.Weapon.PerformedFire();
        }
        else
        {
            _weaponCon.F_HandAnchor.Weapon.CanceledFire();
        }
    }

    void OnDie(DamageInfo _)
    {
        // 중복 호출 및 로직 실행 방지
        if (!_stat.IsAlive)
            return;
        _stat.IsAlive = false;
        _targetEntity.IsLockable = false;

        Destroy(gameObject, 1f);
    }
}
