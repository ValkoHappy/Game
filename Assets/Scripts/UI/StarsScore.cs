using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StarsScore : MonoBehaviour
{
    [SerializeField] private GameObject[] _stars;

    private float _buildingsCount;
    private float _buildingsDiedCount;
    private float _buildingsStars;

    public void ShowStars()
    {
        _buildingsStars = 100 - (_buildingsDiedCount / _buildingsCount * 100);

        if (_buildingsStars >= 1)
        {
            _stars[0].SetActive(true);
        }
        if (_buildingsStars >= 20)
        {
            _stars[1].SetActive(true);
        }
        if(_buildingsStars >= 40) 
        {
            _stars[2].SetActive(true);
        }
        if (_buildingsStars >= 60)
        {
            _stars[3].SetActive(true);
        }
        if (_buildingsStars >= 90)
        {
            _stars[4].SetActive(true);
        }
    }

    public void CloseStars()
    {
        foreach (var star in _stars)
        {
            star.SetActive(false);
        }
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
