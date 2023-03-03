using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

public class BuildingsManager : MonoBehaviour
{
    [SerializeField] private Transform _container;

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
        if(_preservedBuildings != null)
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
            Destroy(building.gameObject);
        }

        _buildings.Clear();

        foreach (var building in _preservedBuildings)
        {
            PeacefulConstruction newBuilding = Instantiate(building.gameObject, _container).GetComponent<PeacefulConstruction>();
            _buildings.Add(newBuilding);
        }

        _preservedBuildings.Clear();
    }

    //private Building LoadSavedBuilding(string buildingId)
    //{
    //    // Load the building data for this ID from your storage
    //    // For example, if you're using PlayerPrefs:
    //    string buildingData = PlayerPrefs.GetString("building_" + buildingId);

    //    // Create a new Building object from the loaded data
    //    Building newBuilding = new Building();
    //    newBuilding.Deserialize(buildingData);

    //    return newBuilding;
    //}
}
