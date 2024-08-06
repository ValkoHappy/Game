using System;
using UnityEngine;
using UnityEngine.UI;

public class ShopScreen : UIScreenAnimator
{
    [SerializeField] private LevelReward _levelReward;
    [SerializeField] private BuildingsGrid _buildingsGrid;
    [SerializeField] private BuildingRemover _buildingRemover;

    [SerializeField] private Button _exitButton;
    [SerializeField] private Button _advertisingButton;
    [SerializeField] private Button _deleteBuildingsButton;

    [SerializeField] private Button _storeTabFencesButton;
    [SerializeField] private Button _storeTabWeaponsButton;
    [SerializeField] private Button _storeTabGeneratorsButton;

    [SerializeField] private UIScreenAnimator _storeTabFences;
    [SerializeField] private UIScreenAnimator _storeTabWeapons;
    [SerializeField] private UIScreenAnimator _storeTabGenerators;

    public event Action Exited;
    public event Action BuildingsDeleted;

    private void Start()
    {
        _storeTabWeapons.OnOpen();   
    }

    private void OnEnable()
    {
        _buildingsGrid.BuildingCreated += OnClose;
        _buildingsGrid.BuildingRemoved += OnOpen;
        _buildingsGrid.BuildingDelivered += OnOpen;
        _deleteBuildingsButton.onClick.AddListener(OnDeleteBuildingsButton);

        _exitButton.onClick.AddListener(OnExitButtonClick);
        _advertisingButton.onClick.AddListener(OnClaimCrystalsAdvertising);
        _storeTabFencesButton.onClick.AddListener(OnStoreTabMainBuildings);
        _storeTabWeaponsButton.onClick.AddListener(OnStoreTabWeapons);
        _storeTabGeneratorsButton.onClick.AddListener(OnStoreTabGenerators);
    }

    private void OnDisable()
    {
        _buildingsGrid.BuildingCreated -= OnClose;
        _buildingsGrid.BuildingRemoved -= OnOpen;
        _buildingsGrid.BuildingDelivered -= OnOpen;
        _deleteBuildingsButton.onClick.RemoveListener(OnDeleteBuildingsButton);

        _exitButton.onClick.RemoveListener(OnExitButtonClick);
        _advertisingButton.onClick.RemoveListener(OnClaimCrystalsAdvertising);
        _storeTabFencesButton.onClick.RemoveListener(OnStoreTabMainBuildings);
        _storeTabWeaponsButton.onClick.RemoveListener(OnStoreTabWeapons);
        _storeTabGeneratorsButton.onClick.RemoveListener(OnStoreTabGenerators);
    }

    public void TurnOffAllButton()
    {
        _exitButton.enabled = false;
        _advertisingButton.enabled = false;
        _deleteBuildingsButton.enabled = false;
        _storeTabFencesButton.enabled = false;
        _storeTabWeaponsButton.enabled = false;
        _storeTabGeneratorsButton.enabled = false;
    }

    public void EnabletGeneratorsButton()
    {
        _storeTabGeneratorsButton.enabled = true;
    }

    public void EnabletFencesButton()
    {
        _storeTabGeneratorsButton.enabled = false;
        _storeTabFencesButton.enabled = true;
    }
    public void EnabletExitButton()
    {
        _storeTabFencesButton.enabled = false;
        _exitButton.enabled = true;
    }

    public void EnabletAllButton()
    {
        _exitButton.enabled = true;
        _advertisingButton.enabled = true;
        _deleteBuildingsButton.enabled = true;
        _storeTabFencesButton.enabled = true;
        _storeTabWeaponsButton.enabled = true;
        _storeTabGeneratorsButton.enabled = true;
    }

    public void Open()
    {
        OnStoreTabWeapons();
        OnOpen();
    }

    private void OnExitButtonClick()
    {
        Exited?.Invoke();
    }

    private void OnStoreTabMainBuildings()
    {
        _storeTabFences.OnOpen();
        _storeTabWeapons.OnClose();
        _storeTabGenerators.OnClose();
    }

    private void OnStoreTabWeapons()
    {
        _storeTabFences.OnClose();
        _storeTabWeapons.OnOpen();
        _storeTabGenerators.OnClose();
    }

    private void OnStoreTabGenerators()
    {
        _storeTabFences.OnClose();
        _storeTabWeapons.OnClose();
        _storeTabGenerators.OnOpen();
    }

    private void OnClaimCrystalsAdvertising()
    {
        _levelReward.ClaimCrystalsAdvertisingReward();
    }

    private void OnDeleteBuildingsButton()
    {
        BuildingsDeleted?.Invoke();
        _buildingRemover.enabled = true;
    }
}
