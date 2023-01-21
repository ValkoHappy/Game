using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class ChaseState : EnemyState
{
    private NavMeshAgent _agent;
    private Transform _peacefulConstruction;
    private Spawner _spawner;
    private List<PeacefulConstruction> constructions;

    private void Awake()
    {
        _agent = GetComponent<NavMeshAgent>();
        constructions = new List<PeacefulConstruction>();
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
        if (_peacefulConstruction != null)
        {
            _agent.SetDestination(_peacefulConstruction.position);
        }

    }

    //private Transform FindTarget()
    //{
    //    float shortestDistance = Mathf.Infinity;
    //    PeacefulConstruction nearestEnemy = null;

    //    foreach (var construction in _spawner.Constructions)
    //    {
    //        float distanceToConstruction = Vector3.Distance(transform.position, construction.transform.position);

    //        if (distanceToConstruction < shortestDistance)
    //        {
    //            shortestDistance = distanceToConstruction;
    //            nearestEnemy = construction;
    //        }
    //    }

    //    if (nearestEnemy != null)
    //    {
    //        return _peacefulConstruction = nearestEnemy.transform;

    //    }
    //    else
    //    {
    //        return _peacefulConstruction = null;
    //    }
    //}

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.TryGetComponent(out PeacefulConstruction peacefulConstruction))
        {
            if (peacefulConstruction != null)
            {
                List<PeacefulConstruction> constructions = new List<PeacefulConstruction>();
                constructions.Add(peacefulConstruction);
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
                    _peacefulConstruction = nearestEnemy.transform;
                    peacefulConstruction.Died += OnDied;

                }
                else
                {
                    _peacefulConstruction = null;
                    peacefulConstruction.Died -= OnDied;
                }
            }
        }
    }

    private void OnDied(PeacefulConstruction peacefulConstruction)
    {
        constructions.Remove(peacefulConstruction);
    }
}
