using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class MinerAnimation : MonoBehaviour
{
    [SerializeField] private float _duration;

    public float Duration => _duration;

    private Building _building;

    public void Awake()
    {
        _building = GetComponentInParent<Building>();
    }

    private void OnEnable()
    {
        _building.DeliveryBuilding += OnDeliveryBuilding;
    }

    private void OnDisable()
    {
        _building.DeliveryBuilding -= OnDeliveryBuilding;
    }

    public abstract void OnDeliveryBuilding();
}
