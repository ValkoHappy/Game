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
        enemy.OnDied += OnEnemyDeath;
    }

    public void OnEnemyDeath(Enemy enemy)
    {
        _enemies.Remove(enemy);
        enemy.OnDied -= OnEnemyDeath;

        if (_enemies.Count <= 0)
        {
            AllEnemiesKilled?.Invoke();
        }
    }
}
