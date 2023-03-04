using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoundBuildings : MonoBehaviour
{
    private PeacefulConstruction _targetConstruction;

    public PeacefulConstruction TargetConstruction => _targetConstruction;

    private List<PeacefulConstruction> _constructions = new List<PeacefulConstruction>();

    private void Update()
    {
        if (_constructions.Count > 0)
            SortEnemies();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out PeacefulConstruction peacefulConstruction))
        {
            if (peacefulConstruction != null && peacefulConstruction.IsAlive())
            {
                _constructions.Add(peacefulConstruction);
            }
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.TryGetComponent(out PeacefulConstruction peacefulConstruction))
        {
            if (peacefulConstruction.IsAlive() == false)
            {
                _constructions.Remove(peacefulConstruction);
            }
        }
    }

    public void SortEnemies()
    {
        float shortestDistance = Mathf.Infinity;
        PeacefulConstruction nearestEnemy = null;

        foreach (var construction in _constructions)
        {
            if (construction != null)
            {
                float distanceToConstruction = Vector3.Distance(transform.position, construction.transform.position);

                if (distanceToConstruction < shortestDistance)
                {
                    shortestDistance = distanceToConstruction;
                    nearestEnemy = construction;
                }
            }

            if (nearestEnemy != null && nearestEnemy.IsAlive())
            {
                _targetConstruction = nearestEnemy;
            }
        }
    }
}
