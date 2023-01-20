using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class ChaseState : EnemyState
{
    private NavMeshAgent _agent;
    private Transform _peacefulConstruction;

    private void Awake()
    {
        _agent = GetComponent<NavMeshAgent>();
    }

    private void OnEnable()
    {
        _agent.enabled = true;
        Animator.SetTrigger("Run1");
    }

    private void OnDisable()
    {
        _agent.enabled = false;
    }

    private void Update()
    {
        _agent.SetDestination(_peacefulConstruction.position);
    }

    private Transform FindTarget()
    {
        PeacefulConstruction[] constructions = FindObjectsOfType<PeacefulConstruction>();
        float shortestDistance = Mathf.Infinity;
        PeacefulConstruction nearestEnemy = null;

        foreach (var construction in constructions)
        {
            float distanceToConstruction = Vector3.Distance(transform.position, construction.transform.position);

            if (distanceToConstruction < shortestDistance)
            {
                shortestDistance = distanceToConstruction;
                nearestEnemy = construction;
            }
        }

        if (nearestEnemy != null)
        {
            return _peacefulConstruction = nearestEnemy.transform;

        }
        else
        {
            return _peacefulConstruction = null;
        }
    }
}
