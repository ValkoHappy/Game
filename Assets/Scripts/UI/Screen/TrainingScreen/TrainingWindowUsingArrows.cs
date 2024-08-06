using UnityEngine;
using UnityEngine.UI;

public class TrainingWindowUsingArrows : UIScreenAnimator
{
    private const string Turret = "Turret";
    private const string Oil = "Oil";
    private const string Fence = "Fence";

    [SerializeField] private Button _shopButton;
    [SerializeField] private Button _generationsShopButton;
    [SerializeField] private Button _fencesShopButton;
    [SerializeField] private Button _exitShopButton;

    [SerializeField] private GameObject _arrowPointingToGenerators;
    [SerializeField] private GameObject _arrowPointingToFences;
    [SerializeField] private GameObject _arrowPointingToExitButton;
    [SerializeField] private GameObject _atrrowOnProduct;

    [SerializeField] private ShopScreen _shopScreen;
    [SerializeField] private BuildingsGrid _buildingGrid;

    private void Start()
    {
        _shopScreen.TurnOffAllButton();
    }

    private void OnEnable()
    {
        _shopButton.onClick.AddListener(OnEnableAtrrowProduct);
        _generationsShopButton.onClick.AddListener(OnEnableAtrrowProduct);
        _fencesShopButton.onClick.AddListener(OnEnableAtrrowProduct);
        _exitShopButton.onClick.AddListener(OnTurnOffAtrrowProduct);
        _buildingGrid.BuildingCreated += OnTurnOffAtrrowProduct;
        _buildingGrid.BuildingSupplied += OnOpenIndicators;
        _buildingGrid.BuildingRemoved += OnEnableAtrrowProduct;
    }

    private void OnDisable()
    {
        _shopButton.onClick.RemoveListener(OnEnableAtrrowProduct);
        _generationsShopButton.onClick.RemoveListener(OnEnableAtrrowProduct);
        _fencesShopButton.onClick.RemoveListener(OnEnableAtrrowProduct);
        _exitShopButton.onClick.RemoveListener(OnTurnOffAtrrowProduct);
        _buildingGrid.BuildingCreated -= OnTurnOffAtrrowProduct;
        _buildingGrid.BuildingSupplied -= OnOpenIndicators;
        _buildingGrid.BuildingRemoved -= OnEnableAtrrowProduct;;
    }

    public void OnEnableAtrrowProduct()
    {
        _atrrowOnProduct.SetActive(true);
        _arrowPointingToGenerators.SetActive(false);
        _arrowPointingToFences.SetActive(false);
    }

    private void OnOpenIndicators(Building building)
    {
        if (building.tag == Turret)
        {
            _shopScreen.EnabletGeneratorsButton();
            _arrowPointingToGenerators.SetActive(true);
        }
        else if (building.tag == Oil)
        {
            _shopScreen.EnabletFencesButton();
            _arrowPointingToGenerators.SetActive(false);
            _arrowPointingToFences.SetActive(true);
        }
        else if (building.tag == Fence)
        {
            _shopScreen.EnabletExitButton();
            _arrowPointingToFences.SetActive(false);
            _atrrowOnProduct.SetActive(false);
            _arrowPointingToExitButton.SetActive(true);
        }
    }

    private void OnTurnOffAtrrowProduct()
    {
        _atrrowOnProduct.SetActive(false);
        _arrowPointingToExitButton.SetActive(false);
    }
}
