//using UnityEngine;

//public class AgentJumpStateLogic : StateLogic<AgentContext>
//{
//    float time = 0f;
//    public AgentJumpStateLogic(AgentContext context) : base(context) { }

//    public override void UpdateStateLogic(float deltaTime)
//    {
//        if(time >= _context.AgentStat.JumpDuration)
//        {
//            time = 0;
//            _context.AgentStat.IsJumping = false;
//        }
//    }
//}