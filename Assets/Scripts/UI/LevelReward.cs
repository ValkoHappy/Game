using UnityEngine;
using UnityEngine.Events;

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
    public event UnityAction<int> GoldChanged;
    public event UnityAction<int> CrystalsChanged;

    public int CrystalsCount => _crystalsCount;
    public int GoldCount => _goldCount;

    private void OnEnable()
    {
        _spawner.LevelStarted += ClearSpentResources;
        _enemyHandler.AllEnemiesKilled += CalculateReward;

        _buttonRewardAd.ShowReward += ClaimDouble;
        _buttonRewardAd.ShowReward += ClaimCrystalsForAdvertising;
    }

    private void OnDisable()
    {
        _spawner.LevelStarted -= ClearSpentResources;
        _enemyHandler.AllEnemiesKilled -= CalculateReward;

        _buttonRewardAd.ShowReward -= ClaimDouble;
        _buttonRewardAd.ShowReward -= ClaimCrystalsForAdvertising;
    }

    public void CalculateReward()
    {
        _crystalsCount = _spawner.Level.CristalsReward;
        _goldCount =  _spawner.Level.GoldReward;
        CrystalsChanged?.Invoke(_crystalsCount);
        GoldChanged?.Invoke(_goldCount);
    }

    public void ClaimReward()
    {
        _goldContainer.AddGold(_goldCount);
        _crystalsContainer.AddCrystals(_crystalsCount);
        _goldCount = 0;
        _crystalsCount = 0;
    }

    public void ClaimDouble()
    {
        _goldContainer.AddGold(_goldCount);
        _crystalsContainer.AddCrystals(_crystalsCount * _doubleMultiplier);
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
        _goldContainer.AddGold(_goldSpent);
        _crystalsContainer.AddCrystals(_crystalsSpent);
        _crystalsSpent = 0;
        _goldSpent = 0;
    }

    public void ClearSpentResources()
    {
        _crystalsSpent = 0;
        _goldSpent = 0;
    }

    public void ClaimCrystalsForAdvertisingReward() 
    {
        _buttonRewardAd.ShowRewardAd();
    }

    private void ClaimCrystalsForAdvertising()
    {
        _crystalsContainer.AddCrystals(_crystalsForAdvertising);
    }
}
