public class WeaponStateMachine : StateMachine<WeaponStateType, WeaponContext>
{
    public WeaponStateMachine(WeaponContext context) { Context = context; }

    protected override State<WeaponStateType, WeaponContext> CreateState(WeaponStateType stateType)
    {
        State<WeaponStateType, WeaponContext> state = this.Create(stateType);
        return state;
    }
}
