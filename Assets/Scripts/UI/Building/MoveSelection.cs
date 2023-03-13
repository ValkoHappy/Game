using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static BuildingsGrid;

public class MoveSelection : MonoBehaviour
{
    private BuildingMode _buildingMode;
    private BuildingMoves _buildingMoves;
    public enum BuildingMode { Movement, Insert, Delete }
    public enum BuildingMoves { Straight, Up, Down, Left, Right }

    public BuildingMode Mode => _buildingMode;
    public BuildingMoves Moves => _buildingMoves;

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

    public void SetBuildingMovesUp()
    {
        _buildingMoves = BuildingMoves.Up;
    }

    public void SetBuildingMovesStraight()
    {
        _buildingMoves = BuildingMoves.Straight;
    }

    public void SetBuildingMovesDown()
    {
        _buildingMoves = BuildingMoves.Down;
    }

    public void SetBuildingMovesLeft()
    {
        _buildingMoves = BuildingMoves.Left;
    }

    public void SetBuildingMovesRight()
    {
        _buildingMoves = BuildingMoves.Right;
    }
}
