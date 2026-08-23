using UnityEngine;

public class AgentStat : MonoBehaviour
{
    [Header("이동 관련 스탯")] // 이동관련 스탯 (파츠 장착 시 LoadOut.RecalculateStats()가 계산해서 덮어씀)
    public float MoveSpeed = 116f;      // 일반 속도
    public float BoostSpeed = 378f;     // 부스트 속도

    [Tooltip("지수 감쇠 반응 속도(1/초). Move()의 v(t) = target + (v0-target)*e^(-k*t) 모델에서의 k값 - 물리적 가속도(m/s²)가 아님")]
    public float Acceleration = 13.5f;      // 일반 가속도 (0→최대속도 약 0.4초)
    public float Deceleration = 54f;        // 일반 감속도 (최대속도→0 약 0.1초)
    public float BoostAcceleration = 2f;    // 부스트 가속도 (0→최대속도 약 2.8초)
    public float BoostDeceleration = 9.5f;  // 부스트 감속도 (최대속도→0 약 0.7초)

    public bool IsBoost = false;        // 부스트 적용 유무

    [Tooltip("목표 점프 높이(월드 유닛). 실제 발사속도는 Movement2D.Jump()에서 v0=√(2·g·h)로 역산해서 적용됨")]
    public float JumpHeight = 21f;      // 점프 높이
    public float JumpDuration = 0.64f;  // 점프상태(상승) 유지 시간 - 목표 높이까지 도달하는 시간과 맞춰야 함
    public float JumpDelay = 0.2f;      // 점프할때까지의 딜레이

    public CharDirection CurrentDirection = CharDirection.Right;

    // 전투 관련 스탯 (파츠 합산 결과)
    [Header("전투 관련 스탯")]
    [Tooltip("최대 체력")]
    public int MaxHp = 100;
    public int CurrentHp = 100;

    [Tooltip("락온 가능 사거리 - 머리(센서) 파츠 등에서 주로 기여")]
    public float LockonRange = 15f;

    [Tooltip("실제 추적 위치가 예측 위치를 따라가는 속도")]
    public float LockOnSpeed = 8f;

    [Tooltip("Manual 모드에서 예측 위치가 입력을 따라 이동하는 속도")]
    public float ManualAimSpeed = 10f;

    [Header("파츠 미장착 시 기본값 (기체 프레임 자체 성능)")]
    public PartStatBlock BaseStat = new PartStatBlock
    {
        MaxHp = 0,
        MoveSpeed = 0f,
        BoostSpeed = 0f,
        Acceleration = 0f,
        Deceleration = 0f,
        BoostAcceleration = 0f,
        BoostDeceleration = 0f,

        JumpHeight = 0f,
        JumpDuration = 0f,
        JumpDelay = 0f,

        LockOnRange = 0f,
        LockOnSpeed = 0f,
        ManualAimSpeed = 0f,
    };

    /// <summary>
    /// LoadOut이 (기본값 + 4개 파츠 스탯) 합산 결과를 여기로 적용할 때 사용.
    /// 다른 곳에서 이 메서드를 직접 호출할 일은 거의 없음.
    /// </summary>
    public void ApplyStatBlock(PartStatBlock total)
    {
        MaxHp = total.MaxHp;
        CurrentHp = MaxHp;

        MoveSpeed = total.MoveSpeed;
        BoostSpeed = total.BoostSpeed;
        Acceleration = total.Acceleration;
        Deceleration = total.Deceleration;
        BoostAcceleration = total.BoostAcceleration;
        BoostDeceleration = total.BoostDeceleration;

        JumpHeight = total.JumpHeight;
        JumpDuration = total.JumpDuration;
        JumpDelay = total.JumpDelay;

        LockonRange = total.LockOnRange;
        LockOnSpeed = total.LockOnSpeed;
        ManualAimSpeed = total.ManualAimSpeed;
    }
}