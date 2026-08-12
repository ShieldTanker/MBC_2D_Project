using UnityEngine;

public interface IMoveInput2D
{
    public Vector2 MoveInput { get; }
}

public interface IJumpInput
{
    public bool JumpInput { get; }
}

public interface IBoostInput
{
    public bool BoostInput { get; }
}