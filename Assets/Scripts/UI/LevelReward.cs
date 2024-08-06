using System;
using UnityEngine;

public class LevelReward : MonoBehaviour
{
    [SerializeField] private EnemyHandler _enemyHandler;
    [SerializeField] private GoldContainer _goldContainer;
    [SerializeField] private CrystalsContainer _crystalsContainer;
    [SerializeField] private Spawner _spawner;
    [SerializeField] private ButtonRewardAd _buttonRewardAd;

    private int _crystalsCount = 0;
    private int _goldCount = 0;
    private int _goldSpent = 0;
    private int _crystalsSpent = 0;
    private int _crystalsForAdvertising = 50;
    private int _doubleMultiplier = 2;

    public event Action<int> GoldChanged;
    public event Action<int> CrystalsChanged;

    public int CrystalsCount => _crystalsCount;
    public int GoldCount => _goldCount;

    private void OnEnable()
    {
        _spawner.LevelStarted += OnClearSpentResources;
        _enemyHandler.AllEnemiesKilled += OnCalculateReward;

        _buttonRewardAd.Shown += OnClaimDouble;
        _buttonRewardAd.Shown += OnClaimCrystalsAdvertising;
    }

    private void OnDisable()
    {
        _spawner.LevelStarted -= OnClearSpentResources;
        _enemyHandler.AllEnemiesKilled -= OnCalculateReward;

        _buttonRewardAd.Shown -= OnClaimDouble;
        _buttonRewardAd.Shown -= OnClaimCrystalsAdvertising;
    }

    public void OnCalculateReward()
    {
        _crystalsCount = _spawner.Level.CristalsReward;
        _goldCount =  _spawner.Level.GoldReward;
        CrystalsChanged?.Invoke(_crystalsCount);
        GoldChanged?.Invoke(_goldCount);
    }

    public void ClaimReward()
    {
        _goldContainer.Add(_goldCount);
        _crystalsContainer.Add(_crystalsCount);
        _goldCount = 0;
        _crystalsCount = 0;
    }

    public void OnClaimDouble()
    {
        _goldContainer.Add(_goldCount);
        _crystalsContainer.Add(_crystalsCount * _doubleMultiplier);
        _goldCount = 0;
        _crystalsCount = 0;
    }

    public void AddGoldSpent(int price)
    {
        _goldSpent += price;
    }

    public void AddCrystalsSpent(int price)
    {
        _crystalsSpent += price;
    }

    public void RemoveGoldSpent(int price)
    {
        _goldSpent -= price;
    }

    public void RemoveCrystalsSpent(int price)
    {
        _crystalsSpent -= price;
    }

    public void ReturnSpentResources()
    {
        _goldContainer.Add(_goldSpent);
        _crystalsContainer.Add(_crystalsSpent);
        _crystalsSpent = 0;
        _goldSpent = 0;
    }

    public void OnClearSpentResources()
    {
        _crystalsSpent = 0;
        _goldSpent = 0;
    }

    public void ClaimCrystalsAdvertisingReward() 
    {
        _buttonRewardAd.ShowRewardAd();
    }

    private void OnClaimCrystalsAdvertising()
    {
        _crystalsContainer.Add(_crystalsForAdvertising);
    }
}
