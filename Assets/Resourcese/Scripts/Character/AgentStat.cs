using UnityEngine;

public class AgentStat : MonoBehaviour
{
    // 이동관련 스탯 (파츠 장착 시 LoadOut.RecalculateStats()가 계산해서 덮어씀)
    public float MoveSpeed = 20f;       // 일반 속도
    public float BoostSpeed = 60f;      // 부스트 속도
    public float Acceleration = 5f;     // 가속도
    public float Deceleration = 10f;    // 정지속도

    public bool IsBoost = false;        // 부스트 적용 유무

    public float JumpForce = 10f;       // 점프 힘
    public float JumpDuration = 0.8f;   // 점프상태 유지 시간
    public float JumpDelay = 0.2f;      // 점프할때까지의 딜레이
    public float JumpDuraition = 0.5f;  // 점프 유지 시간
    // public bool IsJumping = false;      // 현재 점프중인지 확인
    public CharDirection CurrentDirection = CharDirection.Right;

    // 전투 관련 스탯 (파츠 합산 결과)
    [Header("전투 관련 스탯")]
    [Tooltip("락온 가능 사거리 - 머리(센서) 파츠 등에서 주로 기여")]
    public float LockOnRange = 15f;

    [Tooltip("실제 추적 위치가 예측 위치를 따라가는 속도")]
    public float LockOnSpeed = 8f;

    [Tooltip("Manual 모드에서 예측 위치가 입력을 따라 이동하는 속도")]
    public float ManualAimSpeed = 10f;

    [Header("파츠 미장착 시 기본값 (기체 프레임 자체 성능)")]
    public PartStatBlock BaseStat = new PartStatBlock
    {
        MoveSpeed = 0f,
        BoostSpeed = 0f,
        Acceleration = 0f,
        Deceleration = 0f,
        JumpForce = 0f,
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
        MoveSpeed = total.MoveSpeed;
        BoostSpeed = total.BoostSpeed;
        Acceleration = total.Acceleration;
        Deceleration = total.Deceleration;

        JumpForce = total.JumpForce;
        JumpDuration = total.JumpDuration;
        JumpDelay = total.JumpDelay;

        LockOnRange = total.LockOnRange;
        LockOnSpeed = total.LockOnSpeed;
        ManualAimSpeed = total.ManualAimSpeed;
    }
}
