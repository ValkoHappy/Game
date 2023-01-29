using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Enemy))]
public class ApproachedObjectTransition : EnemyTransition
{
    [SerializeField] private float _approachedDistance;

    [SerializeField] private PeacefulConstruction peacefulConstruction;
    private Enemy enemy;

    private void Start()
    {
        enemy = GetComponent<Enemy>();
        peacefulConstruction = enemy.TargetConstruction;
    }

    private void OnEnable()
    {
        peacefulConstruction = enemy.TargetConstruction;
    }

    private void Update()
    {
        if (Vector3.Distance(peacefulConstruction.transform.position, transform.position) < _approachedDistance)
            NeedTransit = true;
    }
}
