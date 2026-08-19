using UnityEngine;

/// <summary>
/// 머리/몸통/팔/다리 파츠 데이터의 공통 베이스.
/// WeaponData가 Model + 스탯을 함께 들고 있는 것과 동일한 패턴.
/// 이 데이터를 교체하면 LoadOut이 모델 스왑과 스탯 재계산을 동시에 처리한다.
/// </summary>
public abstract class PartDataBase : ScriptableObject
{
    [Header("공통 정보")]
    public string PartName;

    [Tooltip("장착 시 해당 부위 앵커에 생성되는 모델 프리팹")]
    public GameObject ModelPrefab;

    [Header("스탯 보정치 (합산되어 AgentStat에 반영됨)")]
    public PartStatBlock Stats;
}
