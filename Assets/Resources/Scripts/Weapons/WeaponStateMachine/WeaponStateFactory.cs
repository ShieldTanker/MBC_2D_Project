using System.Collections.Generic;

public static class WeaponStateFactory
{
    public static State<WeaponStateType, WeaponContext> Create(
        this WeaponStateMachine machine,
        WeaponStateType stateType)
    {
        State<WeaponStateType, WeaponContext> state = null;

        List<StateTransition<WeaponStateType, WeaponContext>> transitions = new();

        switch (stateType)
        {
            // 기본 상태
            case WeaponStateType.Idle:
                transitions.Add(new WeaponIdleToAim(machine.Context, WeaponStateType.Aim));
                transitions.Add(new WeaponIdleToReload(machine.Context, WeaponStateType.Reload));

                state = new WeaponIdleState(machine, transitions);
                break;

            // 조준
            case WeaponStateType.Aim:
                transitions.Add(new WeaponAimToFire(machine.Context, WeaponStateType.Fire));
                transitions.Add(new WeaponAimToReload(machine.Context, WeaponStateType.Reload));
                transitions.Add(new WeaponAimToIdle(machine.Context, WeaponStateType.Idle));

                state = new WeaponAimState(machine, transitions);
                break;

            // 발사
            case WeaponStateType.Fire:
                transitions.Add(new WeaponFireToEmpty(machine.Context, WeaponStateType.Empty));
                transitions.Add(new WeaponFireToAim(machine.Context, WeaponStateType.Aim));

                state = new WeaponFireState(machine, transitions);
                break;

            // 탄약 없음
            case WeaponStateType.Empty:
                transitions.Add(new WeaponEmptyToReload(machine.Context, WeaponStateType.Reload));

                state = new WeaponEmptyState(machine, transitions);
                break;

            // 재장전
            case WeaponStateType.Reload:
                transitions.Add(new WeaponReloadToIdle(machine.Context, WeaponStateType.Idle));

                state = new WeaponReloadState(machine, transitions);
                break;
        }

        return state;
    }
}