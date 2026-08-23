using UnityEngine;

/// <summary>
/// 락온의 기술적 설정값. range, lockOnSpeed, manualAimSpeed는 파츠 스탯 합산 결과인
/// AgentStat에서 가져오므로 여기에는 없다 - 이 SO는 마스크, LOS, 화면판정 등 파츠와
/// 무관한 시스템 설정만 담는다.
/// </summary>
[CreateAssetMenu(menuName = "LockOn/Lock On Tuning", fileName = "NewLockOnTuning")]
public class LockOnTuning : ScriptableObject
{
    [Header("탐지")]
    public LayerMask targetMask;
    [Tooltip("한 프레임에 고려할 최대 후보 수 (성능 상한선)")]
    public int maxCandidates = 16;
    [Tooltip("같은 TeamId를 가진 대상은 후보에서 제외할지 (자기 자신 제외와는 별개)")]
    public bool excludeSameTeam = true;

    [Header("장애물 시야 확인 (Line of Sight)")]
    public bool requireLineOfSight = true;
    public LayerMask obstacleMask;

    [Header("화면 범위 (2D 횡스크롤)")]
    [Tooltip("카메라 화면 밖으로 나간 대상은 후보에서 제외할지")]
    public bool requireOnScreen = true;
    [Range(0f, 0.4f)]
    public float screenEdgeMargin = 0.05f;

    [Header("현재 타겟 기준 각도 제한")]
    [Tooltip("현재 락온 중인 타겟이 있을 때, 그 타겟을 바라보는 방향에서 일정 각도 밖의 후보를 후보군 자체에서 제외할지")]
    public bool limitAngleFromCurrentTarget = false;
    
    [Tooltip("현재 타겟 방향 기준 허용 최대 각도(도). limitAngleFromCurrentTarget이 켜져 있을 때만 사용")]
    [Range(1f, 179f)]
    public float maxAngleFromCurrentTarget = 90f;
}
