using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UIElements;

public class BuildingsGrid : MonoBehaviour
{
    [SerializeField] private Vector2Int _gridSize;
    [SerializeField] private Transform _container;

    private Building[,] _grid;
    private Building _flyingBuilding;
    private BuildingMode _buildingMode;
    private BuildingMoves _buildingMoves;

    public event UnityAction CreatedBuilding;
    public event UnityAction DeliveredBuilding;
    public event UnityAction EditPositionBuilding;

    public enum BuildingMode { Movement, Insert, Delete }
    public enum BuildingMoves { Straight, Up, Down, Left, Right }

    private void Awake()
    {
        _grid = new Building[_gridSize.x, _gridSize.y];
    }

    private void Update()
    {
        if (_flyingBuilding != null)
        {
            int x = Mathf.RoundToInt(_flyingBuilding.transform.position.x);
            int y = Mathf.RoundToInt(_flyingBuilding.transform.position.z);

            if (x < 0 || y < 0 || x + _flyingBuilding.TileSize.x > _gridSize.x || y + _flyingBuilding.TileSize.y > _gridSize.y || IsPlaceTaken(x, y))
            {
                _flyingBuilding.SetTransparent(false);
            }
            else
            {
                _flyingBuilding.SetTransparent(true);
                if (_buildingMode == BuildingMode.Insert || Input.GetKeyUp(KeyCode.I))
                {
                    PlaceFlyingBuilding(x, y);
                }
                else if (_buildingMode == BuildingMode.Delete || Input.GetKeyUp(KeyCode.O))
                {
                    Destroy(_flyingBuilding.gameObject);
                    _flyingBuilding = null;
                }
                _buildingMode = BuildingMode.Movement;
            }

            Vector3 movement = Vector3.zero;
            if (_buildingMoves == BuildingMoves.Up || Input.GetKeyUp(KeyCode.T))
            {
                movement += Vector3.forward;
            }
            if (_buildingMoves == BuildingMoves.Down || Input.GetKeyUp(KeyCode.G))
            {
                movement += Vector3.back;
            }
            if (_buildingMoves == BuildingMoves.Left || Input.GetKeyUp(KeyCode.F))
            {
                movement += Vector3.left;
            }
            if (_buildingMoves == BuildingMoves.Right || Input.GetKeyUp(KeyCode.H))
            {
                movement += Vector3.right;
            }
            if (_flyingBuilding != null && movement != Vector3.zero)
            {
                _flyingBuilding.transform.position += movement;
            }
            _buildingMoves = BuildingMoves.Straight;
        }
    }

    public Building CreateBuilding(Building buildingPrefab)
    {
        if (_flyingBuilding != null)
        {
            Destroy(_flyingBuilding.gameObject);
        }

        _flyingBuilding = Instantiate(buildingPrefab, _container);
        CreatedBuilding?.Invoke();
        return _flyingBuilding;
    }

    private bool IsPlaceTaken(int placeX, int placeY)
    {
        for (int i = 0; i < _flyingBuilding.TileSize.x; i++)
        {
            for (int j = 0; j < _flyingBuilding.TileSize.y; j++)
            {
                if (placeX + i >= _gridSize.x || placeY + j >= _gridSize.y || _grid[placeX + i, placeY + j] != null)
                {
                    return true;
                }
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
        DeliveredBuilding?.Invoke();
        _flyingBuilding = null;
    }

    public void MoveBuilding(Building building)
    {
        _flyingBuilding = building;
        EditPositionBuilding?.Invoke();
    }

    public void SetBuildingModeInsert()
    {
        _buildingMode = BuildingMode.Insert;
    }

    public void SetBuildingModeDelete()
    {
        _buildingMode = BuildingMode.Delete;
    }

    public void SetBuildingMovesUp()
    {
        _buildingMoves = BuildingMoves.Up;
    }

    public void SetBuildingMovesDown()
    {
        _buildingMoves = BuildingMoves.Down;
    }

    public void SetBuildingMovesLeft()
    {
        _buildingMoves = BuildingMoves.Left;
    }

    public void SetBuildingMovesRight()
    {
        _buildingMoves = BuildingMoves.Right;
    }
}
