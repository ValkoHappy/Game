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
    private BuildingsGrid _buildingGrid;

    private void Awake()
    {
        _buildingGrid = FindObjectOfType<BuildingsGrid>();
        _building = GetComponentInParent<Building>();
    }

    private void Start()
    {

    }

    //private void OnEnable()
    //{
    //    if (_isStartAnimation == false)
    //    {
    //        _building.DeliveryBuilding += OnDeliveryBuilding;
    //    }
    //    _buildingGrid.EditPositionBuilding += TurnOffAnimation;
    //    _buildingGrid.DeliveredBuilding += TurnOnAnimation;

    //}

    //private void OnDisable()
    //{
    //    if (_isStartAnimation == false)
    //    {
    //        _building.DeliveryBuilding -= OnDeliveryBuilding;
    //    }
    //    _buildingGrid.EditPositionBuilding -= TurnOffAnimation;
    //    _buildingGrid.DeliveredBuilding -= TurnOnAnimation;
    //}

    public abstract void OnDeliveryBuilding();

    public void Pause()
    {
        if (TweenAnimation != null)
        {
            TweenAnimation.Kill();
        }
    }

    public void TurnOffAnimation()
    {
        Pause();
    }

    public void TurnOnAnimation()
    {
        OnDeliveryBuilding();
    }
}
