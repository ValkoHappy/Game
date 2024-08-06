using System;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHandler : MonoBehaviour
{
    private List<Enemy> _enemies;

    private int _deadEnemiesCount;
    private bool _isAttackBegun = false;

    public event Action AllEnemiesKilled;
    public event Action EnemiesIncluded;
    public event Action EnemiesRemoved;

    public bool IsAttackBegun => _isAttackBegun;

    private void Awake()
    {
        _enemies = new List<Enemy>();
    }

    public void Add(Enemy enemy)
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
            AllEnemiesKilled?.Invoke();
    }

    public void OnDestroyEnemies()
    {
        foreach (var enemy in _enemies)
        {
            Destroy(enemy.gameObject);
        }

        _enemies.Clear();
        EnemiesRemoved?.Invoke();
    }

    public void OnEnemies()
    {
        foreach (var enemy in _enemies)
        {
            enemy.enabled = true;
            _isAttackBegun = true;
        }

        EnemiesIncluded?.Invoke();
    }
}
