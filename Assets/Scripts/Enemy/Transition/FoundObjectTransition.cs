using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(RangeCollider))]
public class FoundObjectTransition : EnemyTransition
{
    [SerializeField] private float _foundDistance;
    private RangeCollider _collider;

    private void Awake()
    {
        _collider = GetComponent<RangeCollider>();
    }

    private void Start()
    {
        _collider.GetRange(_foundDistance);
    }

    private void Update()
    {
        if (Vector3.Distance(PeacefulConstruction.transform.position, transform.position) < _foundDistance)
            NeedTransit = true;
    }
}
