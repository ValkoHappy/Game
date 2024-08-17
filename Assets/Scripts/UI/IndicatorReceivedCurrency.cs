using System;
using Scripts.Build;
using Scripts.Miner;
using UnityEngine;

namespace Scripts.UI
{
    public class IndicatorReceivedCurrency : MonoBehaviour
    {
        [SerializeField] private string _extraction;
        [SerializeField] private BuildingsGrid _buildingsGrid;
        [SerializeField] private Spawner.Spawner _spawner;
        [SerializeField] private BuildingsHandler _buildingsHandler;
        [SerializeField] private BuildingRemover _buildingRemover;

        public event Action<int> OnCurrencyReceived;

        public int AmountCurrencyReceived { get; private set; }

        private void OnEnable()
        {
            _buildingRemover.BuildingRemoved += OnRemoveAmount;
            _buildingsGrid.BuildingSupplied += OnAddAmount;
            _spawner.LevelStarted += OnRemoveAllAmount;
            _buildingsHandler.BuildingsDeleted += OnRemoveAllAmount;
        }

        private void OnDisable()
        {
            _buildingRemover.BuildingRemoved -= OnRemoveAmount;
            _buildingsGrid.BuildingSupplied -= OnAddAmount;
            _spawner.LevelStarted -= OnRemoveAllAmount;
            _buildingsHandler.BuildingsDeleted -= OnRemoveAllAmount;
        }

        public void OnRemoveAllAmount()
        {
            AmountCurrencyReceived = 0;
            OnCurrencyReceived?.Invoke(AmountCurrencyReceived);
        }

        private void OnAddAmount(Build.Building building)
        {
            if (building.tag == _extraction)
                AmountCurrencyReceived += building.GetComponentInChildren<GeneratorMining>().AmountMoneyProduced;

            OnCurrencyReceived?.Invoke(AmountCurrencyReceived);
        }

        private void OnRemoveAmount(Build.Building building)
        {
            if (building.tag == _extraction)
                AmountCurrencyReceived -= building.GetComponentInChildren<GeneratorMining>().AmountMoneyProduced;

            OnCurrencyReceived?.Invoke(AmountCurrencyReceived);
        }
    }
}