using UnityEngine;

/// <summary>
/// 적/파괴 가능 오브젝트 등에 붙여서 락온 대상으로 만드는 컴포넌트.
/// Rigidbody2D가 있으면 linearVelocity를 자동으로 리드 예측에 제공한다.
/// </summary>
public class TargetableEntity : MonoBehaviour, ITargetable
{
    [SerializeField] bool isLockable = true;

    [Tooltip("소속 진영. 같은 진영끼리는 서로 락온되지 않는다")]
    [SerializeField] Faction faction = Faction.None;

    Rigidbody2D _rigidbody;

    #region 외부 공개용 속성
    public Transform TargetTransform => transform;

    public Vector3 Position => transform.position;
    public Vector3 Velocity => _rigidbody != null ? (Vector3)_rigidbody.linearVelocity : Vector3.zero;

    public bool IsLockable => isLockable && gameObject.activeInHierarchy;

    public Faction Faction => faction;
    #endregion

    void Awake() => _rigidbody = GetComponent<Rigidbody2D>();

    /// <summary>사망/부활, 은신 진입/해제 등에서 호출해 락온 가능 여부를 갱신.</summary>
    public void SetLockable(bool value) => isLockable = value;

    /// <summary>진영 변경(예: 포획/전향 이벤트)이 필요할 때 호출.</summary>
    public void SetFaction(Faction value) => faction = value;
}
