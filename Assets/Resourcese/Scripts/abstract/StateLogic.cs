public abstract class StateLogic<TContext>
{
    /// <summary>
    /// 매프레임 해당 상태의 로직을 실행하는함수
    /// </summary>
    /// <param name="context">상태에서 필요한 정보들을 담는 클래스</param>
    /// <param name="deltaTime">Time.deltaTime을 담는 변수</param>
    public abstract void UpdateStateLogic(float deltaTime);
}
