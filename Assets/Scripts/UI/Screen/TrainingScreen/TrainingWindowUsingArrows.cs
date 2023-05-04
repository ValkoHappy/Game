using UnityEngine;
using UnityEngine.UI;

public class TrainingWindowUsingArrows : UIScreenAnimator
{
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

    private const string Turret = "Turret";
    private const string Oil = "Oil";
    private const string Fence = "Fence";

    private void Start()
    {
        _shopScreen.TurnOffAllButton();
    }

    private void OnEnable()
    {
        _shopButton.onClick.AddListener(EnableAtrrowOnProduct);
        _generationsShopButton.onClick.AddListener(EnableAtrrowOnProduct);
        _fencesShopButton.onClick.AddListener(EnableAtrrowOnProduct);
        _exitShopButton.onClick.AddListener(TurnOffAtrrowOnProduct);
        _buildingGrid.CreatedBuilding += TurnOffAtrrowOnProduct;
        _buildingGrid.BuildingSupplied += OnOpenIndicators;
        _buildingGrid.RemoveBuilding += EnableAtrrowOnProduct;
    }

    private void OnDisable()
    {
        _shopButton.onClick.RemoveListener(EnableAtrrowOnProduct);
        _generationsShopButton.onClick.RemoveListener(EnableAtrrowOnProduct);
        _fencesShopButton.onClick.RemoveListener(EnableAtrrowOnProduct);
        _exitShopButton.onClick.RemoveListener(TurnOffAtrrowOnProduct);
        _buildingGrid.CreatedBuilding -= TurnOffAtrrowOnProduct;
        _buildingGrid.BuildingSupplied -= OnOpenIndicators;
        _buildingGrid.RemoveBuilding -= EnableAtrrowOnProduct;;
    }

    public void EnableAtrrowOnProduct()
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

    private void TurnOffAtrrowOnProduct()
    {
        _atrrowOnProduct.SetActive(false);
        _arrowPointingToExitButton.SetActive(false);
    }
}
