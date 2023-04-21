using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class ShopScreen : UIScreenAnimator
{
    [SerializeField] private LevelReward _levelReward;

    [SerializeField] private Button _exitButton;
    [SerializeField] private Button _advertisingButton;

    [SerializeField] private Button _storeTabFencesButton;
    [SerializeField] private Button _storeTabWeaponsButton;
    [SerializeField] private Button _storeTabGeneratorsButton;

    [SerializeField] private UIScreenAnimator _storeTabFences;
    [SerializeField] private UIScreenAnimator _storeTabWeapons;
    [SerializeField] private UIScreenAnimator _storeTabGenerators;

    public event UnityAction ExitButtonClick;

    private void Start()
    {
        _storeTabWeapons.OpenScreen();   
    }


    private void OnEnable()
    {
        _exitButton.onClick.AddListener(OnExitButton);
        _advertisingButton.onClick.AddListener(GetCrystalsForAdvertising);

        _storeTabFencesButton.onClick.AddListener(OnStoreTabMainBuildings);
        _storeTabWeaponsButton.onClick.AddListener(OnStoreTabWeapons);
        _storeTabGeneratorsButton.onClick.AddListener(OnStoreTabGenerators);

    }

    private void OnDisable()
    {
        _exitButton.onClick.RemoveListener(OnExitButton);
        _advertisingButton.onClick.RemoveListener(GetCrystalsForAdvertising);

        _storeTabFencesButton.onClick.RemoveListener(OnStoreTabMainBuildings);
        _storeTabWeaponsButton.onClick.RemoveListener(OnStoreTabWeapons);
        _storeTabGeneratorsButton.onClick.RemoveListener(OnStoreTabGenerators);
    }

    public void OnExitButton()
    {
        ExitButtonClick?.Invoke();
    }

    private void OnStoreTabMainBuildings()
    {
        _storeTabFences.OpenScreen();
        _storeTabFences.Panel.alpha = 1;
        _storeTabWeapons.CloseScreen();
        _storeTabWeapons.Panel.alpha = 0;
        _storeTabGenerators.CloseScreen();
        _storeTabGenerators.Panel.alpha = 0;
    }

    private void OnStoreTabWeapons()
    {
        _storeTabFences.CloseScreen();
        _storeTabFences.Panel.alpha = 0;
        _storeTabWeapons.OpenScreen();
        _storeTabWeapons.Panel.alpha = 1;
        _storeTabGenerators.CloseScreen();
        _storeTabGenerators.Panel.alpha = 0;
    }

    private void OnStoreTabGenerators()
    {
        _storeTabFences.CloseScreen();
        _storeTabFences.Panel.alpha = 0;
        _storeTabWeapons.CloseScreen();
        _storeTabWeapons.Panel.alpha = 0;
        _storeTabGenerators.OpenScreen();
        _storeTabGenerators.Panel.alpha = 1;
    }

    private void GetCrystalsForAdvertising()
    {
        _levelReward.ClaimCrystalsForAdvertising();
    }

    public void TurnOffAllButton()
    {
        _exitButton.enabled = false;
        _advertisingButton.enabled = false;
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
        _storeTabFencesButton.enabled = true;
        _storeTabWeaponsButton.enabled = true;
        _storeTabGeneratorsButton.enabled = true;
    }
}
