using TMPro.EditorUtilities;
using UnityEngine;

public class AgentMoveStateLogic : StateLogic<AgentContext>
{
    public AgentMoveStateLogic(AgentContext context) : base(context) { }

    public override void UpdateStateLogic(float deltaTime)
    {
        _context.Move.MoveInput(_context.MoveInput.MoveInput);
    }
}
