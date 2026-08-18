using System.Collections.Generic;
using UnityEngine;

public class WeaponAimState : State<WeaponStateType, WeaponContext>
{
    private WeaponContext _context;
    private WeaponFlag _flag;
    private WeaponInput _input;

    private float _aimTime;
    private float _aimToIdleTimer;

    public WeaponAimState(WeaponStateMachine machine, List<StateTransition<WeaponStateType, WeaponContext>> transitions)
    {
        _machine = machine;
        _transitions = transitions;

        _context = _machine.Context;
        _flag = _context.WeaponFlag;
        _input = _context.WeaponInput;
    }

    public override void StateEnter()
    {
        base.StateEnter();

        if (_context.WeaponAnchorPos != null)
        {
            _context.WeaponAnchorPos.rotation = Quaternion.Euler(0f, 0f, 0f);
        }

        _aimToIdleTimer = 0f;

        if (!_flag.IsAimComplete)
        {
            _aimTime = 0f;
        }

        _flag.IsAimCanceled = false;
    }

    public override void StateUpdate(float deltaTime)
    {
        if (!_flag.IsAimComplete)
        {
            _aimTime += deltaTime;

            float maxAimTime = Mathf.Max(_context.WeaponData.MaxAimTime, 0f);

            _flag.IsAimComplete = _aimTime >= maxAimTime;
        }

        // 마지막 발사 이후 경과 시간
        _context.TimeSinceLastFire += deltaTime;

        base.StateUpdate(deltaTime);
    }

    public override void StateExit()
    {
        base.StateExit();

        _aimToIdleTimer = 0f;

        if (!_flag.IsAimComplete)
        {
            _aimTime = 0f;
        }

        _flag.IsAimCanceled = false;
    }
}