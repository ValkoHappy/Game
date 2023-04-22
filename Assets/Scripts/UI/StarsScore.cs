using UnityEngine;

public class StarsScore : MonoBehaviour
{
    [SerializeField] private EnemyHandler _enemyHandler;

    [SerializeField] private GameObject[] _stars;
    [SerializeField] private GameObject[] _rewards;

    private float _buildingsCount;
    private float _buildingsDiedCount;
    private float _buildingsStars;
    private float _delay = 2f;
    private float _scaleElasticDelay = 0.1f;
    private int _starsThreshold = 20;
    private int _buildingsStarsPercentage = 100;

    private void OnEnable()
    {
        _enemyHandler.AllEnemiesKilled += ShowStars;
    }

    private void OnDisable()
    {
        _enemyHandler.AllEnemiesKilled -= ShowStars;
    }

    public void ShowStars()
    {
        _buildingsStars = _buildingsStarsPercentage - (_buildingsDiedCount / _buildingsCount * _buildingsStarsPercentage);
        for (int i = 0; i < _stars.Length; i++)
        {
            if (_buildingsStars >= (i + 1) * _starsThreshold)
            {
                LeanTween.scale(_stars[i], new Vector3(1f, 1f, 1f), _delay).setDelay(_scaleElasticDelay * (i + 1)).setEase(LeanTweenType.easeOutElastic);
            }
        }

        for (int i = 0; i < _rewards.Length; i++)
        {
            LeanTween.scale(_rewards[i], new Vector3(1f, 1f, 1f), _delay).setDelay(_scaleElasticDelay * (i + 1)).setEase(LeanTweenType.easeOutElastic);
        }
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
        _buildingsCount--;
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
