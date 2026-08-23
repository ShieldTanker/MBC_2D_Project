public class AgentContext : StateContext
{
    // TODO : Agent에 필요한 요소들을 넣을것
    public AgentStat AgentStat { get; set; }
    public Movement2D Move { get; set; }
    public Rotation2D Rotation { get; set; }
    public ModelController ModelCon { get; set; }

    public InputController InputCon { get; set; }
    public IJumpInput JumpInput { get; set; }
    public IMoveInput2D MoveInput { get; set; }

    public AgentFlag AgentFlag { get; set; }
}

public class AgentFlag
{
    // Controller에서 처리
    public bool OnGround { get; set; }

    // 상태에서 처리
    public bool IsJumping { get; set; }

    public CharDirection CharDirection { get; set; } = CharDirection.Right;
}