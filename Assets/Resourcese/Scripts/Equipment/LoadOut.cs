using UnityEngine;

/// <summary>
/// 머리/몸통/팔/다리 파츠 장착을 관리.
/// 파츠 데이터를 교체하면 (1) 해당 부위 모델을 스왑하고 (2) 4개 파츠 스탯을 합산해
/// AgentStat에 반영한다. Weapon이 WeaponData 교체 시 SetWeapon()을 호출하는 것과 동일한 패턴.
/// </summary>
public class LoadOut : MonoBehaviour
{
    [Header("장착 파츠 데이터")]
    [SerializeField] private HeadPartData _head;
    [SerializeField] private BodyPartData _body;
    [SerializeField] private ArmsPartData _arms;
    [SerializeField] private LegsPartData _legs;

    [Header("모델 부착 위치 (ModelController의 본/앵커 참조)")]
    [SerializeField] private Transform _headAnchor;
    [SerializeField] private Transform _bodyAnchor;
    [SerializeField] private Transform _armsAnchor;
    [SerializeField] private Transform _legsAnchor;

    private GameObject _headModel;
    private GameObject _bodyModel;
    private GameObject _armsModel;
    private GameObject _legsModel;

    private AgentStat _stat;

    public HeadPartData Head => _head;
    public BodyPartData Body => _body;
    public ArmsPartData Arms => _arms;
    public LegsPartData Legs => _legs;

    private void Awake()
    {
        _stat = GetComponent<AgentStat>();
    }

    private void Start()
    {
        // 인스펙터에서 미리 꽂아둔 초기 파츠들을 실제로 장착 처리(모델 생성 + 스탯 반영)
        SetHead(_head);
        SetBody(_body);
        SetArms(_arms);
        SetLegs(_legs);
    }

    #region 파츠 교체 (인벤토리 UI, AI 셋업 등에서 호출)

    public void SetHead(HeadPartData data)
    {
        _head = data;
        SwapModel(ref _headModel, data, _headAnchor);
        RecalculateStats();
    }

    public void SetBody(BodyPartData data)
    {
        _body = data;
        SwapModel(ref _bodyModel, data, _bodyAnchor);
        RecalculateStats();
    }

    public void SetArms(ArmsPartData data)
    {
        _arms = data;
        SwapModel(ref _armsModel, data, _armsAnchor);
        RecalculateStats();
    }

    public void SetLegs(LegsPartData data)
    {
        _legs = data;
        SwapModel(ref _legsModel, data, _legsAnchor);
        RecalculateStats();
    }

    #endregion

    private void SwapModel(ref GameObject current, PartDataBase data, Transform anchor)
    {
        if (current != null)
            Destroy(current);

        if (data == null || data.ModelPrefab == null || anchor == null)
            return;

        current = Instantiate(data.ModelPrefab, anchor);
        current.transform.localPosition = Vector3.zero;
        current.transform.localRotation = Quaternion.identity;
    }

    /// <summary>기본값 + 4개 파츠 스탯을 합산해 AgentStat에 반영.</summary>
    public void RecalculateStats()
    {
        if (_stat == null) return;

        PartStatBlock total = _stat.BaseStat;

        if (_head != null) total += _head.Stats;
        if (_body != null) total += _body.Stats;
        if (_arms != null) total += _arms.Stats;
        if (_legs != null) total += _legs.Stats;

        _stat.ApplyStatBlock(total);
    }
}
