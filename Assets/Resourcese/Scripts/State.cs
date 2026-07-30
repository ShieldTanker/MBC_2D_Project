using System;
using System.Collections.Generic;

public abstract class State<TState, TContext> where TState : Enum where TContext : StateContext
{
    StateMachine<TState,TContext> _machine;
    List<StateTransition> _transitions;
    List<StateLogic> _logics;

    /// <summary>
    /// StateFactory에서 초기화할 상태머신 초기화 함수
    /// </summary>
    /// <param name="machine"></param>
    public void InitStateMachine(StateMachine<TState, TContext> machine)
    {
        _machine = machine;
    }

    /// <summary>
    /// StateFactory에서 각 상태에 초기화할 상태전이 검사함수
    /// </summary>
    /// <param name="transitions"></param>
    public void InitStateTransition(List<StateTransition> transitions)
    {
        _transitions = transitions;
    }

    /// <summary>
    /// StateFactory에서 각 상태에 초기화할 로직함수
    /// </summary>
    /// <param name="logics"></param>
    public void InitStateLogic(List<StateLogic> logics)
    {
        _logics = logics;
    }

    /// <summary>
    /// 각 상태에서 상태진입시 실행될 함수
    /// </summary>
    public virtual void StateEnter()
    {

    }

    /// <summary>
    /// 각 상태에서 매프레임 실행될 함수
    /// </summary>
    /// <param name="deltaTime"></param>
    public virtual void Update(float deltaTime)
    {
        // 조건들 리스트가 비어있으면 리턴
        if (_transitions == null) return;

        foreach (var transit in _transitions)
        {
            // 검사 조건에서 변이 조건 만족시
            if(transit.CheckStateTransit(_machine.Context, deltaTime))
            {
                // 상태머신에게 상태 변경요청
                // _machine.ChangeState(transit.ChangeNextState());
            }
        }

        // 수행할 로직들이 비어있으면 리턴
        if(_logics == null) return;
        foreach(var logic in _logics)
        {
            logic.UpdateStateLogic(_machine.Context, deltaTime);
        }
    }

    /// <summary>
    /// 각 상태에서 상태 나갈시 실행될 함수
    /// </summary>
    public virtual void StateExit()
    {

    }
}
