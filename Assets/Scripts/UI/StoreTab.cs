using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoreTab : MonoBehaviour
{
    [SerializeField] private GoldContainer _moneyContainer;
    [SerializeField] private BuildingsManager _buildingsManager;
    [SerializeField] private BuildingsGrid _buildingsGrid;

    [SerializeField] private List<Goods> _buildings;
    [SerializeField] private BuilderView _builderView;
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
        var view = Instantiate(_builderView, _itenContainer);
        view.SellButtonClick += OnSellButtonClick;
        view.Render(building);
    }

    private void OnSellButtonClick(Goods statsBuilding, BuilderView builderView)
    {
        TrySellBuilding(statsBuilding, builderView);
    }

    private void TrySellBuilding(Goods statsBuilding, BuilderView builderView)
    {
        if (statsBuilding.Price <= _moneyContainer.Gold)
        {

            _priceGoods = statsBuilding.Price;
            _moneyContainer.BuyBuilding(statsBuilding);
            //builderView.SellButtonClick -= OnSellButtonClick;

            Building building = _buildingsGrid.CreateBuilding(statsBuilding.BuildingPrefab);
            _buildingsManager.AddBuilding(building.GetComponentInChildren<PeacefulConstruction>());
        }
    }

    private void OnPurchaseCancelled()
    {
        _moneyContainer.GetGold(_priceGoods);
    }
}
