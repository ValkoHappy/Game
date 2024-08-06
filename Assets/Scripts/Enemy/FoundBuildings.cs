using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoundBuildings : MonoBehaviour
{
    private PeacefulConstruction _targetConstruction;
    private Coroutine _removeCoroutine;
    private Coroutine _sortCoroutine;
    private WaitForSeconds _sortWait = new WaitForSeconds(0.5f);
    private WaitForSeconds _removeWait = new WaitForSeconds(5f);

    private List<PeacefulConstruction> _constructions = new List<PeacefulConstruction>();

    public PeacefulConstruction TargetConstruction => _targetConstruction;

    private void Start()
    {
        StartSortConstructions();
    }

    public void StartRemove()
    {
        if (_removeCoroutine != null)
        {
            StopCoroutine(_removeCoroutine);
            _sortCoroutine = null;
        }

        _removeCoroutine = StartCoroutine(RemoveBuilding());
    }

    public void StartSortConstructions()
    {
        if (_sortCoroutine != null)
        {
            StopCoroutine(_sortCoroutine);
            _sortCoroutine = null;
        }

        _sortCoroutine = StartCoroutine(SortConstructions());
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out PeacefulConstruction peacefulConstruction))
        {
            if (peacefulConstruction != null && peacefulConstruction.IsAlive())
                _constructions.Add(peacefulConstruction);
        }
    }

    private IEnumerator SortConstructions()
    {
        if (_constructions.Count > 0)
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
                else if (construction.IsAlive() == false)
                    nearestEnemy = null;

                if (nearestEnemy != null && nearestEnemy.IsAlive())
                    _targetConstruction = nearestEnemy;
            }
        }

        yield return _sortWait;

        StartSortConstructions();
    }

    private IEnumerator RemoveBuilding()
    {
        if (_constructions.Count > 0)
        {
            foreach (var construction in _constructions)
            {
                if (construction != null && construction.IsAlive() == false)
                    _constructions.Remove(construction);
            }
        }

        yield return _removeWait;

        StartRemove();
    }
}
