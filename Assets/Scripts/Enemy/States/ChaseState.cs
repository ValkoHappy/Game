using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent), typeof(FoundBuildings))]
public class ChaseState : EnemyState
{
    private FoundBuildings _foundBuildings;
    private NavMeshAgent _agent;
    private PeacefulConstruction _targetConstruction;
    private const string _run = "Run";

    private void Awake()
    {
        _foundBuildings = GetComponent<FoundBuildings>();
        _agent = GetComponent<NavMeshAgent>();
    }

    private void OnEnable()
    {
        Animator.SetFloat(_run, 0.01f);
        _agent.enabled = true;
    }

    private void OnDisable()
    {
        Animator.SetFloat(_run, 0);
        _agent.enabled = false;
    }

    private void Update()
    {
        _targetConstruction = _foundBuildings.TargetConstruction;
        if (_targetConstruction != null)
        {
            _agent.SetDestination(_targetConstruction.transform.position);
        }
    }
}
