using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoreTab : MonoBehaviour
{
    [SerializeField] private MoneyContainer _moneyContainer;
    [SerializeField] private BuildingsManager _buildingsManager;
    [SerializeField] private BuildingsGrid _buildingsGrid;

    [SerializeField] private List<Goods> _buildings;
    [SerializeField] private BuilderView _builderView;
    [SerializeField] private GameObject _itenContainer;

    private void Start()
    {
        for (int i = 0; i < _buildings.Count; i++)
        {
            AddItem(_buildings[i]);
        }
    }

    private void AddItem(Goods building)
    {
        var view = Instantiate(_builderView, _itenContainer.transform);
        view.SellButtonClick += OnSellButtonClick;
        view.Render(building);
    }

    private void OnSellButtonClick(Goods statsBuilding, BuilderView builderView)
    {
        TrySellBuilding(statsBuilding, builderView);
    }

    private void TrySellBuilding(Goods statsBuilding, BuilderView builderView)
    {
        if (statsBuilding.Price <= _moneyContainer.Money)
        {
            _moneyContainer.BuyBuilding(statsBuilding);
            //builderView.SellButtonClick -= OnSellButtonClick;

            Building building = _buildingsGrid.CreateBuilding(statsBuilding.BuildingPrefab);
            _buildingsManager.AddBuilding(building.GetComponentInChildren<PeacefulConstruction>());
        }
    }
}
