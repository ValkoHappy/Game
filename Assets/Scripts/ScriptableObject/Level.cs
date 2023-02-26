using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using UnityEngine;

[CreateAssetMenu(fileName = "new Level", menuName = "Level", order = 51)]
public class Level : ScriptableObject
{
    [SerializeField] private Enemy _miniEnemy;
    [SerializeField] private Enemy _bossEnemy;
    [SerializeField] private int _miniEnemyCount;
    [SerializeField] private int _bossEnemyCount;
    [SerializeField] private float _spawnDelay;
    [SerializeField] private Side _spawnSide;

    public Enemy MiniEnemy => _miniEnemy;
    public Enemy BossEnemy => _bossEnemy;
    public int MiniEnemyCount => _miniEnemyCount;
    public int BossEnemyCount => _bossEnemyCount;
    public float SpawnDelay => _spawnDelay;
    public Side SpawnSide => _spawnSide;

    public enum Side
    {
        Up,
        Down,
        Left,
        Right
    }
}
