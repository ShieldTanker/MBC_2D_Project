using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public abstract class StateMachine<TState, TContext> where TState : Enum where TContext : StateContext
{
    protected Dictionary<TState, State> stateDic;
    public State CurrentState;
    public TContext Context;

    public virtual void Update(float deltaTime)
    {
        CurrentState?.Update(deltaTime);
    }

    // 상속받은 상태머신 내에서 enum타입을 캐스팅하기
    protected abstract State CreateState(TState stateType);

    public void ChangeState(TState stateType)
    {
        // TODO : 딕셔너리에서 값을 찾을수 있으면 가져오고
        //        못찾으면 새로 하나 만들고 딕셔너리에 저장
        if (!stateDic.TryGetValue(stateType, out State state))
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

// 나중에 .cs로 분리할것
public class AgentStateMachine : StateMachine<AgentStateType, AgentContext>
{
    AgentStateMachine(AgentContext context)
    {
        Context = context;
    }

    protected override State CreateState(AgentStateType stateType)
    {
        State state = this.Create(stateType);
        return state;
    }
}

public class GameStateMachine : StateMachine<GameStateType, GameContext>
{
    GameStateMachine(GameContext context)
    {
        Context = context;
    }

    protected override State CreateState(GameStateType stateType)
    {
        State state = this.InitState(stateType);
        return state;
    }
}