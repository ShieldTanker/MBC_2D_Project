using TMPro.EditorUtilities;
using UnityEngine;

public class AgentMoveStateLogic : StateLogic<AgentContext>
{
    public AgentMoveStateLogic(AgentContext context) : base(context) { }

    public override void UpdateStateLogic(float deltaTime)
    {
        Vector2 input = _context.MoveInput.MoveInput;

        _context.Move.MoveInput(input);
        _context.AnimCon.SetFlaot("MoveX", input.x);

    }
}
