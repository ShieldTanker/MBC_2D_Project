using System.Collections.Generic;
using UnityEngine;

public class WeaponFireState : State<WeaponStateType, WeaponContext>
{
    private WeaponContext _context;

    private float _rateTime;

    public WeaponFireState(WeaponStateMachine machine, List<StateTransition<WeaponStateType, WeaponContext>> transitions)
    {
        _machine = machine;
        _transitions = transitions;

        _context = _machine.Context;
    }

    public override void StateEnter()
    {
        base.StateEnter();

        _rateTime = 0f;
    }

    public override void StateUpdate(float deltaTime)
    {
        if (_context.WeaponData == null)
        {
            _context.WeaponFlag.CanFire = false;
            base.StateUpdate(deltaTime);
            return;
        }

        _context.WeaponFlag.CanFire = _context.WeaponData.BulletModel != null
            && _context.CurrentCapacity > 0;

        _rateTime += deltaTime;

        TryFire();

        base.StateUpdate(deltaTime);
    }

    public override void StateExit()
    {
        base.StateExit();
        _rateTime = 0f;
    }

    private void TryFire()
    {
        WeaponInput input = _context.WeaponInput;

        // 버튼도 안 누르고 있고 새로운 공격 입력도 없으면 발사X
        if (!input.AttackPressed && !_context.WeaponFlag.AttackSequenceStarted) return;
        if (!_context.WeaponFlag.CanFire) return;

        float fireRate = GetFireRate();
        
        // 새로운 공격 입력
        // 단발 / 연발 모두 첫 발은 AttackSequenceStarted를 기준으로 발사합니다.
        if (_context.WeaponFlag.AttackSequenceStarted)
        {
            if (_rateTime < fireRate)
                return;
            _context.WeaponFlag.AttackSequenceStarted = false;
            Fire();

            return;
        }

        // 새로운 공격 입력이 아니고 버튼을 계속 누르고 있는 경우.
        // FullAuto만 계속 발사합니다.
        if (!input.AttackPressed)
            return;

        if (_context.WeaponData.FireMode == WeaponFireMode.FullAuto)
        {
            if (_rateTime >= fireRate)
            {
                Fire();
            }
        }
    }

    private float GetFireRate()
    {
        if (_context.WeaponData == null)
            return float.MaxValue;

        if (_context.WeaponData.FireRate <= 0f)
            return float.MaxValue;

        return 60f / _context.WeaponData.FireRate;
    }

    private void Fire()
    {
        if (!_context.WeaponFlag.CanFire) return;
        if (_context.WeaponData.BulletModel == null) return;
        if (_context.FirePosition == null) return;

        GameObject.Instantiate(
            _context.WeaponData.BulletModel, _context.FirePosition.position, _context.FirePosition.rotation);

        _context.CurrentCapacity--;

        // 실제 발사했으므로 타이머 초기화
        _context.TimeSinceLastFire = 0f;

        _rateTime = 0f;

        _context.WeaponFlag.CanFire = _context.CurrentCapacity > 0;

        FireRecoilTest();
    }

    private void FireRecoilTest()
    {
        if (_context.WeaponData.RecoilData == null)
            return;

        float kickBack = _context.WeaponData.RecoilData.KickBack;
        float kickUp = _context.WeaponData.RecoilData.KickUp;

        float min = Mathf.Min(kickBack, kickUp);
        float max = Mathf.Max(kickBack, kickUp);

        float recoilX = Random.Range(min, max);

        Vector2 recoil = Vector2.right * recoilX;

        _context.WeaponPos.localPosition -= new Vector3(recoil.x, 0f, 0f);
    }
}