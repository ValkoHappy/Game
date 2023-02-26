using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] private EnemyManager _enemyManager;
    [SerializeField] private Transform _container;
    [SerializeField] private List<Level> _levels;
    [SerializeField] private Transform[] _upPoints;
    [SerializeField] private Transform[] _downPoints;
    [SerializeField] private Transform[] _leftPoints;
    [SerializeField] private Transform[] _rightPoints;


    private Coroutine _coroutine;
    private int _currentLevelIndex = -1;
    private Level _currentLevel;
    private int _miniEnemiesRemaining;
    private int _bossEnemiesRemaining;

    public void StartNextLevel()
    {
        _currentLevelIndex++;
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
            Transform spawnPoint = GetSpawnPoint(_currentLevel);
            Enemy miniEnemy = Instantiate(_currentLevel.MiniEnemy, spawnPoint.position, Quaternion.identity, _container);
            _enemyManager.AddEnemy(miniEnemy);
            _miniEnemiesRemaining--;
        }

        if (_bossEnemiesRemaining > 0)
            {
                Transform spawnPoint = GetSpawnPoint(_currentLevel);
                Enemy bossEnemy = Instantiate(_currentLevel.BossEnemy, spawnPoint.position, Quaternion.identity, _container);
                _enemyManager.AddEnemy(bossEnemy);
                _bossEnemiesRemaining--;
            }

        yield return new WaitForSeconds(_currentLevel.SpawnDelay);
    
        Debug.Log("Level complete!");
    }

    private Transform GetSpawnPoint(Level level)
    {
        Transform[] points = null;
        switch (level.SpawnSide)
        {
            case Level.Side.Up:
                points = _upPoints;
                break;
            case Level.Side.Down:
                points = _downPoints;
                break;
            case Level.Side.Left:
                points = _leftPoints;
                break;
            case Level.Side.Right:
                points = _rightPoints;
                break;
            default:
                points = _upPoints;
                break;
        }

        return points[Random.Range(0, points.Length)];
    }
}


//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;

//public class Spawner : MonoBehaviour
//{
//    [SerializeField] private EnemyManager _enemyManager;
//    [SerializeField] private Enemy _templateEnemy;
//    [SerializeField] private Enemy _templateMiniBossEnemy;
//    [SerializeField] private float _spawnDelay;
//    [SerializeField] private Vector2Int _spawnPoint;
//    [SerializeField] private Transform _conteiner;
//    [SerializeField] private Level[] _levels;

//    private Coroutine _coroutine;
//    private int number = 25;

//    int numderRundom = 100;

//    public void StartSpawn()
//    {
//        if (_coroutine != null)
//        {
//            StopCoroutine(_coroutine);
//        }

//        _coroutine = StartCoroutine(Spawn());
//    }

//    private IEnumerator Spawn()
//    {
//        if (_spawnEnemyCount > 0)
//        {
//            Enemy enemy = Instantiate(_templateEnemy, SpawnPosition(), Quaternion.identity, _conteiner);
//            if (enemy != null)
//                _enemyManager.AddEnemy(enemy);
//            _spawnEnemyCount--;
//        }
//        if (_spawnMiniBossEnemyCount > 0)
//        {
//            Enemy enemy = Instantiate(_templateMiniBossEnemy, SpawnPosition(), Quaternion.identity, _conteiner);
//            if (enemy != null)
//                _enemyManager.AddEnemy(enemy);
//            _spawnMiniBossEnemyCount--;
//        }
//        yield return new WaitForSeconds(_spawnDelay);

//        StartSpawn();
//    }

//    private Vector3 SpawnPosition()
//    {
//        return new Vector3(transform.position.x + Random.Range(_spawnPoint.x, _spawnPoint.y), transform.position.y, transform.position.z);
//    }
//}
