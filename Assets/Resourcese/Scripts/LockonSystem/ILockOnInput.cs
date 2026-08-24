using System;

/// <summary>
/// IWeaponInput과 동일한 패턴 - 락온 관련 이산적(discrete) 입력을 Action으로 노출.
/// 연속 입력(수동 조준 델타)은 새로 만들지 않고 기존 ILookInput.LookAction을 그대로 재사용한다.
/// </summary>
public interface ILockonInput
{
    public Action LockOnManualToggleAction { get; set; }
    public Action LockOnNextTargetAction { get; set; }
    public Action LockOnPrevTargetAction { get; set; }
}
