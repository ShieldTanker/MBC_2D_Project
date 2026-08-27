using UnityEngine;

[RequireComponent(typeof(Health))]
[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(AgentStat))]
public class Boss : MonoBehaviour
{
    Health _health;
    public LockonController _lockon;
    AgentStat _stat;
    public float StopDis = 20f;
    Rigidbody2D rigidbody2D;
    public float _speed = 20f;

    private void Awake()
    {
        _stat = GetComponent<AgentStat>();
        rigidbody2D = GetComponent<Rigidbody2D>();
        _health = GetComponent<Health>();
        _lockon = GetComponentInChildren<LockonController>();
    }

    private void Start()
    {
        _lockon.Init(_stat, new DistanceTargetSelector());
        _health.MaxHealth = 2000;
        _health.Init();
    }

    private void FixedUpdate()
    {
        if (_lockon.CurrentTarget == null)
        {
            return;
        }   
        Vector2 dir = _lockon.CurrentTargetTransform.position - transform.position;
        float dis = dir.sqrMagnitude;
        float chaseDis = _stat.LockonRange;

        if (dis > StopDis * StopDis && dis < chaseDis * chaseDis)
        {
            Vector3 vel = rigidbody2D.linearVelocity;
            dir.Normalize();
            vel.x = dir.x *_speed;
            rigidbody2D.linearVelocity = vel;
        }
        else
        {
            rigidbody2D.linearVelocity = Vector2.zero;
        }
    }

    private void OnEnable()
    {
        _health.OnDamageAction += OnHit;
        _health.OnDieAction += OnDie;
    }

    private void OnDisable()
    {
        _health.OnDamageAction -= OnHit;
        _health.OnDieAction -= OnDie;
    }

    void OnHit(DamageInfo info)
    {
        BossUIEventBus.Publish(BossBattleUIEventType.BossHpSet, _health);
    }

    void OnDie(DamageInfo info)
    {
        BossUIEventBus.Publish(BossBattleUIEventType.BossDie, _health);
        Destroy(gameObject, 1f);
    }
}
