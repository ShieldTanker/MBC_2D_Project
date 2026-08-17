using System;
using UnityEngine;

public class InputController : MonoBehaviour, IMoveInput2D, IJumpInput, IWeaponInput
{
    // 움직임 입력
    public Vector2 MoveInput {  get; protected set; }

    // 점프 입력
    public bool JumpInput {  get; protected set; }

    // 공격 입력 시도
    public Action F_HandPerformedFire { get; set; }
    public Action B_HandPerformedFire { get; set; }
    public Action F_ShoulderPerformedFire { get; set; }
    public Action B_ShoulderPerformedFire { get; set; }

    // 공격 입력 해제
    public Action F_HandCanceledFire { get; set; }
    public Action B_HandCanceledFire { get; set; }
    public Action B_ShoulderCanceledFire { get; set; }
    public Action F_ShoulderCanceledFire { get; set; }
}