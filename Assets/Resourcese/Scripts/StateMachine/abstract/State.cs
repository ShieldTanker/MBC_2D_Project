using System;
using System.Collections.Generic;
using UnityEngine;

public abstract class State<TStateType, TContext>
    where TStateType : Enum
    where TContext : StateContext
{
    protected StateMachine<TStateType, TContext> _machine;
    protected List<StateTransition<TStateType, TContext>> _transitions;

    /// <summary>
    /// 각 상태에서 상태진입시 실행될 함수
    /// </summary>
    public virtual void StateEnter()
    {
        Debug.Log($"{this.GetType()} 진입");
        ClearTransition();
    }

    /// <summary>
    /// 각 상태에서 매프레임 실행될 함수
    /// </summary>
    /// <param name="deltaTime"></param>
    public virtual void StateUpdate(float deltaTime)
    {
        if (_transitions != null) // 조건들 리스트가 비어있으면 리턴
        {
            foreach (var transit in _transitions)
            {
                if (transit.CheckStateTransit(deltaTime)) // 검사 조건에서 변이 조건 만족시
                {
                    _machine.ChangeState(transit.ChangeNextState()); // 상태머신에게 상태 변경요청
                    break;
                }
            }
        }
    }

    /// <summary>
    /// 각 상태에서 상태 나갈시 실행될 함수
    /// </summary>
    public virtual void StateExit()
    {
        ClearTransition();
    }

    void ClearTransition()
    {
        foreach (StateTransition<TStateType, TContext> transition in _transitions) { transition.Clear(); }
    }
}