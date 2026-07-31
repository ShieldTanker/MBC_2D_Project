using UnityEngine;

public class Player : MonoBehaviour
{
    private AgentStateMachine _agent;
    private AgentContext _context;

    private void Awake() { }

    void Start()
    {
        // TODO : _context 초기화할것
        _agent = new AgentStateMachine(_context);
    }

    void Update()
    {
        _agent?.Update(Time.deltaTime);
    }
}
