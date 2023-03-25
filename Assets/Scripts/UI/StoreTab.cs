using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoreTab : MonoBehaviour
{
    [SerializeField] private GoldContainer _goldContainer;
    [SerializeField] private CrystalsContainer _crystalsContainer; 
    [SerializeField] private BuildingsManager _buildingsManager;
    [SerializeField] private BuildingsGrid _buildingsGrid;
    [SerializeField] private LevelReward _levelReward;

    [SerializeField] private List<Goods> _buildings;
    [SerializeField] private BuilderView _builderViewGold;
    [SerializeField] private BuilderView _builderViewCrystals;
    [SerializeField] private Transform _itenContainer;

    private int _priceGoods = 0;
    private bool _isSoldForCrystalsGoods;

    private void OnEnable()
    {
        _buildingsManager.PurchaseCancelled += OnPurchaseCancelled;
    }

    private void OnDisable()
    {
        _buildingsManager.PurchaseCancelled -= OnPurchaseCancelled;
    }

    private void Start()
    {
        for (int i = 0; i < _buildings.Count; i++)
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
                _levelReward.AddCrystalsSpent(statsBuilding.Price);
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
                _levelReward.AddGoldSpent(statsBuilding.Price);
                _isSoldForCrystalsGoods = false;
            }
            else
            {
                return;
            }
        }

        //if (statsBuilding.IsSingleProduct == false)
        //{
        //    builderView.SellButtonClick -= OnSellButtonClick;
        //}   


            Building building = _buildingsGrid.CreateBuilding(statsBuilding.BuildingPrefab);
        _buildingsManager.AddBuilding(building.PeacefulConstruction);
    }

    private void OnPurchaseCancelled()
    {
        if (_isSoldForCrystalsGoods)
        {
            _crystalsContainer.GetCrystals(_priceGoods);
        }
        else
        {
            _goldContainer.GetGold(_priceGoods);
        }
    }
}
