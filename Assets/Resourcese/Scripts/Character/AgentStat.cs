using UnityEngine;

public class AgentStat : MonoBehaviour
{
    // 이동관련 스탯
    public float MoveSpeed = 20f;       // 일반 속도
    public float BoostSpeed = 60f;      // 부스트 속도
    public float Acceleration = 5f;     // 가속도
    public float Deceleration = 10f;    // 정지속도

    public bool IsBoost = false;        // 부스트 유무

    public float JumpForce = 10f;       // 점프 힘


    // 전투 관련 스탯
}
