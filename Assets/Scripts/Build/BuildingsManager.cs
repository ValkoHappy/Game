using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

public class BuildingsManager : MonoBehaviour
{
    [SerializeField] private Transform _container;
    [SerializeField] private List<Building> _buildingPrefabs;

    private List<PeacefulConstruction> _buildings;

    private List<Building> _preservedBuildings;

    public event UnityAction AllBuildingsDestroyed;

    private void Start()
    {
        _buildings = new List<PeacefulConstruction>();
        _preservedBuildings = new List<Building>();
    }

    public void AddBuilding(PeacefulConstruction building)
    {
        _buildings.Add(building);
        building.Die += OnBuildingDeath;
    }

    public void OnBuildingDeath(PeacefulConstruction building)
    {
        _buildings.Remove(building);
        building.Die -= OnBuildingDeath;

        if (_buildings.Count <= 0)
        {
            AllBuildingsDestroyed?.Invoke();
        }
    }

    public void OnSaveBuildings()
    {
        if (_preservedBuildings != null)
            _preservedBuildings.Clear();

        foreach (var building in _buildings)
        {
            _preservedBuildings.Add(building.GetComponentInParent<Building>());
        }
    }

    public void OnCreateSavedBuildings()
    {
        foreach (var building in _buildings)
        {
            Destroy(building.GetComponentInParent<Building>().gameObject);
        }
        Debug.Log("dfdfd");
        _buildings.Clear();

        foreach (var building in _preservedBuildings)
        {
            foreach (var buildingPrefab in _buildingPrefabs)
            {
                if(building == buildingPrefab)
                {
                    Building newBuilding = Instantiate(buildingPrefab, _container);
                    _buildings.Add(newBuilding.GetComponentInChildren<PeacefulConstruction>());
                }
            }
        }
        _preservedBuildings.Clear();
    }
}
