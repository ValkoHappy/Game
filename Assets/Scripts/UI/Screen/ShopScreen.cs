using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class ShopScreen : ScreenUI
{
    [SerializeField] private Button _exitButton;

    [SerializeField] private Button _storeTabMainBuildingsButton;
    [SerializeField] private Button _storeTabWeaponsButton;
    [SerializeField] private Button _storeTabGeneratorsButton;
    [SerializeField] private Button _storeTabDecorativeButton;

    [SerializeField] private ScreenUI _storeTabMainBuildings;
    [SerializeField] private ScreenUI _storeTabWeapons;
    [SerializeField] private ScreenUI _storeTabGenerators;
    [SerializeField] private ScreenUI _storeTabDecorative;

    public event UnityAction ExitButtonClick;

    private void Start()
    {
        _storeTabMainBuildings.Open();
        _storeTabWeapons.Close();
        _storeTabGenerators.Close();
        _storeTabDecorative.Close();
        
    }


    private void OnEnable()
    {
        _exitButton.onClick.AddListener(OnExitButton);

        _storeTabMainBuildingsButton.onClick.AddListener(OnStoreTabMainBuildings);
        _storeTabWeaponsButton.onClick.AddListener(OnStoreTabWeapons);
        _storeTabGeneratorsButton.onClick.AddListener(OnStoreTabGenerators);
        _storeTabDecorativeButton.onClick.AddListener(OnStoreTabDecorative);

    }

    private void OnDisable()
    {
        _exitButton.onClick.RemoveListener(OnExitButton);

        _storeTabMainBuildingsButton.onClick.RemoveListener(OnStoreTabMainBuildings);
        _storeTabWeaponsButton.onClick.RemoveListener(OnStoreTabWeapons);
        _storeTabGeneratorsButton.onClick.RemoveListener(OnStoreTabGenerators);
        _storeTabDecorativeButton.onClick.RemoveListener(OnStoreTabDecorative);
    }

    public void OnExitButton()
    {
        ExitButtonClick?.Invoke();
    }

    private void OnStoreTabMainBuildings()
    {
        _storeTabMainBuildings.Open();
        _storeTabWeapons.Close();
        _storeTabGenerators.Close();
        _storeTabDecorative.Close();
    }

    private void OnStoreTabWeapons()
    {
        _storeTabMainBuildings.Close();
        _storeTabWeapons.Open();
        _storeTabGenerators.Close();
        _storeTabDecorative.Close();
    }

    private void OnStoreTabGenerators()
    {
        _storeTabMainBuildings.Close();
        _storeTabWeapons.Close();
        _storeTabGenerators.Open();
        _storeTabDecorative.Close();
    }

    private void OnStoreTabDecorative()
    {
        _storeTabMainBuildings.Close();
        _storeTabWeapons.Close();
        _storeTabGenerators.Close();
        _storeTabDecorative.Open();
    }
}
