using Agava.YandexGames;
using UnityEngine;
using UnityEngine.Events;

public class GoldContainer : MonoBehaviour
{
    [SerializeField] private int _gold;

    private int _allGoldReceived;

    public int Gold => _gold;
    public int AllGoldReceived => _allGoldReceived;
    public event UnityAction<int> GoldChanged;

    public void GetGold(int value)
    {
        _gold += value;
        _allGoldReceived += value;
        GoldChanged?.Invoke(_gold);
#if UNITY_WEBGL && !UNITY_EDITOR
        if (PlayerAccount.IsAuthorized)
        {
            Leaderboard.SetScore("Coins", _allGoldReceived);
        }
#endif
    }

    public void BuyBuilding(Goods statsBuilding)
    {
        _gold -= statsBuilding.Price;
        GoldChanged?.Invoke(_gold);
    }

    public void InitGold(int gold, int allGoldReceived)
    {
        _gold = gold;
        _allGoldReceived = allGoldReceived;
    }
}
