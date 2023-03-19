using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelReward : MonoBehaviour
{
    [SerializeField] private EnemyManager _enemyManager;
    [SerializeField] private GoldContainer _goldContainer;
    [SerializeField] private CrystalsContainer _crystalsContainer;
    
    private int _crystalsCount = 0;
    private int _goldCount = 0;

    public void GetReward()
    {
        _crystalsCount = _enemyManager.DeadEnemiesCount * 3;
        _goldCount = _enemyManager.DeadEnemiesCount;
    }
}
