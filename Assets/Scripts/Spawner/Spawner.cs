using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Spawner : MonoBehaviour
{
    [SerializeField] private EnemyHandler _enemyHandler;
    [SerializeField] private SceneNext _sceneManage;
    [SerializeField] private Transform _container;
    [SerializeField] private List<Level> _levels;
    [SerializeField] private Transform[] _spawnPoints;

    private int _currentLevelIndex = 0;
    private int _levelIndex = 1;
    private Level _currentLevel;
    private EnemyCount[] _enemyCounts;

    public event UnityAction<int> LevelChanged;
    public event UnityAction LevelStarted;
    public event UnityAction LevelCreated;
    public event UnityAction CurrentLevelExceedsCount;

    public Level Level => _currentLevel;
    public int CurrentLevelIndex => _currentLevelIndex;
    public int LevelIndex => _levelIndex;

    private void Start()
    {
        ShowLevel();
        StartLevel();
    }

    private void OnEnable()
    {
        _enemyHandler.AllEnemiesKilled += NextLevel;
    }

    private void OnDisable()
    {
        _enemyHandler.AllEnemiesKilled -= NextLevel;
    }

    public void NextLevel()
    {
        if (_currentLevelIndex < _levels.Count)
        {
            _currentLevelIndex++;
            _levelIndex++;
            LevelStarted?.Invoke();
        }
    }

    public void StartLevel()
    {
        if (_currentLevelIndex >= _levels.Count)
        {
            return;
        }
        _currentLevel = _levels[_currentLevelIndex];
        _enemyCounts = _currentLevel.EnemyCounts;

        SpawnEnemies();
    }

    public void ShowLevel()
    {
        LevelChanged?.Invoke(_levelIndex);
    }

    public void SwitchAnotherMap()
    {
        if (_currentLevelIndex >= _levels.Count)
        {
            _sceneManage.NextScene();
        }
    }

    public bool ChecForMaximumLevel()
    {
        if (_currentLevelIndex >= _levels.Count)
        {
            _sceneManage.ShowScene();
            CurrentLevelExceedsCount?.Invoke();
            return true;
        }
        return false;
    }

    public void InitCurrentLevel(int currentLevel)
    {
        _currentLevelIndex = currentLevel;
    }

    public void InitLevel(int currentLevel)
    {
        _levelIndex = currentLevel;
    }

    private void SpawnEnemies()
    {
        foreach (var enemy in _enemyCounts)
        {
            Transform spawnPoint;
            Enemy enemyCurrent = null;
            int countEnemies = enemy.Count;
            while (countEnemies > 0)
            {
                spawnPoint = GetSpawnPoint();
                enemyCurrent = Instantiate(enemy.Enemy, spawnPoint.position, Quaternion.identity, _container);
                _enemyHandler.AddEnemy(enemyCurrent);
                enemyCurrent = null;
                countEnemies--;
            }
        }
    }

    private Transform GetSpawnPoint()
    {
        List<Transform> points = new List<Transform>();
        foreach (Transform spawnPoint in _spawnPoints)
        {
            if (spawnPoint.GetComponent<SpawnPoint>().Side == _currentLevel.SpawnSide)
            {
                points.Add(spawnPoint);
            }
        }

        if (points.Count == 0)
        {
            return null;
        }
        return points[Random.Range(0, points.Count)];
    }
}
