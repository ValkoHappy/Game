using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent), typeof(FoundBuildings))]
public class ChaseState : EnemyState
{
    private FoundBuildings _buildings;
    private NavMeshAgent _agent;
    private PeacefulConstruction _targetConstruction;

    private void Awake()
    {
        _buildings = GetComponent<FoundBuildings>();
        _agent = GetComponent<NavMeshAgent>();
    }

    private void OnEnable()
    {
        _agent.enabled = true;
    }

    private void OnDisable()
    {
        _agent.enabled = false;
    }

    private void Update()
    {
        _targetConstruction = _buildings.TargetConstruction;
        if (_targetConstruction != null)
        {
            _agent.SetDestination(_targetConstruction.transform.position);
            Animator.SetTrigger("Run 0");
        }
    }
}
