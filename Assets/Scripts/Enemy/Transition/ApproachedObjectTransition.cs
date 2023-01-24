using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ApproachedObjectTransition : EnemyTransition
{
    [SerializeField] private float _approachedDistance;

    private void Update()
    {
        if (Vector3.Distance(PeacefulConstruction.transform.position, transform.position) < _approachedDistance)
            NeedTransit = true;
    }
}
