using UnityEngine;
using UnityEngine.Rendering;

public class WeaponIdleStateLogic : StateLogic<WeaponContext>
{
    public WeaponIdleStateLogic(WeaponContext context) : base(context) { }

    public override void UpdateStateLogic(float deltaTime)
    {
        _context.WeaponAnchorPos.rotation = Quaternion.Euler(new Vector3(0,0,300f));
    }

    public override void Clear() { }
}

public class WeaponAimStateLogic : StateLogic<WeaponContext>
{
    private float _aimTime;

    public WeaponAimStateLogic(WeaponContext context)
        : base(context) { }

    public override void UpdateStateLogic(float deltaTime)
    {
        if (!_context.WeaponFlag.IsAimComplete)
        {
            _aimTime += deltaTime;

            _context.WeaponFlag.IsAimComplete = _aimTime >= _context.WeaponData.MaxAimTime;
        }

        if (_context.WeaponInput.AttackPressed ||
            _context.WeaponInput.AttackHold ||
            _context.WeaponInput.ReloadPressed)
        {
            _context.AimIdleTimer = 0f;
        }
        else
        {
            _context.AimIdleTimer += deltaTime;
        }
    }

    public override void Clear()
    {
        _aimTime = 0f;
        _context.AimIdleTimer = 0f;
    }
}

public class WeaponFireStateLogic : StateLogic<WeaponContext>
{
    public WeaponFireStateLogic(WeaponContext context)
        : base(context) { }

    public override void UpdateStateLogic(float deltaTime)
    {
        _context.WeaponFlag.CanFire =
            _context.WeaponData != null && _context.WeaponData.BulletModel != null &&
            _context.CurrentCapacity > 0 ; // 현재 탄약이 0 이상, 

        _context.WeaponFlag.IsFireComplete = true;
    }

    public override void Clear() { }
}

public class WeaponRecoilStateLogic : StateLogic<WeaponContext>
{
    private float _elapsedTime;

    public WeaponRecoilStateLogic(WeaponContext context)
        : base(context) { }

    public override void UpdateStateLogic(float deltaTime)
    {
        RecoilData recoilData = _context.WeaponData.RecoilData;

        if (recoilData == null)
        {
            _context.WeaponFlag.IsRecoilComplete = true;
            return;
        }

        _elapsedTime += deltaTime;

        float recoilDuration = Mathf.Max(recoilData.RecoilDuration, 0.0001f);
        float normalizedTime = Mathf.Clamp01(_elapsedTime / recoilDuration);

        Vector3 basePosition = _context.WeaponBaseLocalPos;
        Quaternion baseRotation = _context.WeaponBaseLocalRot;

        Vector3 recoilPosition = basePosition;
        Quaternion recoilRotation = baseRotation;

        // 반동 방향은 무기의 로컬 -X 방향
        recoilPosition.x -= recoilData.KickBack;

        // Pitch 반동
        recoilRotation *= Quaternion.Euler(0f, 0f, recoilData.KickPitchAngle);

        if (normalizedTime < 0.5f)
        {
            float kickTime = normalizedTime / 0.5f;
            float kickSpeed = Mathf.Max(recoilData.KickSpeed, 0.0001f);
            float kickProgress = Mathf.Clamp01(kickTime * kickSpeed);

            _context.WeaponPos.localPosition = Vector3.Lerp(basePosition, recoilPosition, kickProgress);
            _context.WeaponPos.localRotation = Quaternion.Lerp(baseRotation, recoilRotation, kickProgress);
        }
        else
        {
            float recoveryTime = (normalizedTime - 0.5f) / 0.5f;
            float recoverySpeed = Mathf.Max(recoilData.RecoverySpeed, 0.0001f);
            float recoveryProgress = Mathf.Clamp01(recoveryTime * recoverySpeed);

            _context.WeaponPos.localPosition = Vector3.Lerp(recoilPosition, basePosition, recoveryProgress);
            _context.WeaponPos.localRotation = Quaternion.Lerp(recoilRotation, baseRotation, recoveryProgress);
        }

        if (_elapsedTime >= recoilDuration)
        {
            _context.WeaponPos.localPosition = basePosition;
            _context.WeaponPos.localRotation = baseRotation;

            _context.WeaponFlag.IsRecoilComplete = true;
        }
    }

    public override void Clear()
    {
        _elapsedTime = 0f;
    }
}

public class WeaponReloadStateLogic : StateLogic<WeaponContext>
{
    private float _currentReloadTime;

    public WeaponReloadStateLogic(WeaponContext context)
        : base(context) { }

    public override void UpdateStateLogic(float deltaTime)
    {
        if (_context.WeaponFlag.IsReloadComplete) return;
        _currentReloadTime += deltaTime;

        // 현재 장전시간 < 최대 장전시간 = return
        if (_currentReloadTime < _context.WeaponData.MaxReloadDuration) return;

        // 필요 탄약량
        int requiredAmmo = _context.WeaponData.MaxCapacity - _context.CurrentCapacity;
        // 필요탄약량 혹은 현재 탄약량중 더 적은것을 반환
        int reloadAmount = Mathf.Min(requiredAmmo, _context.AmmoRemaining);

        _context.CurrentCapacity += reloadAmount;
        _context.AmmoRemaining -= reloadAmount;

        _context.WeaponFlag.CanFire =
            _context.WeaponData != null && _context.WeaponData.BulletModel != null &&
            _context.CurrentCapacity > 0; // 현재 탄약이 0 이상, 
        _context.WeaponFlag.IsReloadComplete = true;
    }

    public override void Clear()
    {
        _currentReloadTime = 0f;
    }
}
