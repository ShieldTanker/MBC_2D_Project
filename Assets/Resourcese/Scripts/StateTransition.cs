public abstract class StateTransition
{
    // 다음으로 이동할 스테이트
    AgentStateType _nextState;

    // 매프레임 해당 상태의 전이조건 검사
    public abstract bool CheckStateTransit(StateContext context, float deltaTime);

    public abstract AgentStateType ChangeNextState();
}
