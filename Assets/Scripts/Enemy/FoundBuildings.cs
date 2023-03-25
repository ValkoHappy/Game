using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class FoundBuildings : MonoBehaviour
{
    private PeacefulConstruction _targetConstruction;

    private Coroutine _removeCoroutine;

    public PeacefulConstruction TargetConstruction => _targetConstruction;

    private List<PeacefulConstruction> _constructions = new List<PeacefulConstruction>();

    private void Start()
    {
        //StartRemove();
    }

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

    private void SortEnemies()
    {
        float shortestDistance = Mathf.Infinity;
        PeacefulConstruction nearestEnemy = null;

        foreach (var construction in _constructions)
        {
            if (construction != null && construction.IsAlive())
            {
                float distanceToConstruction = Vector3.Distance(transform.position, construction.transform.position);

                if (distanceToConstruction < shortestDistance)
                {
                    shortestDistance = distanceToConstruction;
                    nearestEnemy = construction;
                }
            }
            else if(construction.IsAlive() == false)
            {
                nearestEnemy = null;
            }

            if (nearestEnemy != null && nearestEnemy.IsAlive())
            {
                _targetConstruction = nearestEnemy;
            }
        }
    }

    public void StartRemove()
    {
        if (_removeCoroutine != null)
        {
            StopCoroutine(_removeCoroutine);
        }
        _removeCoroutine = StartCoroutine(RemoveBuilding());

    }

    private IEnumerator RemoveBuilding()
    {
        var waitForSeconds = new WaitForSeconds(5f);
        if (_constructions.Count > 0)
        {
            foreach (var construction in _constructions)
            {
                if (construction != null && construction.IsAlive() == false)
                {
                    _constructions.Remove(construction);
                }
            }
        }
        yield return waitForSeconds;
        StartRemove();
    }
}
