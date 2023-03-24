using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class LevelReward : MonoBehaviour
{
    [SerializeField] private EnemyManager _enemyManager;
    [SerializeField] private GoldContainer _goldContainer;
    [SerializeField] private CrystalsContainer _crystalsContainer;
    [SerializeField] private Spawner _spawner;

    private int _crystalsCount = 0;
    private int _goldCount = 0;
    private int _goldSpent = 0;
    private int _crystalsSpent = 0;
    public event UnityAction<int> GoldChanged;
    public event UnityAction<int> CrystalsChanged;

    public int CrystalsCount => _crystalsCount;
    public int GoldCount => _goldCount;

    public void CalculateReward()
    {
        _crystalsCount = _spawner.Level.CristalsReward;
        _goldCount =  _spawner.Level.GoldReward;
        CrystalsChanged?.Invoke(_crystalsCount);
        GoldChanged?.Invoke(_goldCount);
    }

    public void GetReward()
    {
        _goldContainer.GetGold(_goldCount);
        _crystalsContainer.GetCrystals(_crystalsCount);
    }

    public void AddGoldSpent(int price)
    {
        _goldSpent += price;
    }

    public void AddCrystalsSpent(int price)
    {
        _crystalsSpent += price;
    }
    public void ReturnAfterLosing()
    {
        _goldContainer.GetGold(_goldSpent);
        _crystalsContainer.GetCrystals(_crystalsSpent);
        _crystalsSpent = 0;
        _goldSpent = 0;
    }
}
