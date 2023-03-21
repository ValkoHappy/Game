using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class LevelReward : MonoBehaviour
{
    [SerializeField] private EnemyManager _enemyManager;
    [SerializeField] private GoldContainer _goldContainer;
    [SerializeField] private CrystalsContainer _crystalsContainer;
    
    private int _crystalsCount = 0;
    private int _goldCount = 0;
    public event UnityAction<int> GoldChanged;
    public event UnityAction<int> CrystalsChanged;

    public int CrystalsCount => _crystalsCount;
    public int GoldCount => _goldCount;

    public void CalculateReward()
    {
        //_crystalsCount = _enemyManager.DeadEnemiesCount * 3;
        _crystalsCount += 100;
        //_goldCount = _enemyManager.DeadEnemiesCount;
        _goldCount += _crystalsCount + 25;
        CrystalsChanged?.Invoke(_crystalsCount);
        GoldChanged?.Invoke(_goldCount);
    }

    public void GetReward()
    {
        _goldContainer.GetGold(_goldCount);
        _crystalsContainer.GetCrystals(_crystalsCount);
    }


}
