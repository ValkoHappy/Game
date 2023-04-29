using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class FinalWindowOfTraining : UIScreenAnimator
{
    [SerializeField] private Button[] _openPanelButtons;
    [SerializeField] private Button _resumeButton;

    [SerializeField] private MainMenuScreen _mainMenuScreen;
    [SerializeField] private ShopScreen _shopScreen;
    [SerializeField] private YandexAds _yandexAds;

    public event UnityAction ResumeButtonClick;

    private void OnEnable()
    {
        _resumeButton.onClick.AddListener(OnResumeButton);
        foreach (var button in _openPanelButtons)
        {
            button.onClick.AddListener(OnOpenScreen);
        }
    }

    private void OnDisable()
    {
        _resumeButton.onClick.RemoveListener(OnResumeButton);
        foreach (var button in _openPanelButtons)
        {
            button.onClick.RemoveListener(OnOpenScreen);
        }
    }

    public void OnOpenScreen()
    {
        OpenScreen();
    }

    private void OnResumeButton()
    {
        _yandexAds.ShowInterstitial();
        _mainMenuScreen.EnabletAllButton();
        _shopScreen.EnabletAllButton();;
        CloseScreen();
        ResumeButtonClick?.Invoke();
    }
}
