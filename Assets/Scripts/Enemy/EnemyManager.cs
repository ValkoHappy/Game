using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EnemyManager : MonoBehaviour
{
    private List<Enemy> _enemies;

    public event UnityAction AllEnemiesKilled;

    private void Start()
    {
        _enemies = new List<Enemy>();
    }

    public void AddEnemy(Enemy enemy)
    {
        _enemies.Add(enemy);
        enemy.Died += OnEnemyDeath;
    }

    public void OnEnemyDeath(Enemy enemy)
    {
        _enemies.Remove(enemy);
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
}
