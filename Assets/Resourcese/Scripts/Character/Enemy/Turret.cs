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
    

    private void Awake()
    {
        _stat = GetComponent<AgentStat>();
        _lockon = GetComponentInChildren<LockonController>();
        _weaponCon = GetComponent<WeaponController>();
        _health = GetComponent<Health>();
    }

    private void Start()
    {
        _lockon.Init(_stat, new DistanceTargetSelector());
        _weaponCon.SetLockonController(_lockon);
        _health.OnDieAction += OnDie;
    }

    private void Update()
    {
        if (!_stat.IsAlive)
            return;
        if(_lockon.CurrentTarget != null)
            _weaponCon.F_HandAnchor.Weapon.PerformedFire();
    }

    void OnDie(DamageInfo _)
    {
        // 중복 호출 및 로직 실행 방지
        if (!_stat.IsAlive)
            return;

        _stat.IsAlive = false;
        Destroy(gameObject, 1f);
    }
}
