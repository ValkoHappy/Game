using System;
using System.Collections.Generic;
using UnityEngine;

public class BuildingsHandler : MonoBehaviour
{
    private const string Fence = "Fence";

    [SerializeField] private Transform _container;
    [SerializeField] private BuildingsGrid _buildingsGrid;
    [SerializeField] private StarsScore _starsScore;

    private List<PeacefulConstruction> _buildings;

    public event Action BuildingsBroked;
    public event Action BuildingsDeleted;

    private void Awake()
    {
        _buildings = new List<PeacefulConstruction>();
    }

    private void OnEnable()
    {
        _buildingsGrid.BuildingDestroyed += OnDestroyBuilding;
    }

    private void OnDisable()
    {
        _buildingsGrid.BuildingDestroyed -= OnDestroyBuilding;
    }

    public void AddBuilding(PeacefulConstruction building)
    {
        _buildings.Add(building);

        if(building.tag != Fence)
            _starsScore.AddBuildingsCount();

        building.ConstructionDied += OnBuildingDeath;
    }

    public void OnBuildingDeath(PeacefulConstruction building)
    {
        building.ConstructionDied -= OnBuildingDeath;

        if (_buildings.Count <= 0)
            BuildingsBroked?.Invoke();

        if(building.IsAlive() ==false)
        {
            if (building.tag != Fence)
                _starsScore.AddBuildingsDiedCount();
        }

        bool allBuildingsDestroyed = true;

        foreach (var construction in _buildings)
        {
            if (construction.IsAlive())
            {
                allBuildingsDestroyed = false;
                break;
            }
        }

        if (allBuildingsDestroyed)
            BuildingsBroked?.Invoke();
    }

    public void OnCreateSavedBuildings()
    {
        foreach (var building in _buildings)
        {
            building.ResetDetails();
            building.ConstructionDied += OnBuildingDeath;
        }
    }

    public void OnDestroyAllBuildings()
    {
        foreach (var building in _buildings)
        {
            building.Destroy();
        }

        BuildingsDeleted?.Invoke();
        _buildings.Clear();
    }

    public void OnDestroyBuilding(Building building)
    {
        for (int i = 0; i < _buildings.Count; i++)
        {
            if (_buildings[i].Building == building)
            {
                _buildings.Remove(_buildings[i]);
                _starsScore.RemoveBuildingsCount();
            }
        }
    }
}
