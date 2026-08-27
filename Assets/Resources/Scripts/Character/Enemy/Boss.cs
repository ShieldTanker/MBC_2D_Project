using UnityEngine;

[RequireComponent(typeof(Health))]
[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(AgentStat))]
public class Boss : MonoBehaviour
{
    Health _health;
    AgentStat _stat;
    Rigidbody2D _rb2D;

    public LockonController _lockon;
    public float StopDis = 20f;
    public float _speed = 20f;

    private void Awake()
    {
        _stat = GetComponent<AgentStat>();
        _rb2D = GetComponent<Rigidbody2D>();
        _health = GetComponent<Health>();
        _lockon = GetComponentInChildren<LockonController>();
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
            Vector3 vel = _rb2D.linearVelocity;
            dir.Normalize();
            vel.x = dir.x *_speed;
            _rb2D.linearVelocity = vel;
        }
        else
        {
            _rb2D.linearVelocity = Vector2.zero;
        }
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