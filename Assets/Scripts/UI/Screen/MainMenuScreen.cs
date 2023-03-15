using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class MainMenuScreen : ScreenUI
{
    [SerializeField] private Button _playButton;
    [SerializeField] private Button _shopButton;
    [SerializeField] private Button _questsButton;
    [SerializeField] private Button _rankingButton;
    [SerializeField] private Button _settingButton;

    public event UnityAction PlayButtonClick;
    public event UnityAction ShopButtonClick;
    public event UnityAction QuestsButtonClick;
    public event UnityAction RankingButtonClick;
    public event UnityAction SettingButtonClick;

    private void OnEnable()
    {
        _playButton.onClick.AddListener(OnPlayButton);
        _shopButton.onClick.AddListener(OnShopButton);
        _questsButton.onClick.AddListener(OnQuestsButton);
        _rankingButton.onClick.AddListener(OnRankingButton);
        _settingButton.onClick.AddListener(OnSettingButton);
    }

    private void OnDisable()
    {
        _playButton.onClick.RemoveListener(OnPlayButton);
        _shopButton.onClick.RemoveListener(OnShopButton);
        _questsButton.onClick.RemoveListener(OnQuestsButton);
        _rankingButton.onClick.RemoveListener(OnRankingButton);
        _settingButton.onClick.RemoveListener(OnSettingButton);
    }

    public void OnPlayButton()
    {
        PlayButtonClick?.Invoke();
    }

    public void OnShopButton()
    {
        ShopButtonClick?.Invoke();
    }

    public void OnQuestsButton()
    {
        QuestsButtonClick?.Invoke();
    }

    public void OnRankingButton()
    {
        RankingButtonClick?.Invoke();
    }

    public void OnSettingButton()
    {
        SettingButtonClick?.Invoke();
    }
}
