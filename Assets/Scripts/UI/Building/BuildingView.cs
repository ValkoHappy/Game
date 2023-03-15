using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class BuildingView : MonoBehaviour
{
    [SerializeField] private MovementScreen _movementScreen;
    //[SerializeField] private BuildingMenuScreen _menuScreen;
    private Building _building;
    private Building _clickedBuilding;
    private BuildingsGrid _buildingGrid;
    private Camera _camera;
    private BuildingsViewManager _buildingsViewManager;

    private void Awake()
    {
        _building = GetComponent<Building>();
        _buildingGrid = FindObjectOfType<BuildingsGrid>();
        _buildingsViewManager = FindObjectOfType<BuildingsViewManager>();
        _camera = Camera.main;
    }

    private void Start()
    {
        _buildingsViewManager.OpenMenu(_movementScreen);
        //_menuScreen.Close();
    }

    private void Update()
    { 
        //if(_movementScreen.Panel.active == false)
        //    OnMenuScreen();
    }

    private void OnEnable()
    {
        _building.DeliveryBuilding += _movementScreen.Close;

        _buildingGrid.EditPositionBuilding += OnEditPositionBuilding;
    }

    private void OnDisable()
    {
        _building.DeliveryBuilding -= _movementScreen.Close;

        _buildingGrid.EditPositionBuilding -= OnEditPositionBuilding;
    }

    private void OnMenuScreen()
    {
        //if (_buildingsViewManager == null)
        //{
        //    return;
        //}

        //if (_buildingsViewManager == null || _buildingsViewManager.MenuOpen)
        //{
        //    _buildingsViewManager.CloseMenu(_menuScreen);
        //    return;
        //}

        //if (Input.GetMouseButtonDown(0))
        //{
        //    if (EventSystem.current.IsPointerOverGameObject())
        //    {
        //        return;
        //    }

        //    Ray ray = _camera.ScreenPointToRay(Input.mousePosition);
        //    RaycastHit hit;
        //    if (Physics.Raycast(ray, out hit))
        //    {
        //        _clickedBuilding = hit.collider.gameObject.GetComponentInParent<Building>();
        //        if (_clickedBuilding != null && _clickedBuilding == _building)
        //        {
        //            _buildingsViewManager.OpenMenu(_menuScreen);
        //        }
        //        else
        //        {
        //            _buildingsViewManager.CloseMenu(_menuScreen);
        //        }
        //    }
        //}
    }

    private void OnEditPositionBuilding()
    {
        _buildingsViewManager.OpenMenu(_movementScreen);
    }
}
