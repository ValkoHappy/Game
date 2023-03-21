using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoreTab : MonoBehaviour
{
    [SerializeField] private GoldContainer _goldContainer;
    [SerializeField] private CrystalsContainer _crystalsContainer; 
    [SerializeField] private BuildingsManager _buildingsManager;
    [SerializeField] private BuildingsGrid _buildingsGrid;

    [SerializeField] private List<Goods> _buildings;
    [SerializeField] private BuilderView _builderViewGold;
    [SerializeField] private BuilderView _builderViewCrystals;
    [SerializeField] private Transform _itenContainer;

    private int _priceGoods = 0;

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
            }
            else
            {
                return;
            }
        }
        //builderView.SellButtonClick -= OnSellButtonClick;

        Building building = _buildingsGrid.CreateBuilding(statsBuilding.BuildingPrefab);
        _buildingsManager.AddBuilding(building.GetComponentInChildren<PeacefulConstruction>());
    }

    private void OnPurchaseCancelled()
    {
        _goldContainer.GetGold(_priceGoods);
    }
}
