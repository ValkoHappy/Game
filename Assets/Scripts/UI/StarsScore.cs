using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class StarsScore : MonoBehaviour
{
    [SerializeField] private GameObject[] _stars;
    [SerializeField] private GameObject[] _rewards;

    private float _buildingsCount;
    private float _buildingsDiedCount;
    private float _buildingsStars;

    public void ShowStars()
    {
        _buildingsStars = 100 - (_buildingsDiedCount / _buildingsCount * 100);
        if (_buildingsStars >= 1)
        {
            LeanTween.scale(_stars[0], new Vector3(1f, 1f, 1f), 2f).setDelay(.1f).setEase(LeanTweenType.easeOutElastic);
        }
        if (_buildingsStars >= 20)
        {
            LeanTween.scale(_stars[1], new Vector3(1f, 1f, 1f), 2f).setDelay(.2f).setEase(LeanTweenType.easeOutElastic);
        }
        if(_buildingsStars >= 40) 
        {
            LeanTween.scale(_stars[2], new Vector3(1f, 1f, 1f), 2f).setDelay(.3f).setEase(LeanTweenType.easeOutElastic);
        }
        if (_buildingsStars >= 80)
        {
            LeanTween.scale(_stars[3], new Vector3(1f, 1f, 1f), 2f).setDelay(.4f).setEase(LeanTweenType.easeOutElastic);
        }
        if (_buildingsStars == 100)
        {
            LeanTween.scale(_stars[4], new Vector3(1f, 1f, 1f), 2f).setDelay(.5f).setEase(LeanTweenType.easeOutElastic);
        }
        LeanTween.scale(_rewards[0], new Vector3(1f, 1f, 1f), 2f).setDelay(.6f).setEase(LeanTweenType.easeOutElastic);
        LeanTween.scale(_rewards[1], new Vector3(1f, 1f, 1f), 2f).setDelay(.7f).setEase(LeanTweenType.easeOutElastic);
    }

    public void CloseStars()
    {
        foreach (var star in _stars)
        {
            star.LeanScale(new Vector3(0, 0, 0), 0);
        }
        foreach (var reward in _rewards)
        {
            reward.LeanScale(new Vector3(0, 0, 0), 0);
        }
        _buildingsStars = 0;
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
