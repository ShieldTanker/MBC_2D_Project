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
    public Action B_HandPerformedFire { get; set; }
    public Action F_HandPerformedFire { get; set; }
    public Action B_ShoulderPerformedFire { get; set; }
    public Action F_ShoulderPerformedFire { get; set; }

    public Action B_HandCanceledFire { get; set; }
    public Action F_HandCanceledFire { get; set; }
    public Action B_ShoulderCanceledFire { get; set; }
    public Action F_ShoulderCanceledFire { get; set; }
}

public interface IBoostInput
{
    public bool BoostInput { get; }
}