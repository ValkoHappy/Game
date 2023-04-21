using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EnemyHandler : MonoBehaviour
{
    private List<Enemy> _enemies;
    private int _deadEnemiesCount;
    private bool _isAttackBegun = false;

    public event UnityAction AllEnemiesKilled;

    public bool IsAttackBegun => _isAttackBegun;
    public int DeadEnemiesCount => _deadEnemiesCount;


    private void Awake()
    {
        _enemies = new List<Enemy>();
    }

    public void AddEnemy(Enemy enemy)
    {
        _enemies.Add(enemy);
        enemy.Died += OnEnemyDeath;
        enemy.enabled = false;
        _isAttackBegun = false;
    }

    public void OnEnemyDeath(Enemy enemy)
    {
        _enemies.Remove(enemy);
        _deadEnemiesCount++;
        enemy.Died -= OnEnemyDeath;

        if (_enemies.Count <= 0)
        {
            AllEnemiesKilled?.Invoke();
        }
    }

    public void OnDestroyEnemies()
    {
        foreach (var enemy in _enemies)
        {
            Destroy(enemy.gameObject);
        }
        _enemies.Clear();
    }

    public void OnEnemies()
    {
        foreach (var enemy in _enemies)
        {
            enemy.enabled = true;
            _isAttackBegun = true;
        }
    }
}
