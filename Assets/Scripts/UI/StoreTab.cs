using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoreTab : MonoBehaviour
{
    [SerializeField] private List<StatsBuilding> _buildings;
    [SerializeField] private MoneyContainer _moneyContainer;
    [SerializeField] private BuilderView _builderView;
    [SerializeField] private GameObject _itenContainer;

    private void Start()
    {
        for (int i = 0; i < _buildings.Count; i++)
        {
            AddItem(_buildings[i]);
        }
    }

    private void AddItem(StatsBuilding building)
    {
        var view = Instantiate(_builderView, _itenContainer.transform);
        view.SellButtonClick += OnSellButtonClick;
        view.Render(building);
    }

    private void OnSellButtonClick(StatsBuilding statsBuilding, BuilderView builderView)
    {
        TrySellBuilding(statsBuilding, builderView);
    }

    private void TrySellBuilding(StatsBuilding statsBuilding, BuilderView builderView)
    {
        if (statsBuilding.Price <= _moneyContainer.Money)
        {
            _moneyContainer.BuyBuilding(statsBuilding);
            //builderView.SellButtonClick -= OnSellButtonClick;

            BuildingsGrid buildingsGrid = FindObjectOfType<BuildingsGrid>();
            buildingsGrid.CreateBuilding(statsBuilding.BuildingPrefab);
        }
    }
}
