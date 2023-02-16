using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class MinerAnimation : MonoBehaviour
{
    [SerializeField] private float _duration;
    [SerializeField] private bool _isStartAnimation;

    public float Duration => _duration;

    private Building _building;

    private void Awake()
    {
        _building = GetComponentInParent<Building>();
    }

    private void Start()
    {
        if(_isStartAnimation)
        {
            OnDeliveryBuilding();
        }
    }

    private void OnEnable()
    {
        if (_isStartAnimation == false)
        {
            _building.DeliveryBuilding += OnDeliveryBuilding;
        }

    }

    private void OnDisable()
    {
        if (_isStartAnimation == false)
        {
            _building.DeliveryBuilding -= OnDeliveryBuilding;
        }
    }

    public abstract void OnDeliveryBuilding();
}
