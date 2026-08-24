using System.Collections.Generic;
using UnityEngine;
// 재장전 상태
public class WeaponReloadState : State<WeaponStateType, WeaponContext>
{
    private float _currentReloadTime;

    private WeaponContext _context;
    WeaponFlag _flag;

    public WeaponReloadState(WeaponStateMachine machine,
        List<StateTransition<WeaponStateType, WeaponContext>> transitions)
    {
        _machine = machine;
        _transitions = transitions;

        _context = _machine.Context;
        _flag = _machine.Context.WeaponFlag;
    }

    public override void StateEnter()
    {
        base.StateEnter();

        _flag.CanFire = false;

        // 조준 관련 초기화
        _flag.IsAiming = false;
        _flag.IsAimComplete = false;
        _flag.IsAimCanceled = true;

        _flag.IsReloadComplete = false;
        _currentReloadTime = 0f;

        _context.Weapon.OnReloadStart?.Invoke();
    }

    public override void StateUpdate(float deltaTime)
    {
        //if (_context.WeaponFlag.IsReloadComplete) return;
        _currentReloadTime += deltaTime;

        // 현재 장전시간 >= 최대 장전시간
        if (_currentReloadTime >= _context.WeaponData.MaxReloadDuration && !_flag.IsReloadComplete)
            _context.Weapon.ReloadAmmo();

        base.StateUpdate(deltaTime);
    }

    public override void StateExit()
    {
        base.StateExit();
        _machine.Context.WeaponFlag.IsReloadComplete = false;
        _currentReloadTime = 0f;
        _context.Weapon.OnReloadExit?.Invoke();
    }
}