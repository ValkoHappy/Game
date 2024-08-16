using System;
using UnityEngine;
using UnityEngine.UI;

public class FinalWindowOfTraining : UIScreenAnimator
{
    [SerializeField] private Button[] _openPanelButtons;
    [SerializeField] private Button _resumeButton;

    [SerializeField] private MainMenuScreen _mainMenuScreen;
    [SerializeField] private ShopScreen _shopScreen;
    [SerializeField] private YandexAds _yandexAds;

    public event Action ButtonResumed;

    private void OnEnable()
    {
        _resumeButton.onClick.AddListener(OnResumeButtonClick);

        foreach (var button in _openPanelButtons)
        {
            button.onClick.AddListener(OnOpen);
        }
    }

    public void OnDisable()
    {
        _resumeButton.onClick.RemoveListener(OnResumeButtonClick);

        foreach (var button in _openPanelButtons)
        {
            button.onClick.RemoveListener(OnOpen);
        }
    }

    public override void OnOpen()
    {
        _mainMenuScreen.TurnOffAllButton();
        base.OnOpen();
    }

    private void OnResumeButtonClick()
    {
        _yandexAds.ShowInterstitial();
        _mainMenuScreen.EnabletAllButton();
        _shopScreen.EnabletAllButton();;
        OnClose();
        ButtonResumed?.Invoke();
    }
}
