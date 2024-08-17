using System.Collections.Generic;
using Scripts.Build;
using Scripts.Miner;
using Scripts.SO;
using Scripts.UI.Screen.TrainingScreen;
using UnityEngine;

namespace Scripts.UI
{
    public class StoreTab : MonoBehaviour
    {
        private const string Level = "Level";

        [SerializeField] private GoldContainer _goldContainer;
        [SerializeField] private CrystalsContainer _crystalsContainer;
        [SerializeField] private BuildingsHandler _buildingsHandler;
        [SerializeField] private BuildingsGrid _buildingsGrid;
        [SerializeField] private LevelReward _levelReward;
        [SerializeField] private TrainingScreen _trainingScreen;

        [SerializeField] private List<Goods> _buildings;
        [SerializeField] private BuilderView _builderViewGold;
        [SerializeField] private BuilderView _builderViewCrystals;
        [SerializeField] private Transform _itenContainer;

        private int _priceGoods = 0;
        private bool _isSoldForCrystalsGoods;
        private int _minCountLevel = 1;
        private int _addIndex = 0;

        private void OnEnable()
        {
            _buildingsGrid.BuildingDelivered += OnCanceliedPurchase;
            _buildingsGrid.BuildingRemoved += OnPurchaseCancelled;

            if (_trainingScreen != null)
                _trainingScreen.Finished += OnAddMissingItems;
        }

        private void OnDisable()
        {
            _buildingsGrid.BuildingDelivered -= OnCanceliedPurchase;
            _buildingsGrid.BuildingRemoved -= OnPurchaseCancelled;

            if (_trainingScreen != null)
                _trainingScreen.Finished -= OnAddMissingItems;
        }

        private void Start()
        {
            int level = PlayerPrefs.GetInt(Level);

            if (level <= _minCountLevel)
            {
                AddItem(_buildings[_addIndex]);
            }
            else
            {
                for (int i = 0; i < _buildings.Count; i++)
                {
                    AddItem(_buildings[i]);
                }
            }
        }

        public void OnAddMissingItems()
        {
            for (int i = 1; i < _buildings.Count; i++)
            {
                AddItem(_buildings[i]);
            }
        }

        private void AddItem(Goods building)
        {
            if (building.IsSoldForCrystals)
            {
                var view = Instantiate(_builderViewCrystals, _itenContainer);
                view.Selling += OnSellButtonClick;
                view.Render(building);
            }
            else
            {
                var view = Instantiate(_builderViewGold, _itenContainer);
                view.Selling += OnSellButtonClick;
                view.Render(building);
            }
        }

        private void OnSellButtonClick(Goods statsBuilding, BuilderView builderView)
        {
            TrySellBuilding(statsBuilding, builderView);
        }

        private void TrySellBuilding(Goods statsBuilding, BuilderView builderView)
        {
            _priceGoods = statsBuilding.Price;

            if (statsBuilding.IsSoldForCrystals)
            {
                if (statsBuilding.Price <= _crystalsContainer.Crystals)
                {
                    _crystalsContainer.BuyBuilding(statsBuilding);
                    _isSoldForCrystalsGoods = true;
                }
                else
                {
                    return;
                }
            }
            else
            {
                if (statsBuilding.Price <= _goldContainer.Gold)
                {
                    _goldContainer.BuyBuilding(statsBuilding);
                    _isSoldForCrystalsGoods = false;
                }
                else
                {
                    return;
                }
            }

            Build.Building building = _buildingsGrid.CreateBuilding(statsBuilding.BuildingPrefab);
            _buildingsHandler.AddBuilding(building.PeacefulConstruction);
        }

        private void OnPurchaseCancelled()
        {
            if (_isSoldForCrystalsGoods)
                _crystalsContainer.Add(_priceGoods);
            else
                _goldContainer.Add(_priceGoods);

            _priceGoods = 0;
        }

        private void OnCanceliedPurchase()
        {
            if (_isSoldForCrystalsGoods)
            {
                _levelReward.AddCrystalsSpent(_priceGoods);
                _priceGoods = 0;
            }
            else
            {
                _levelReward.AddGoldSpent(_priceGoods);
                _priceGoods = 0;
            }
        }
    }
}