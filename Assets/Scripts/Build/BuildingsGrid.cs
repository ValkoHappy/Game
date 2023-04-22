using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using static MoveSelection;

public class BuildingsGrid : MonoBehaviour
{
    [SerializeField] private BuildingsHandler _buildingsHandler;

    [SerializeField] private Vector2Int _gridSize;
    [SerializeField] private Transform _container;
    [SerializeField] private Building _towerHall;

    private Building[,] _grid;
    private Building _flyingBuilding;
    private Camera _camera;
    private bool _isBuildingSelected = false;
    private bool _isBuild;

    private MoveSelection _moveSelection;

    public event UnityAction CreatedBuilding;
    public event UnityAction DeliveredBuilding;
    public event UnityAction<Building> DestroyBuilding;
    public event UnityAction<Building> BuildingSupplied;
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
                _isBuild = false;
            }
            else
            {
                _flyingBuilding.SetTransparent(true);
                _isBuild = true;
                if (_moveSelection.Mode == BuildingMode.Insert && _isBuild)
                {
                    PlaceFlyingBuilding(x, y);
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
                    positionX = Mathf.Clamp(positionX, 0, _gridSize.x - _flyingBuilding.TileSize.x);
                    positionY = Mathf.Clamp(positionY, 0, _gridSize.y - _flyingBuilding.TileSize.y);
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

    public void RemoveGrid()
    {
        _grid = new Building[_gridSize.x, _gridSize.y];
    }

    public void CreateTowerHall()
    {
        _flyingBuilding = Instantiate(_towerHall, _container);
        _buildingsHandler.AddBuilding(_flyingBuilding.PeacefulConstruction);
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
        BuildingSupplied?.Invoke(_flyingBuilding);
        DeliveredBuilding?.Invoke();
        _flyingBuilding = null;
    }
}
