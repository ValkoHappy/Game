using Scripts.Build;
using UnityEngine;
using UnityEngine.UI;

namespace Scripts.UI.Screen.TrainingScreen
{
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
            _shopButton.onClick.AddListener(OnEnableArrowProduct);
            _generationsShopButton.onClick.AddListener(OnEnableArrowProduct);
            _fencesShopButton.onClick.AddListener(OnEnableArrowProduct);
            _exitShopButton.onClick.AddListener(OnTurnOffArrowProduct);
            _buildingGrid.BuildingCreated += OnTurnOffArrowProduct;
            _buildingGrid.BuildingSupplied += OnOpenIndicators;
            _buildingGrid.BuildingRemoved += OnEnableArrowProduct;
        }

        private void OnDisable()
        {
            _shopButton.onClick.RemoveListener(OnEnableArrowProduct);
            _generationsShopButton.onClick.RemoveListener(OnEnableArrowProduct);
            _fencesShopButton.onClick.RemoveListener(OnEnableArrowProduct);
            _exitShopButton.onClick.RemoveListener(OnTurnOffArrowProduct);
            _buildingGrid.BuildingCreated -= OnTurnOffArrowProduct;
            _buildingGrid.BuildingSupplied -= OnOpenIndicators;
            _buildingGrid.BuildingRemoved -= OnEnableArrowProduct;
        }

        public void OnEnableArrowProduct()
        {
            _atrrowOnProduct.SetActive(true);
            _arrowPointingToGenerators.SetActive(false);
            _arrowPointingToFences.SetActive(false);
        }

        private void OnOpenIndicators(Build.Building building)
        {
            if (building.tag == Turret)
            {
                _shopScreen.EnableGeneratorsButton();
                _arrowPointingToGenerators.SetActive(true);
            }
            else if (building.tag == Oil)
            {
                _shopScreen.EnableFencesButton();
                _arrowPointingToGenerators.SetActive(false);
                _arrowPointingToFences.SetActive(true);
            }
            else if (building.tag == Fence)
            {
                _shopScreen.EnableExitButton();
                _arrowPointingToFences.SetActive(false);
                _atrrowOnProduct.SetActive(false);
                _arrowPointingToExitButton.SetActive(true);
            }
        }

        private void OnTurnOffArrowProduct()
        {
            _atrrowOnProduct.SetActive(false);
            _arrowPointingToExitButton.SetActive(false);
        }
    }
}