using System.Collections.Generic;
using UnityEngine;

public class WeaponFireState : State<WeaponStateType, WeaponContext>
{
    private WeaponContext _context;

    public WeaponFireState(WeaponStateMachine machine, List<StateTransition<WeaponStateType, WeaponContext>> transitions)
    {
        _machine = machine;
        _transitions = transitions;

        _context = _machine.Context;
    }

    public override void StateEnter()
    {
        base.StateEnter();

        _context.WeaponFlag.CanFire = _context.WeaponData.BulletModel != null
            && _context.CurrentCapacity > 0;
    }

    public override void StateUpdate(float deltaTime)
    {
        if (_context.WeaponData == null)
        {
            _context.WeaponFlag.CanFire = false;
            base.StateUpdate(deltaTime);
            return;
        }

        TryFire();

        base.StateUpdate(deltaTime);
    }

    public override void StateExit()
    {
        base.StateExit();
        _context.BurstRemaining = _context.WeaponData.BurstCount;
        _context.WeaponFlag.AttackSequenceStarted = false;
    }

    private void TryFire()
    {
        if (!_context.WeaponFlag.CanFire) return;
        // 버튼도 안 누르고 있고 새로운 공격 입력도 없으면 발사X
        WeaponInput input = _context.WeaponInput;
        if (!_context.WeaponFlag.AttackSequenceStarted
            && !input.AttackPressed) return;

        switch (_context.WeaponData.FireMode)
        {
            case WeaponFireMode.SemiAuto:
                if (_context.WeaponFlag.AttackSequenceStarted)
                    _context.Weapon.Fire();
                break;

            case WeaponFireMode.FullAuto:

                if (_context.WeaponFlag.AttackSequenceStarted || input.AttackPressed)
                    _context.Weapon.Fire();
                break;

            case WeaponFireMode.Burst:
                if (_context.BurstRemaining > 0)
                    _context.Weapon.Fire();
                
                break;
        }
    }
}