using System.Collections.Generic;
using UnityEngine;
// 재장전 상태
public class WeaponReloadState : State<WeaponStateType, WeaponContext>
{
    private float _currentReloadTime;

    private WeaponContext _context;

    public WeaponReloadState(WeaponStateMachine machine,
        List<StateTransition<WeaponStateType, WeaponContext>> transitions)
    {
        _machine = machine;
        _transitions = transitions;

        _context = _machine.Context;
    }

    public override void StateEnter()
    {
        base.StateEnter();
        
        //// 재장전 확인용 회전
        //if (_context.WeaponAnchor != null)
        //    _context.WeaponAnchor.transform.rotation = Quaternion.Euler(0f, 0f, 60f);
        
        // 조준 관련 초기화
        _context.WeaponFlag.IsAiming = false;
        _context.WeaponFlag.IsAimComplete = false;
        _context.WeaponFlag.IsAimCanceled = true;

        _context.WeaponFlag.IsReloadComplete = false;
        _currentReloadTime = 0f;
    }

    public override void StateUpdate(float deltaTime)
    {
        //if (_context.WeaponFlag.IsReloadComplete) return;
        _currentReloadTime += deltaTime;

        // 현재 장전시간 >= 최대 장전시간
        if (_currentReloadTime >= _context.WeaponData.MaxReloadDuration)
            ReloadAmmo();

        base.StateUpdate(deltaTime);
    }

    void ReloadAmmo()
    {
        // 필요 탄약량
        int requiredAmmo = _context.WeaponData.MaxCapacity - _context.CurrentCapacity;
        // 필요탄약량 혹은 현재 탄약량중 더 적은것을 반환
        int reloadAmount = Mathf.Min(requiredAmmo, _context.AmmoRemaining);

        _context.CurrentCapacity += reloadAmount;
        _context.AmmoRemaining -= reloadAmount;

        bool dataNull = _context.WeaponData != null && _context.WeaponData.BulletModel != null;
        _context.WeaponFlag.CanFire = dataNull && _context.CurrentCapacity > 0; // 현재 탄약이 0 이상, 
        _context.WeaponFlag.IsReloadComplete = true;
    }

    public override void StateExit()
    {
        base.StateExit();
        _machine.Context.WeaponFlag.IsReloadComplete = false;
        _currentReloadTime = 0f;
    }
}