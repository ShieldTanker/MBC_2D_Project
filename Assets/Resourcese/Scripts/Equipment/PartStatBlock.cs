using System;
using UnityEngine;

/// <summary>
/// 파츠(머리/몸통/팔/다리)가 기여하는 스탯 보정치.
/// 모든 파츠가 같은 필드 세트를 공유하되, 파츠별로 관련 없는 값은 0으로 둔다.
/// 예) LegsPartData는 LockOnRange를 안 건드리고 0으로 두면 합산에 영향 없음.
/// 새 스탯이 필요하면 여기 필드만 추가하면 AgentStat/LoadOut 쪽은 자동으로 합산에 포함된다.
/// </summary>
[Serializable]
public struct PartStatBlock
{
    [Header("체력")]
    public int MaxHp;

    [Header("이동")]
    public float MoveSpeed;
    public float BoostSpeed;
    public float Acceleration;
    public float Deceleration;
    public float BoostAcceleration;
    public float BoostDeceleration;

    [Header("점프")]
    public float JumpHeight;
    public float JumpDuration;
    public float JumpDelay;

    [Header("락온")]
    public float LockOnRange;
    public float LockOnSpeed;
    public float ManualAimSpeed;

    // 파츠 구조체 내부의 공통 필드들을 더함
    public static PartStatBlock operator +(PartStatBlock a, PartStatBlock b)
    {
        return new PartStatBlock
        {
            MoveSpeed = a.MoveSpeed + b.MoveSpeed,
            BoostSpeed = a.BoostSpeed + b.BoostSpeed,
            Acceleration = a.Acceleration + b.Acceleration,
            Deceleration = a.Deceleration + b.Deceleration,
            BoostAcceleration = a.BoostAcceleration + b.BoostAcceleration,
            BoostDeceleration = a.BoostDeceleration + b.BoostDeceleration,

            JumpHeight = a.JumpHeight + b.JumpHeight,
            JumpDuration = a.JumpDuration + b.JumpDuration,
            JumpDelay = a.JumpDelay + b.JumpDelay,

            LockOnRange = a.LockOnRange + b.LockOnRange,
            LockOnSpeed = a.LockOnSpeed + b.LockOnSpeed,
            ManualAimSpeed = a.ManualAimSpeed + b.ManualAimSpeed,
        };
    }
}