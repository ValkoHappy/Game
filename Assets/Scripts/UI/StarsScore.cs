using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StarsScore : MonoBehaviour
{
    private int _buildingsCount;
    private int _buildingsDiedCount;
    private int _buildingsMaxCount;
    private int _buildingsStars;

    public void ShowStars()
    {
        _buildingsStars = _buildingsDiedCount / _buildingsCount * 100;
        Debug.Log(_buildingsStars);
    }

    public void AddBuildingsCount()
    {
        _buildingsCount++;
    }

    public void RemoveBuildingsCount()
    {
        _buildingsCount++;
    }
    public void AddBuildingsDiedCount()
    {
        _buildingsDiedCount++;
    }

    public void RemoveAllBuildingsDiedCount()
    {
        _buildingsDiedCount = 0;
    }
}
