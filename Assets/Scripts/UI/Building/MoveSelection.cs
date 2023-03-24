using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static BuildingsGrid;

public class MoveSelection : MonoBehaviour
{
    private BuildingMode _buildingMode;
    public enum BuildingMode { Movement, Insert, Delete }

    public BuildingMode Mode => _buildingMode;

    public void SetBuildingModeMovement()
    {
        _buildingMode = BuildingMode.Movement;
    }

    public void SetBuildingModeInsert()
    {
        _buildingMode = BuildingMode.Insert;
    }

    public void SetBuildingModeDelete()
    {
        _buildingMode = BuildingMode.Delete;
    }
}
