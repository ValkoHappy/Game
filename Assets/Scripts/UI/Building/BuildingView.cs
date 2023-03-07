using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingView : MonoBehaviour
{
    [SerializeField] private MovementScreen _movementScreen;
    [SerializeField] private BuildingMenuScreen _menuScreen;
    private Building _building;
    private BuildingsGrid _buildingGrid;
    private Camera _camera;

    private void Awake()
    {
        _building = GetComponent<Building>();
        _buildingGrid = FindObjectOfType<BuildingsGrid>();
        _camera = Camera.main;
    }

    private void Start()
    {
        _movementScreen.Open();
        _menuScreen.Close();
    }

    private void Update()
    { 
        if(_movementScreen.Panel.active == false)
            OnMenuScreen();
    }

    private void OnEnable()
    {
        _building.DeliveryBuilding += _movementScreen.Close;

        _buildingGrid.EditPositionBuilding += OnEditPositionBuilding;

    }

    private void OnDisable()
    {
        _building.DeliveryBuilding -= _movementScreen.Close;

        _buildingGrid.EditPositionBuilding += OnEditPositionBuilding;
    }

    private void OnMenuScreen()
    {
        if (Input.GetMouseButtonDown(0))
        {

            Ray ray = _camera.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit))
            {
                if (hit.collider.gameObject.tag == "target")
                {
                    _menuScreen.Open();
                    Debug.Log(hit.collider.gameObject.name);
                }
            }
        }
    }

    private void OnEditPositionBuilding()
    {
        _movementScreen.Open();
        _menuScreen.Close();

    }
}
