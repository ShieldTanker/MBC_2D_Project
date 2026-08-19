/// <summary>
/// 소속 진영. Eden 세계관의 4개 국가 + 팀 개념이 없는 오브젝트용 Neutral로 구성.
/// 필요한 세력이 늘어나면 여기에 값만 추가하면 된다 (하위 조직까지 구분하고 싶다면
/// Faction은 국가 단위로 유지하고 별도 SubFaction enum을 추가하는 걸 추천).
/// </summary>
public enum Faction
{
    Neutral = 0,   // 팀 개념이 없는 파괴물 등 - 항상 락온 가능
    Haven,
    Arcadia,
    Valhall,
    Orion,
}
