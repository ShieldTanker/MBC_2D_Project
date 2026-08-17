using UnityEngine;

public class AgentStat : MonoBehaviour
{
    // 이동관련 스탯
    public float MoveSpeed = 20f;       // 일반 속도
    public float BoostSpeed = 60f;      // 부스트 속도
    public float Acceleration = 5f;     // 가속도
    public float Deceleration = 10f;    // 정지속도

    public bool IsBoost = false;        // 부스트 적용 유무

    public float JumpForce = 40f;       // 점프 힘
    public float JumpDuration = 0.8f;   // 점프상태 유지 시간
    public float JumpDelay = 0.2f;      // 점프할때까지의 딜레이
    public float JumpDuraition = 0.5f;  // 점프 유지 시간
    public bool IsJumping = false;      // 현재 점프중인지 확인

    // 전투 관련 스탯
}
