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

    private void Awake()
    {
        _grid = new Building[_gridSize.x, _gridSize.y];
        _camera = Camera.main;
    }

    public void CreateBuilding(Building buildingPrefab)
    {
        if(_flyingBuilding != null)
        {
            Destroy(_flyingBuilding.gameObject);
        }

        _flyingBuilding = Instantiate(buildingPrefab, _container);
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
                    if (Input.GetMouseButtonDown(0))
                    {
                        PlaceFlyingBuilding(x, y);
                    }
                }

                if(_flyingBuilding != null)
                {
                    _flyingBuilding.transform.position = new Vector3(x, 0, y);
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

    //private void OnDrawGizmos()
    //{
    //    for (int x = 0; x < _gridSize.x; x++)
    //    {
    //        for (int y = 0; y < _gridSize.y; y++)
    //        {
    //            Gizmos.color = new Color(0, 1, 0, 0.3f);
    //            Gizmos.DrawCube(transform.position + new Vector3(x, 0, y), new Vector3(1, .1f, 1));
    //        }
    //    }
    //}
}
