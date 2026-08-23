using System;
using UnityEngine;

public class InputController : MonoBehaviour,
    IMoveInput2D, IJumpInput, IWeaponInput, ILookInput, ILockOnInput
{
    // 움직임 입력
    public Vector2 MoveInput { get; protected set; }
    public float Horizontal { get; protected set; }
    public bool JumpPressed { get ; set; }

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

    // 마우스, 패드로 카메라 등 입력
    public Vector2 LookInput { get; set; }
    public Action<Vector2> LookAction { get; set; }

    // 락온 입력
    public Action LockOnManualToggleAction { get; set; }
    public Action LockOnNextTargetAction { get; set; }
    public Action LockOnPrevTargetAction { get; set; }

}
