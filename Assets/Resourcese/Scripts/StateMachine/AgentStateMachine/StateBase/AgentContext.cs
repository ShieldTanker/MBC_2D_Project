public class AgentContext : StateContext
{
    // TODO : Agent에 필요한 요소들을 넣을것
    public Movement2D Move {  get; set; }
    public InputController InputCon {  get; set; }
    public AnimController AnimCon { get; set; }


    public IJumpInput JumpInput { get; set; }
    public IMoveInput2D MoveInput { get; set; }
}
