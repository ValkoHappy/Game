using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class BuildingsManager : MonoBehaviour
{
    private List<PeacefulConstruction> _buildings;

    public event UnityAction AllBuildingsDestroyed;

    private void Start()
    {
        _buildings = new List<PeacefulConstruction>();
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
}
