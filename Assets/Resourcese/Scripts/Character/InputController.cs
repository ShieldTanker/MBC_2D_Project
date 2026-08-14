using System;
using UnityEngine;

public class InputController : MonoBehaviour, IMoveInput2D, IJumpInput, IWeaponInput
{
    // 움직임 입력
    public Vector2 MoveInput {  get; protected set; }

    // 점프 입력
    public bool JumpInput {  get; protected set; }

    // 공격 입력
    public Action F_HandFire { get; set; }
    public Action B_HandFire { get; set; }
    public Action F_ShoulderFire { get; set; }
    public Action B_ShoulderFire { get; set; }   
}