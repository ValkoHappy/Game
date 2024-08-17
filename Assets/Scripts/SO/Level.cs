using Scripts.SO.Struct;
using UnityEngine;

namespace Scripts.SO
{
    [CreateAssetMenu(fileName = "new Level", menuName = "Level", order = 51)]
    public class Level : ScriptableObject
    {
        [SerializeField] private EnemyCount[] _enemyCounts;
        [SerializeField] private Side _spawnSide;
        [SerializeField] private int _goldReward;
        [SerializeField] private int _cristalsReward;

        public enum Side
        {
            Up,
            Down,
            Left,
            Right,
        }

        public EnemyCount[] EnemyCounts => _enemyCounts;
        public Side SpawnSide => _spawnSide;
        public int GoldReward => _goldReward;
        public int CristalsReward => _cristalsReward;
    }
}