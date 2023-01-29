using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent), typeof(FoundBuildings))]
public class ChaseState : EnemyState
{
    private FoundBuildings _buildings;
    private NavMeshAgent _agent;
    [SerializeField] private PeacefulConstruction _targetConstruction;

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
        _buildings.SortEnemies();
        _targetConstruction = _buildings.TargetConstruction;
        if (_targetConstruction != null)
        {
            _agent.SetDestination(_targetConstruction.transform.position);
        }
    }
}
