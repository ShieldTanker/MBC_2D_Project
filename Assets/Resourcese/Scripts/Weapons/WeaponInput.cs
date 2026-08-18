public class WeaponInput
{
    /// <summary>
    /// 현재 공격 버튼을 누르고 있는지 여부.
    /// 버튼을 누르고 있는 동안 true.
    /// </summary>
    public bool AttackPressed { get; set; }

    /// <summary>
    /// 공격 버튼을 누르고 있는 동안의 Hold 상태.
    /// </summary>
    public bool AttackHold { get; set; }

    /// <summary>
    /// 재장전 입력.
    /// </summary>
    public bool InteractionPressed { get; set; }

    public void ClearAll()
    {
        AttackPressed = false;
        AttackHold = false;
        InteractionPressed = false;
    }
}
