using UnityEngine;

// 다른 상태머신에서 자기가 쓰는 enum이 각자 달라 제네릭으로 구현
public abstract class StateTransition<TStateType, TContext> 
{
    protected TContext _context;
    protected TStateType _nextState;
    public StateTransition(TContext context, TStateType stateType)
    {
        _context = context;
        _nextState = stateType;
    }

    /// <summary> 매프레임 해당 상태의 전이조건 검사 </summary>
    /// <param name="deltaTime"></param>
    /// <returns></returns>
    public abstract bool CheckStateTransit(float deltaTime);

    /// <summary> 각 상태머신에 맞는 상태enum으로 반환 </summary>
    /// <returns></returns>
    public TStateType ChangeNextState()
    {
        return _nextState;
    }

    /// <summary> 트랜지션 탐색 조건 초기화 </summary>
    public abstract void Clear();
}