using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingsGrid : MonoBehaviour
{
    [SerializeField] private Vector2Int _grigSize;

    private Building[,] _grid;
    private Building _flayingBuilding;
    private Camera _camera;

    private void Awake()
    {
        _grid = new Building[_grigSize.x, _grigSize.y];
        _camera = Camera.main;
    }

    public void CreateBuilding(Building buildingPrefab)
    {
        if(_flayingBuilding != null)
        {
            Destroy(_flayingBuilding.gameObject);
        }

        _flayingBuilding = Instantiate(buildingPrefab);
    }

    private void Update()
    {
        if(_flayingBuilding != null)
        {
            Plane groundPlane = new Plane(Vector3.up, Vector3.zero);
            Ray ray = _camera.ScreenPointToRay(Input.mousePosition);

            if(groundPlane.Raycast(ray, out float position))
            {
                Vector3 worldPosition = ray.GetPoint(position);

                int x = Mathf.RoundToInt(worldPosition.x);
                int y = Mathf.RoundToInt(worldPosition.z);

                bool isAvailable = true;

                if(x > _grigSize.x - _flayingBuilding.Size.x)
                    isAvailable = false;
                if(y > _grigSize.y - _flayingBuilding.Size.y)
                    isAvailable = false;

                if(isAvailable && IsPlaceTaken(x, y))
                    isAvailable = false;

                _flayingBuilding.transform.position = new Vector3(x, 0, y);
                _flayingBuilding.SetTransparent(isAvailable);

                if (isAvailable && Input.GetMouseButtonDown(0))
                {
                    PlaceFlyingBuilding(x, y);
                }
            }
        }
    }

    private bool IsPlaceTaken(int placeX, int placeY)
    {
        for (int x = 0; x < _flayingBuilding.Size.x; x++)
        {
            for (int y = 0; y < _flayingBuilding.Size.y; y++)
            {
                if(_grid[placeX + x, placeY + y] != null)
                    return true;
            }
        }
        return false;
    }

    private void PlaceFlyingBuilding(int placeX, int placeY)
    {
        for (int x = 0; x < _flayingBuilding.Size.x; x++)
        {
            for (int y = 0; y < _flayingBuilding.Size.y; y++)
            {
                _grid[placeX + x, placeY + y] = _flayingBuilding;
            }
        }

        _flayingBuilding = null;
        _flayingBuilding.SetNormal();
    }

    private void OnDrawGizmos()
    {
        for (int x = 0; x < _grigSize.x; x++)
        {
            for (int y = 0; y < _grigSize.y; y++)
            {
                Gizmos.color = new Color(0, 1, 0, 0.3f);
                Gizmos.DrawCube(transform.position + new Vector3(x, 0, y), new Vector3(1, .1f, 1));
            }
        }
    }
}
