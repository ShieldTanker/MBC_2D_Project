using System.Collections.Generic;
using UnityEngine;

public class WeaponAimState : State<WeaponStateType, WeaponContext>
{
    private WeaponContext _context;
    private WeaponFlag _flag;

    private float _aimTime;

    public WeaponAimState(WeaponStateMachine machine, List<StateTransition<WeaponStateType, WeaponContext>> transitions)
    {
        _machine = machine;
        _transitions = transitions;

        _context = _machine.Context;
        _flag = _context.WeaponFlag;
    }

    public override void StateEnter()
    {
        base.StateEnter();

        _aimTime = 0f;
        _flag.IsAiming = true;
        _flag.IsAimCanceled = false;
    }

    public override void StateUpdate(float deltaTime)
    {
        // 조준이 완료되지않으면 조준완료 될 때 까지 검사
        if (!_flag.IsAimComplete)
        {
            _aimTime += deltaTime;
            _context.WeaponFlag.IsAimComplete = _aimTime >= 0.5f;
        }

        // 마지막 발사 이후 경과 시간이 1초 이상이면 조준 취소
        _context.LastFireTime += deltaTime;
        if (_context.LastFireTime > 1f)
        {
            _flag.IsAimCanceled = true;
            _context.LastFireTime = 0f;
        }

        base.StateUpdate(deltaTime);
    }

    public override void StateExit()
    {
        base.StateExit();

        _aimTime = 0f;
    }
}