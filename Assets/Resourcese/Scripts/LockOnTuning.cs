using UnityEngine;

/// <summary>
/// 락온의 "기술적" 설정값. range/lockOnSpeed/manualAimSpeed는 파츠 스탯 합산 결과인
/// AgentStat에서 가져오므로 여기에는 없다 - 이 SO는 마스크/LOS/화면판정 등 파츠와
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

    [Header("리드 예측 (Lead Prediction)")]
    [Tooltip("대상의 이동 속도를 반영해 앞쪽 지점을 예측 위치로 사용할지")]
    public bool useLeadPrediction = false;
    [Tooltip("몇 초 뒤의 위치를 미리 노릴지")]
    public float leadTime = 0.2f;

    [Header("시야 확인 (Line of Sight)")]
    public bool requireLineOfSight = true;
    public LayerMask obstacleMask;

    [Header("화면 범위 (2D 횡스크롤)")]
    [Tooltip("카메라 화면 밖으로 나간 대상은 후보에서 제외할지")]
    public bool requireOnScreen = true;
    [Range(0f, 0.4f)]
    public float screenEdgeMargin = 0.05f;

    [Header("방향 입력 타겟 전환")]
    [Tooltip("이 크기 이상 '누적' 입력이 쌓여야 전환된다 (프레임 단위 순간값이 아니라 누적값 기준). 스틱/마우스를 의도적으로 휙 움직여야 넘도록 조절")]
    public float targetSwitchInputThreshold = 3f;
    
    [Tooltip("입력을 안 주는 동안 누적치가 초당 이만큼씩 0으로 줄어든다. 값이 작으면 미세한 떨림이 여러 프레임에 걸쳐 쌓여 오작동할 수 있다")]
    public float aimAccumDecayPerSecond = 8f;
    
    [Tooltip("전환 직후 다음 전환까지의 최소 대기시간(초) - 누적 리셋과 별개의 2차 안전장치")]
    public float targetSwitchCooldown = 0.15f;
    
    [Tooltip("입력 방향과 후보 방향 사이 허용 최대 각도(도). 이 각도를 벗어난 후보는 전환 대상에서 제외")]
    [Range(1f, 179f)]
    public float targetSwitchMaxAngle = 70f;

    [Header("현재 타겟 기준 각도 제한")]
    [Tooltip("현재 락온 중인 타겟이 있을 때, 그 타겟을 바라보는 방향에서 일정 각도 밖의 후보를 후보군 자체에서 제외할지")]
    public bool limitAngleFromCurrentTarget = false;
    
    [Tooltip("현재 타겟 방향 기준 허용 최대 각도(도). limitAngleFromCurrentTarget이 켜져 있을 때만 사용")]
    [Range(1f, 179f)]
    public float maxAngleFromCurrentTarget = 90f;
}
