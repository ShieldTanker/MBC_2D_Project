using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public abstract class StateMachine<TState, TContext> where TState : Enum where TContext : StateContext
{
    protected Dictionary<TState, State<TState, TContext>> stateDic = new();
    public State<TState, TContext> CurrentState;
    public TContext Context;

    public virtual void Update(float deltaTime)
    { CurrentState?.Update(deltaTime); }

    // 상속받은 상태머신 내에서 enum타입을 캐스팅하기
    /// <summary>
    /// 각 상태머신에 맞는 상태 enum 반환
    /// </summary>
    /// <param name="stateType">상태머신의 딕셔너리에 보낼 상태이넘타입</param>
    /// <returns></returns>
    protected abstract State<TState, TContext> CreateState(TState stateType);

    /// <summary>
    /// 각 상태머신의 맞는 enum으로 상태 변환
    /// </summary>
    /// <param name="stateType"></param>
    /// <exception cref="Exception"></exception>
    public void ChangeState(TState stateType)
    {
        // TODO : 딕셔너리에서 값을 찾을수 있으면 가져오고
        //        못찾으면 새로 하나 만들고 딕셔너리에 저장
        if (!stateDic.TryGetValue(stateType, out State<TState, TContext> state))
        {
            state = CreateState(stateType);

            if (state == null)
                throw new Exception($"{stateType} 상태 생성 실패");

            stateDic.Add(stateType, state);
        }

        // 만약 바꾸려는 상태가 현재상태와 같으면 리턴
        if (CurrentState == state)
            return;

        // TODO : 현재 상태가 존재하면 현재 상태의 StateExit()를 실행한뒤 바꾸기
        CurrentState?.StateExit();

        // TODO : 현재 상태를 바꾼뒤 상태의 StateEnter()실행
        CurrentState = state;
        CurrentState.StateEnter();
    }
}
