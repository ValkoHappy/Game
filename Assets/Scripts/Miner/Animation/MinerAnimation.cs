using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public abstract class MinerAnimation : MonoBehaviour
{
    [SerializeField] private float _duration;
    [SerializeField] private bool _isStartAnimation;

    private int _repetitions = -1;
    protected Tween TweenAnimation;

    public int Repetitions => _repetitions;

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

    public  void Pause()
    {
        if (TweenAnimation != null)
        {
            TweenAnimation.Kill();
        }
    }
}
