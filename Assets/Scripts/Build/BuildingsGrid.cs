using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class BuildingsGrid : MonoBehaviour
{
    [SerializeField] private Vector2Int _gridSize;
    [SerializeField] private Transform _container;

    private Building[,] _grid;
    private Building _flyingBuilding;
    private Camera _camera;
    private bool _isBuilding;
    public enum BuildingMode { Movement,Insert, Delete }
    private BuildingMode _buildingMode;

    public void SetBuildingModeInsert()
    {
        _buildingMode = BuildingMode.Insert;
    }

    public void SetBuildingModeDelete()
    {
        _buildingMode = BuildingMode.Delete;
    }

    private void Awake()
    {
        _grid = new Building[_gridSize.x, _gridSize.y];
        _camera = Camera.main;
    }

    public Building CreateBuilding(Building buildingPrefab)
    {
        if(_flyingBuilding != null)
        {
            Destroy(_flyingBuilding.gameObject);
        }

        _flyingBuilding = Instantiate(buildingPrefab, _container);
        return _flyingBuilding;
    }

    private void Update()
    {
        if (_flyingBuilding != null)
        {
            var groundPlane = new Plane(Vector3.up, Vector3.zero);
            Ray ray = _camera.ScreenPointToRay(Input.mousePosition);

            if (groundPlane.Raycast(ray, out float position))
            {
                Vector3 worldPosition = ray.GetPoint(position);
                int x = Mathf.RoundToInt(worldPosition.x);
                int y = Mathf.RoundToInt(worldPosition.z);

                if (x < 0 || y < 0 || x > _gridSize.x - _flyingBuilding.TileSize.x || y > _gridSize.y - _flyingBuilding.TileSize.y || IsPlaceTaken(x, y))
                {
                    _flyingBuilding.SetTransparent(false);
                }
                else
                {
                    _flyingBuilding.SetTransparent(true);
                    //if (Input.GetMouseButtonDown(0))
                    //{
                    //    PlaceFlyingBuilding(x, y);
                    //    _isBuilding = false;
                    //}
                    if (_buildingMode == BuildingMode.Insert || Input.GetKeyUp(KeyCode.I))
                    {
                        PlaceFlyingBuilding(x, y);
                        _isBuilding = false;
                    }
                    else if (_buildingMode == BuildingMode.Delete || Input.GetKeyUp(KeyCode.O))
                    {
                        Destroy(_flyingBuilding.gameObject);
                        _flyingBuilding = null;
                    }
                    _buildingMode = BuildingMode.Movement;
                }

                //if (_flyingBuilding != null && Input.GetMouseButton(0))
                //{
                //    if(IsMouseOverBuilding(_flyingBuilding))
                //    {
                //        Vector3 positionWorld = ray.GetPoint(position);
                //        float buildingX = Mathf.Floor(positionWorld.x) + 0.5f;
                //        float buildingY = Mathf.Floor(positionWorld.z) + 0.5f;
                //        _flyingBuilding.transform.position = new Vector3(buildingX, 0, buildingY);
                //    }
                //}

                Vector3 movement = Vector3.zero;
                if (Input.GetKeyUp(KeyCode.UpArrow) || Input.GetKeyUp(KeyCode.T))
                {
                    movement += Vector3.forward;
                }
                if (Input.GetKeyUp(KeyCode.DownArrow) || Input.GetKeyUp(KeyCode.G))
                {
                    movement += Vector3.back;
                }
                if (Input.GetKeyUp(KeyCode.LeftArrow) || Input.GetKeyUp(KeyCode.F))
                {
                    movement += Vector3.left;
                }
                if (Input.GetKeyUp(KeyCode.RightArrow) || Input.GetKeyUp(KeyCode.H))
                {
                    movement += Vector3.right;
                }

                // Move the building
                if (_flyingBuilding != null && movement != Vector3.zero)
                {
                    _flyingBuilding.transform.position += movement;
                }
            }
        }
    }
    private bool IsPlaceTaken(int placeX, int placeY)
    {
        for (int i = 0; i < _flyingBuilding.TileSize.x; i++)
        {
            for (int j = 0; j < _flyingBuilding.TileSize.y; j++)
            {
                if (_grid[placeX + i, placeY + j] != null)
                    return true;
            }
        }
        return false;
    }

    private void PlaceFlyingBuilding(int placeX, int placeY)
    {
        for (int i = 0; i < _flyingBuilding.TileSize.x; i++)
        {
            for (int j = 0; j < _flyingBuilding.TileSize.y; j++)
            {
                _grid[placeX + i, placeY + j] = _flyingBuilding;
            }
        }

        _flyingBuilding.SetNormal();
        _flyingBuilding = null;
    }

    private bool IsMouseOverBuilding(Building building)
    {
        Ray ray = _camera.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit))
        {
            return hit.collider == building.GetComponentInChildren<BoxCollider>();
        }
        return false;
    }
}
