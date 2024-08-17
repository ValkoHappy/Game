using System;
using Agava.YandexGames;
using Scripts.SO;
using UnityEngine;

namespace Scripts.Miner
{
    public class GoldContainer : MonoBehaviour
    {
        private const string Coins = "Coins";

        [SerializeField] private int _gold;

        private int _allGoldReceived;

        public event Action<int> GoldChanged;

        public int Gold => _gold;
        public int AllGoldReceived => _allGoldReceived;

        public void Add(int value)
        {
            _gold += value;
            _allGoldReceived += value;
            GoldChanged?.Invoke(_gold);

#if UNITY_WEBGL && !UNITY_EDITOR
        if (PlayerAccount.IsAuthorized)
            Leaderboard.SetScore(Coins, _allGoldReceived);
#endif
        }

        public void BuyBuilding(Goods statsBuilding)
        {
            _gold -= statsBuilding.Price;
            GoldChanged?.Invoke(_gold);
        }

        public void Init(int gold, int allGoldReceived)
        {
            _gold = gold;
            _allGoldReceived = allGoldReceived;
        }
    }
}