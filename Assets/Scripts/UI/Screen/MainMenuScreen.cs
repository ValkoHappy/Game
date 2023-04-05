using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class MainMenuScreen : ScreenUI
{
    [SerializeField] private Button _playButton;
    [SerializeField] private Button _shopButton;
    [SerializeField] private Button _rankingButton;
    [SerializeField] private Button _settingButton;
    [SerializeField] private Button _findSpawnEnemiesButton;

    public event UnityAction PlayButtonClick;
    public event UnityAction ShopButtonClick;
    public event UnityAction RankingButtonClick;
    public event UnityAction SettingButtonClick;
    public event UnityAction FindSpawnEnemiesButtonClick;

    private void OnEnable()
    {
        _playButton.onClick.AddListener(OnPlayButton);
        _shopButton.onClick.AddListener(OnShopButton);
        _rankingButton.onClick.AddListener(OnRankingButton);
        _settingButton.onClick.AddListener(OnSettingButton);
        _findSpawnEnemiesButton.onClick.AddListener(OnFindSpawnEnemiesButton);
    }

    private void OnDisable()
    {
        _playButton.onClick.RemoveListener(OnPlayButton);
        _shopButton.onClick.RemoveListener(OnShopButton);
        _rankingButton.onClick.RemoveListener(OnRankingButton);
        _settingButton.onClick.RemoveListener(OnSettingButton);
        _findSpawnEnemiesButton.onClick.AddListener(OnFindSpawnEnemiesButton);
    }

    public void OnPlayButton()
    {
        PlayButtonClick?.Invoke();
    }

    public void OnShopButton()
    {
        ShopButtonClick?.Invoke();
    }

    public void OnRankingButton()
    {
        RankingButtonClick?.Invoke();
    }

    public void OnSettingButton()
    {
        SettingButtonClick?.Invoke();
    }

    public void OnFindSpawnEnemiesButton()
    {
        FindSpawnEnemiesButtonClick?.Invoke();
    }
}
