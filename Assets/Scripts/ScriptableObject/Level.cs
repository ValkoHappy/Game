using UnityEngine;

[CreateAssetMenu(fileName = "new Level", menuName = "Level", order = 51)]
public class Level : ScriptableObject
{
    [SerializeField] private EnemyCount[] _enemyCounts;
    [SerializeField] private Side _spawnSide;
    [SerializeField] private int _goldReward;
    [SerializeField] private int _cristalsReward;

    public EnemyCount[] EnemyCounts => _enemyCounts;
    public Side SpawnSide => _spawnSide;
    public int GoldReward => _goldReward;
    public int CristalsReward => _cristalsReward;

    public enum Side
    {
        Up,
        Down,
        Left,
        Right
    }
}

[System.Serializable]
public class EnemyCount
{
    [SerializeField] private Enemy _enemy;
    [SerializeField] private int _count;

    public Enemy Enemy => _enemy;
    public int Count => _count;
}
