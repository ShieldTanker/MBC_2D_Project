using System;
using UnityEngine;

public class Health : MonoBehaviour, IDamageable, IDieable
{
    public event Action<DamageInfo> OnDamageAction;
    public event Action<DamageInfo> OnDieAction;

    [SerializeField] private int _currentHealth;
    [SerializeField] private int _maxHealth;

    #region 속성
    public int CurrentHealth
    { 
        get { return _currentHealth; }
        set { _currentHealth = value; }
    }

    public int MaxHealth 
    {
        get { return _maxHealth; }
        set { _maxHealth = value; }
    }
    #endregion

    public void Init()
    {
        _currentHealth = _maxHealth;
    }

    public void TakeDamage(DamageInfo damageInfo)
    {
        _currentHealth -= damageInfo.Damage;
        OnDamageAction?.Invoke(damageInfo);
        if(_currentHealth <= 0)
            OnDie(damageInfo);
    }

    public void OnDie(DamageInfo damageInfo)
    {
        _currentHealth = 0;
        OnDieAction?.Invoke(damageInfo);
    }
}