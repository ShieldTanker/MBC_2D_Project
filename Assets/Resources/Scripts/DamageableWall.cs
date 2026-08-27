using UnityEngine;

public class DamageableWall : MonoBehaviour
{
    Health _health;
    private void Awake()
    {
        _health = GetComponent<Health>();
    }
        
    private void Start()
    {
        _health.OnDieAction += (_) =>Destroy(gameObject);
    }
}
