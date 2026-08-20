using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 범위 내 ITargetable을 Physics2D.OverlapCircle(ContactFilter2D, List)로 찾고,
/// 시야(LOS) / 화면 범위 조건으로 필터링한 뒤 거리순으로 정렬해 반환한다.
/// Unity 6부터 OverlapCircleNonAlloc(배열 버전)은 tentatively deprecated되어
/// ContactFilter2D + List&lt;Collider2D&gt; 오버로드를 사용한다 (내부에서 알아서 리스트를 비우고 채움).
/// </summary>
public class DistanceTargetSelector : ITargetSelector
{
    readonly List<Collider2D> _hitBuffer = new List<Collider2D>(32);
    readonly List<ITargetable> _results = new List<ITargetable>();

    public List<ITargetable> GetCandidates(Transform origin, float range, LockOnTuning tuning, Camera viewCamera, Faction selfFaction, ITargetable currentTarget)
    {
        _results.Clear();

        ContactFilter2D filter = new ContactFilter2D();
        filter.SetLayerMask(tuning.targetMask);
        filter.useTriggers = true; // 락온 대상 콜라이더가 트리거여도 감지되도록

        int count = Physics2D.OverlapCircle(origin.position, range, filter, _hitBuffer);

        // 현재 타겟을 바라보는 기준 방향 - 루프 밖에서 한 번만 계산 (대상이 없거나 자기 위치와 겹치면 필터 미적용)
        bool useCurrentTargetAngleFilter = false;
        Vector2 currentTargetDir = Vector2.zero;
        if (tuning.limitAngleFromCurrentTarget && currentTarget != null)
        {
            Vector2 toCurrent = (Vector2)currentTarget.Position - (Vector2)origin.position;
            if (toCurrent.sqrMagnitude > 0.0001f)
            {
                currentTargetDir = toCurrent.normalized;
                useCurrentTargetAngleFilter = true;
            }
        }

        for (int i = 0; i < count; i++)
        {
            var col = _hitBuffer[i];
            if (col == null) continue;

            // 같은 레이어를 쓰더라도 자기 자신(과 자기 자식 콜라이더 - 무기, 파츠 등)은 후보에서 제외
            if (origin != null && col.transform.root == origin.root)
                continue;

            var targetable = col.GetComponent<ITargetable>();
            if (targetable == null || !targetable.IsLockable) continue;

            // 같은 진영은 후보에서 제외 (아군 오인사격 방지). Neutral끼리는 예외로 제외하지 않는다.
            if (tuning.excludeSameTeam && selfFaction != Faction.None && targetable.Faction == selfFaction)
                continue;

            if (tuning.requireLineOfSight && !HasLineOfSight(origin.position, targetable.Position, tuning.obstacleMask))
                continue;

            if (tuning.requireOnScreen && viewCamera != null &&
                !IsOnScreen(viewCamera, targetable.Position, tuning.screenEdgeMargin))
                continue;

            // 현재 타겟을 바라보는 방향에서 일정 각도 밖의 후보는 제외 (자기 자신과 같은 위치라 각도가 없으면 통과시킴)
            if (useCurrentTargetAngleFilter && targetable != currentTarget)
            {
                Vector2 toCandidate = (Vector2)targetable.Position - (Vector2)origin.position;
                if (toCandidate.sqrMagnitude > 0.0001f)
                {
                    float angleFromCurrent = Vector2.Angle(currentTargetDir, toCandidate.normalized);
                    if (angleFromCurrent > tuning.maxAngleFromCurrentTarget)
                        continue;
                }
            }

            _results.Add(targetable);
        }

        _results.Sort((a, b) =>
        {
            float da = (a.Position - origin.position).sqrMagnitude;
            float db = (b.Position - origin.position).sqrMagnitude;
            return da.CompareTo(db);
        });

        if (_results.Count > tuning.maxCandidates)
            _results.RemoveRange(tuning.maxCandidates, _results.Count - tuning.maxCandidates);

        return _results;
    }

    static bool HasLineOfSight(Vector3 origin, Vector3 targetPos, LayerMask obstacleMask)
    {
        Vector3 dir = targetPos - origin;
        float dist = dir.magnitude;
        if (dist <= 0.001f) return true;

        var hit = Physics2D.Raycast(origin, dir / dist, dist, obstacleMask);
        return hit.collider == null;
    }

    static bool IsOnScreen(Camera cam, Vector3 worldPos, float margin)
    {
        Vector3 vp = cam.WorldToViewportPoint(worldPos);
        if (vp.z < 0f) return false;
        return vp.x > margin && vp.x < 1f - margin && vp.y > margin && vp.y < 1f - margin;
    }
}
