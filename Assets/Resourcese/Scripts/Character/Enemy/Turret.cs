using UnityEngine;

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
    }

    private void Update()
    {
        if(_lockon.CurrentTarget != null)
            _weaponCon.F_HandAnchor.Weapon.PerformedFire();
    }
}
