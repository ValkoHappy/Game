using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

public class BuildingsManager : MonoBehaviour
{
    [SerializeField] private Transform _container;
    [SerializeField] private BuildingsGrid _buildingsGrid;
    [SerializeField] private StarsScore _starsScore;

    private List<PeacefulConstruction> _buildings;
    
    public event UnityAction AllBuildingsDestroyed;
    public event UnityAction TownHallNotBuilt;
    public event UnityAction PurchaseCancelled;

    private void Start()
    {
        _buildings = new List<PeacefulConstruction>();
    }

    private void OnEnable()
    {
        _buildingsGrid.DestroyBuilding += OnDestroyBuilding;
    }

    private void OnDisable()
    {
        _buildingsGrid.DestroyBuilding -= OnDestroyBuilding;
    }

    public void AddBuilding(PeacefulConstruction building)
    {
        _buildings.Add(building);
        _starsScore.AddBuildingsCount();
        building.Die += OnBuildingDeath;
    }

    public void OnBuildingDeath(PeacefulConstruction building)
    {
        //building.Die -= OnBuildingDeath;
        if (_buildings.Count <= 0)
        {
            AllBuildingsDestroyed?.Invoke();
        }

        if(building.IsAlive() ==false)
        {
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
        {
            AllBuildingsDestroyed?.Invoke();
        }
    }

    public void OnCreateSavedBuildings()
    {
        foreach (var building in _buildings)
        {
            building.ResetDetails();
        }
    }

    private void OnDestroyBuilding(Building building)
    {
        for (int i = 0; i < _buildings.Count; i++)
        {
            if (_buildings[i].GetComponentInParent<Building>() == building)
            {
                PurchaseCancelled?.Invoke();
                Debug.Log(PurchaseCancelled);
                _buildings.Remove(_buildings[i]);
                _starsScore.RemoveBuildingsCount();
            }

        }
    }

    public void OnDestroyAllBuildings()
    {
        foreach (var building in _buildings)
        {
            building.Destroy();
        }

        _buildings.Clear();
    }

    public bool IsTownHallBuilt()
    {
        foreach (var building in _buildings)
        {
            if (building.CompareTag("TownHall"))
            {
                return true;
            }
        }
        TownHallNotBuilt?.Invoke();
        return false;
    }
}
