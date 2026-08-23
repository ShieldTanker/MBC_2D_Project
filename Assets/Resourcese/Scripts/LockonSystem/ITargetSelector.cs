using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 범위 내 후보를 찾아 정렬된 리스트로 반환하는 전략 인터페이스.
/// range는 AgentStat에서 계산된 값을 매 프레임 LockOnController가 넘겨준다.
/// self는 조회를 요청한 주체의 루트 Transform - 같은 레이어라도 자기 자신(과 자기 자식 콜라이더)은
/// 후보에서 제외하기 위해 넘긴다.
/// selfFaction은 자기 진영 - tuning.excludeSameTeam이 켜져 있으면 같은 진영도 제외한다
/// (단, Faction.Neutral은 예외 - 항상 제외되지 않는다).
/// currentTarget은 현재 락온 중인 대상(없으면 null) - tuning.limitAngleFromCurrentTarget이 켜져 있으면
/// 이 대상을 바라보는 방향 기준 각도 밖의 후보를 걸러내는 데 쓰인다.
/// </summary>
public interface ITargetSelector
{
    List<ITargetable> GetTargets(Transform origin, float range, LockOnTuning tuning, Camera viewCamera, Faction selfFaction, ITargetable currentTarget);
}