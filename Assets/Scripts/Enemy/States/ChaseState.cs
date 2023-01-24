
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent), typeof(SphereCollider))]
public class ChaseState : EnemyState
{
    private NavMeshAgent _agent;
    private Transform _peacefulConstruction;
    private SphereCollider _collider;
    private List<PeacefulConstruction> _constructions = new List<PeacefulConstruction>();

    private void Awake()
    {
        _agent = GetComponent<NavMeshAgent>();
        _collider = GetComponent<SphereCollider>();
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
        else if(_constructions.Count > 0)
        {
            float shortestDistance = Mathf.Infinity;
            PeacefulConstruction nearestEnemy = null;

            foreach (var construction in _constructions)
            {
                float distanceToConstruction = Vector3.Distance(transform.position, construction.transform.position);

                if (distanceToConstruction < shortestDistance)
                {
                    shortestDistance = distanceToConstruction;
                    nearestEnemy = construction;
                    PeacefulConstruction = nearestEnemy;
                }


                if (nearestEnemy != null && nearestEnemy.IsAlive())
                {
                    _peacefulConstruction = nearestEnemy.transform;
                }
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out PeacefulConstruction peacefulConstruction))
        {
            if (peacefulConstruction != null && peacefulConstruction.IsAlive())
            {
                //_peacefulConstruction = peacefulConstruction.transform;

                _constructions.Add(peacefulConstruction);
                //float shortestDistance = Mathf.Infinity;
                //PeacefulConstruction nearestEnemy = null;

                //foreach (var construction in constructions)
                //{
                //    float distanceToConstruction = Vector3.Distance(transform.position, construction.transform.position);

                //    if (distanceToConstruction < shortestDistance)
                //    {
                //        shortestDistance = distanceToConstruction;
                //        nearestEnemy = construction;
                //        PeacefulConstruction = nearestEnemy;
                //    }


                //    if (nearestEnemy != null && nearestEnemy.IsAlive())
                //    {
                //        _peacefulConstruction = nearestEnemy.transform;
                //    }
                //}
            }
        }  
    }
}
