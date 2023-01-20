using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LostObjectTransition : EnemyTransition
{
    [SerializeField] private float _minimumLostDistance;

    private void Update()
    {
        if (Vector3.Distance(PeacefulConstruction.transform.position, transform.position) < _minimumLostDistance)
            NeedTransit = true;
    }
}
