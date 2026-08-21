using UnityEngine;
using UnityEngine.UIElements;

public abstract class WeaponAimAnchor : MonoBehaviour
{
    public Transform IKTarget;
    public Transform AimTarget { get; set; }

    [Space]
    public float AimSpeed = 10f;
    public bool CanRotate = true;
    float angle = 0f;
    public bool useGizmo;

    public Vector3 IdleRotate = new Vector3(0, 0, 300);
    public Vector3 currRot;
    public Vector3 AimDirection { get; private set; }

    public WeaponBase Weapon { get; private set; }
    private WeaponContext WeaponContext;

    private void Awake()
    {
        transform.rotation = Quaternion.Euler(IdleRotate);
        Weapon = GetComponentInChildren<WeaponBase>();
        WeaponContext = Weapon.Context;
    }

    private void OnEnable()
    {
        Weapon.OnIdleStart += OnIdleStart;

        Weapon.OnReloadStart += OnReloadStart;
        Weapon.OnReloadExit += OnReloadExit;
        BaseOnEnable();
    }

    protected virtual void BaseOnEnable() { }
    protected virtual void BaseOnDisable() { }

    private void OnDisable()
    {
        Weapon.OnIdleStart -= OnIdleStart;

        Weapon.OnReloadStart -= OnReloadStart;
        Weapon.OnReloadExit -= OnReloadExit;
        BaseOnDisable();
    }

    void LateUpdate()
    {
        currRot = transform.rotation.eulerAngles;
        IKAiming();
        Aiming();
    }

    #region 이벤트 등록
    void OnIdleStart() { CanRotate = true; }

    void OnReloadStart() { CanRotate = false; }

    void OnReloadExit() { CanRotate = true; }
    #endregion

    private void IKAiming()
    {
        if (Weapon == null || IKTarget == null)
            return;

        IKTarget.position = Weapon.transform.position;
        IKTarget.rotation = Weapon.transform.rotation;
    }

    private void Aiming()
    {
        if (!CanRotate)
            return;
        if (WeaponContext.WeaponFlag.IsAiming)
        {
            Vector3 dir = AimTarget.position - transform.position;
            WeaponAim(dir);
        }
        else { IdleAim(); }
    }

    private void WeaponAim(Vector3 dir)
    {
        AimDirection = dir.normalized; // 실제 타겟 방향 — 총알/레이캐스트는 이걸 사용

        // 부모(UpperBody, Spine, Hip, Model, 캐릭터 반전까지 전부) 기준 로컬 방향으로 변환
        Vector3 localDir = transform.parent.InverseTransformDirection(dir);

        float localAngle = Mathf.Atan2(localDir.y, localDir.x) * Mathf.Rad2Deg;
        angle = Mathf.LerpAngle(transform.localEulerAngles.z, localAngle, AimSpeed * Time.deltaTime);
        transform.localRotation = Quaternion.Euler(0, 0, angle);
    }

    private void IdleAim()
    {
        angle = Mathf.LerpAngle(transform.localEulerAngles.z, IdleRotate.z, AimSpeed * Time.deltaTime);
        transform.localRotation = Quaternion.Euler(0, 0, angle);
    }

    private void OnDrawGizmos()
    {
        if(AimTarget != null && useGizmo)
        Gizmos.DrawLine(transform.position, AimTarget.position);
    }
}