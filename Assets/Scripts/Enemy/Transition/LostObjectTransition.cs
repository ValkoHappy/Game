using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LostObjectTransition : EnemyTransition
{
    [SerializeField] private float _minimumLostDistance;

    private void Update()
    {
        if (Vector3.Distance(transform.position, PeacefulConstruction.transform.position) > _minimumLostDistance || PeacefulConstruction.IsAlive() == false)
            NeedTransit = true;
    }
}
