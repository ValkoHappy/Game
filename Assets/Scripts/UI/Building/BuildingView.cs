using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingView : MonoBehaviour
{
    [SerializeField] private MovementScreen _movementScreen;
    private Building _building;

    private void Awake()
    {
        _building = GetComponent<Building>();
    }

    private void OnEnable()
    {
        _building.DeliveryBuilding += _movementScreen.Close;
    }

    private void OnDisable()
    {
        _building.DeliveryBuilding -= _movementScreen.Close;
    }
}
