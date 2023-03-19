using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UIElements;
using static MoveSelection;

public class BuildingsGrid : MonoBehaviour
{
    [SerializeField] private BuildingsManager _buildingsManager;

    [SerializeField] private Vector2Int _gridSize;
    [SerializeField] private Transform _container;

    private Building[,] _grid;
    private Building _flyingBuilding;

    private MoveSelection _moveSelection;

    public event UnityAction CreatedBuilding;
    public event UnityAction DeliveredBuilding;
    public event UnityAction EditPositionBuilding;
    public event UnityAction<Building> DestroyBuilding;


    private void Awake()
    {
        _grid = new Building[_gridSize.x, _gridSize.y];
        _moveSelection = GetComponent<MoveSelection>();
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
                if (_moveSelection.Mode == BuildingMode.Insert || Input.GetKeyUp(KeyCode.I))
                {
                    PlaceFlyingBuilding(x, y);
                }
                else if (_moveSelection.Mode == BuildingMode.Delete || Input.GetKeyUp(KeyCode.O))
                {
                    DestroyBuilding?.Invoke(_flyingBuilding);
                    Destroy(_flyingBuilding.gameObject);
                    _flyingBuilding = null;
                    DeliveredBuilding?.Invoke();
                }
                _moveSelection.SetBuildingModeMovement();
            }

            Vector3 movement = Vector3.zero;
            if (_moveSelection.Moves == BuildingMoves.Up || Input.GetKeyUp(KeyCode.T))
            {
                movement += Vector3.forward;
            }
            if (_moveSelection.Moves == BuildingMoves.Down || Input.GetKeyUp(KeyCode.G))
            {
                movement += Vector3.back;
            }
            if (_moveSelection.Moves == BuildingMoves.Left || Input.GetKeyUp(KeyCode.F))
            {
                movement += Vector3.left;
            }
            if (_moveSelection.Moves == BuildingMoves.Right || Input.GetKeyUp(KeyCode.H))
            {
                movement += Vector3.right;
            }
            if (_flyingBuilding != null && movement != Vector3.zero)
            {
                _flyingBuilding.transform.position += movement;
            }
            _moveSelection.SetBuildingMovesStraight();
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

    public void RemoveGrid()
    {
        _grid = new Building[_gridSize.x, _gridSize.y];
    }
}
