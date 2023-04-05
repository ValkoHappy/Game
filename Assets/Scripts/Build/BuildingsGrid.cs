using Agava.YandexGames;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UIElements;
using static MoveSelection;

public class BuildingsGrid : MonoBehaviour
{
    [SerializeField] private BuildingsManager _buildingsManager;

    [SerializeField] private Vector2Int _gridSize;
    [SerializeField] private Transform _container;
    [SerializeField] private Building _towerHall;

    private Building[,] _grid;
    private Building _flyingBuilding;
    private Camera _camera;
    private bool _isBuildingSelected = false;

    private MoveSelection _moveSelection;

    public event UnityAction CreatedBuilding;
    public event UnityAction DeliveredBuilding;
    public event UnityAction EditPositionBuilding;
    public event UnityAction<Building> DestroyBuilding;
    public event UnityAction<Building> ExtrationBuilding;
    public event UnityAction RemoveBuilding;


    private void Awake()
    {
        _grid = new Building[_gridSize.x, _gridSize.y];
        _moveSelection = GetComponent<MoveSelection>();
        _camera = Camera.main;
    }

    private void Start()
    {
        CreateTowerHall();
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
                if (_moveSelection.Mode == BuildingMode.Insert)
                {
                    PlaceFlyingBuilding(x, y);
                    DeliveredBuilding?.Invoke();
                }
            }

            if (_moveSelection.Mode == BuildingMode.Delete)
            {
                DestroyBuilding?.Invoke(_flyingBuilding);
                Destroy(_flyingBuilding.gameObject);
                _flyingBuilding = null;
                RemoveBuilding?.Invoke();
            }
            _moveSelection.SetBuildingModeMovement();


            if (_isBuildingSelected && Input.GetMouseButton(0))
            {
                var groundPlane = new Plane(Vector3.up, Vector3.zero);
                Ray ray = _camera.ScreenPointToRay(Input.mousePosition);

                if (EventSystem.current.IsPointerOverGameObject())
                {
                    return;
                }

                if (groundPlane.Raycast(ray, out float hit))
                {
                    Vector3 newPosition = ray.GetPoint(hit);

                    int positionX = Mathf.RoundToInt(newPosition.x);
                    int positionY = Mathf.RoundToInt(newPosition.z);

                    _flyingBuilding.transform.position = new Vector3(positionX, 0, positionY);
                }
            }
        }
    }

    public Building CreateBuilding(Building buildingPrefab)
    {
        if (_flyingBuilding != null)
        {
            Destroy(_flyingBuilding.gameObject);
        }
        _isBuildingSelected = true;
        _flyingBuilding = Instantiate(buildingPrefab, _container);
        CreatedBuilding?.Invoke();
        _flyingBuilding.Create();
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
        _isBuildingSelected = false;
        _flyingBuilding.SetNormal();
        ExtrationBuilding?.Invoke(_flyingBuilding);
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

    public void CreateTowerHall()
    {
        Building building = _flyingBuilding = Instantiate(_towerHall, _container);
        int placeX = Mathf.RoundToInt(_flyingBuilding.transform.position.x);
        int placeY = Mathf.RoundToInt(_flyingBuilding.transform.position.z);

        for (int i = 0; i < _flyingBuilding.TileSize.x; i++)
        {
            for (int j = 0; j < _flyingBuilding.TileSize.y; j++)
            {
                _grid[placeX + i, placeY + j] = _flyingBuilding;
            }
        }

        _flyingBuilding = null;
        _buildingsManager.AddBuilding(building.PeacefulConstruction);
    }
}
