using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class Spawner : MonoBehaviour
{
    [SerializeField] private EnemyManager _enemyManager;
    [SerializeField] private SceneNext _sceneManage;
    [SerializeField] private Transform _container;
    [SerializeField] private List<Level> _levels;
    [SerializeField] private Transform[] _spawnPoints;

    private Coroutine _coroutine;
    private int _currentLevelIndex = 0;
    private int _levelIndex = 1;
    private Level _currentLevel;
    private int _miniEnemiesRemaining;
    private int _bossEnemiesRemaining;

    public event UnityAction<int> LevelChanged;
    public event UnityAction LevelStarted;
    public event UnityAction ÑurrentLevelExceedsCount;

    public Level Level => _currentLevel;
    public int CurrentLevelIndex => _currentLevelIndex;
    public int LevelIndex => _levelIndex;


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
            Debug.Log("All levels completed!");
            return;
        }

        _currentLevel = _levels[_currentLevelIndex];
        _miniEnemiesRemaining = _currentLevel.MiniEnemyCount;
        _bossEnemiesRemaining = _currentLevel.BossEnemyCount;

        if (_coroutine != null)
        {
            StopCoroutine(_coroutine);
        }
        _coroutine = StartCoroutine(SpawnEnemies());
    }

    private IEnumerator SpawnEnemies()
    {
        while (_miniEnemiesRemaining > 0 || _bossEnemiesRemaining > 0)
        {
            Transform spawnPoint = GetSpawnPoint();
            Enemy enemy = null;
            if (_miniEnemiesRemaining > 0)
            {
                enemy = Instantiate(_currentLevel.MiniEnemy, spawnPoint.position, Quaternion.identity, _container);
                _miniEnemiesRemaining--;
            }
            else if (_bossEnemiesRemaining > 0)
            {
                enemy = Instantiate(_currentLevel.BossEnemy, spawnPoint.position, Quaternion.identity, _container);
                _bossEnemiesRemaining--;
            }
            _enemyManager.AddEnemy(enemy);
        }

        yield return new WaitForSeconds(_currentLevel.SpawnDelay);

        Debug.Log("Level complete!");
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
            Debug.LogError($"No spawn points found for side {_currentLevel.SpawnSide}");
            return null;
        }

        return points[Random.Range(0, points.Count)];
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

    public void ChecForMaximumLevel()
    {
        if (_currentLevelIndex >= _levels.Count)
        {
            _sceneManage.ShowScene();
            ÑurrentLevelExceedsCount?.Invoke();
        }
    }


    public void InitCurrentLevel(int currentLevel)
    {
        _currentLevelIndex = currentLevel;
    }

    public void InitLevel(int currentLevel)
    {
        _levelIndex = currentLevel + 1;
    }
}
