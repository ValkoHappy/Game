using UnityEngine;

namespace Scripts.SO.Struct
{
    [System.Serializable]
    public struct EnemyCount
    {
        [SerializeField] private Enemy.Enemy _enemy;
        [SerializeField] private int _count;

        public Enemy.Enemy Enemy => _enemy;
        public int Count => _count;
    }
}