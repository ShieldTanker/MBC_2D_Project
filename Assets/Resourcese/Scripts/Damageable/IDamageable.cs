using System;
using UnityEngine;

public struct DamageInfo
{
    public Vector3 AttackPosition;
    public int Damage;

}

public interface IDamageable
{
    public event Action<DamageInfo> OnDamageAction;
    public void TakeDamage(DamageInfo damageInfo);
}

public interface IDieable
{
    public event Action<DamageInfo> OnDieAction;
    public void OnDie(DamageInfo damageInfo);
}
