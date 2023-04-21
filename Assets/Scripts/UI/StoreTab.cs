using System.Collections.Generic;
using UnityEngine;

public class StoreTab : MonoBehaviour
{
    [SerializeField] private GoldContainer _goldContainer;
    [SerializeField] private CrystalsContainer _crystalsContainer; 
    [SerializeField] private BuildingsHandler _buildingsManager;
    [SerializeField] private BuildingsGrid _buildingsGrid;
    [SerializeField] private LevelReward _levelReward;
    [SerializeField] private TrainingScreen _trainingScreen;

    [SerializeField] private List<Goods> _buildings;
    [SerializeField] private BuilderView _builderViewGold;
    [SerializeField] private BuilderView _builderViewCrystals;
    [SerializeField] private Transform _itenContainer;

    private int _priceGoods = 0;
    private bool _isSoldForCrystalsGoods;
    private const string Level = "Level";

    private void OnEnable()
    {
        _buildingsGrid.DeliveredBuilding += OnCanceliedPurchase;
        _buildingsGrid.RemoveBuilding += OnPurchaseCancelled;

        if (_trainingScreen != null)
        {
            _trainingScreen.TutorialFinished += AddMissingItems;
        }
    }

    private void OnDisable()
    {
        _buildingsGrid.DeliveredBuilding -= OnCanceliedPurchase;
        _buildingsGrid.RemoveBuilding -= OnPurchaseCancelled;

        if (_trainingScreen != null)
        {
            _trainingScreen.TutorialFinished -= AddMissingItems;
        }
    }

    private void Start()
    {
        int level = PlayerPrefs.GetInt(Level);
        if (level <= 1)
        {
            AddItem(_buildings[0]);
        }
        else
        {
            for (int i = 0; i < _buildings.Count; i++)
            {
                AddItem(_buildings[i]);
            }
        }
    }

    public void AddMissingItems()
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
            view.SellButtonClick += OnSellButtonClick;
            view.Render(building);
        }
        else 
        {
            var view = Instantiate(_builderViewGold, _itenContainer);
            view.SellButtonClick += OnSellButtonClick;
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
        Building building = _buildingsGrid.CreateBuilding(statsBuilding.BuildingPrefab);
        _buildingsManager.AddBuilding(building.PeacefulConstruction);
    }

    private void OnPurchaseCancelled()
    {
        if (_isSoldForCrystalsGoods)
        {
            _crystalsContainer.AddCrystals(_priceGoods);
            _priceGoods = 0;
        }
        else
        {
            _goldContainer.GetGold(_priceGoods);
            _priceGoods = 0;
        }
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
