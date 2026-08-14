using UnityEngine;
using System;

public interface IMoveInput2D
{
    public Vector2 MoveInput { get; }
}

public interface IJumpInput
{
    public bool JumpInput { get; }
}

public interface IWeaponInput
{
    public Action B_HandFire { get; set; }
    public Action F_HandFire { get; set; }
    public Action B_ShoulderFire { get; set; }
    public Action F_ShoulderFire { get; set; }
}

public interface IBoostInput
{
    public bool BoostInput { get; }
}