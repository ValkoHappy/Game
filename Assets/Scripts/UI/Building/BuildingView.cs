using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class BuildingView : MonoBehaviour
{
    [SerializeField] private MovementScreen _movementScreen;

    private Building _building;
    private BuildingsGrid _buildingGrid;
    private BuildingsViewManager _buildingsViewManager;

    private void Awake()
    {
        _building = GetComponent<Building>();
        _buildingGrid = FindObjectOfType<BuildingsGrid>();
        _buildingsViewManager = FindObjectOfType<BuildingsViewManager>();
    }

    private void OnEnable()
    {
        _building.CreateBuilding += OnOpen;
        _building.DeliveryBuilding += OnClose;

        _buildingGrid.EditPositionBuilding += OnEditPositionBuilding;
    }

    private void OnDisable()
    {
        _building.CreateBuilding -= OnOpen;
        _building.DeliveryBuilding -= OnClose;

        _buildingGrid.EditPositionBuilding -= OnEditPositionBuilding;
    }

    private void OnEditPositionBuilding()
    {
        _buildingsViewManager.OpenMenu(_movementScreen);
    }

    private void OnOpen()
    {
        _movementScreen.Open();
        _movementScreen.Panel.alpha = 1.0f;
    }
    private void OnClose()
    {
        _movementScreen.Close();
        _movementScreen.Panel.alpha = 0f;
    }
}
