using UnityEngine;

/// <summary>
/// 락온 대상이 될 수 있는 모든 객체가 구현해야 하는 인터페이스.
/// </summary>
public interface ITargetable
{
    Transform TargetTransform { get; }
    Vector3 Position { get; }

    /// <summary>리드 예측(Lead Prediction)에 사용되는 대상의 현재 속도.</summary>
    Vector3 Velocity { get; }

    /// <summary>사망, 무적, 은신 등으로 인해 현재 락온이 불가능한 상태인지.</summary>
    bool IsLockable { get; }

    /// <summary>
    /// 소속 진영. 같은 값이면 같은 팀으로 취급되어 락온 대상에서 제외된다.
    /// Faction.Neutral은 "팀 개념 없음"을 뜻하는 특수값 - Neutral끼리는 서로 제외되지 않고,
    /// 다른 진영에게 항상 락온 가능한 대상으로 취급된다.
    /// </summary>
    Faction Faction { get; }
}
