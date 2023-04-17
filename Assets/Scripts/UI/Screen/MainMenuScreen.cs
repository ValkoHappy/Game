using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class MainMenuScreen : ScreenUI
{
    [SerializeField] private Button _playButton;
    [SerializeField] private Button _shopButton;
    [SerializeField] private Button _leaderboardButton;
    [SerializeField] private Button _settingButton;
    [SerializeField] private Button _findSpawnEnemiesButton;

    public event UnityAction PlayButtonClick;
    public event UnityAction ShopButtonClick;
    public event UnityAction LeaderboardButtonClick;
    public event UnityAction SettingButtonClick;
    public event UnityAction FindSpawnEnemiesButtonClick;

    private void OnEnable()
    {
        _playButton.onClick.AddListener(OnPlayButton);
        _shopButton.onClick.AddListener(OnShopButton);
        _leaderboardButton.onClick.AddListener(OnLeaderboardButton);
        _settingButton.onClick.AddListener(OnSettingButton);
        _findSpawnEnemiesButton.onClick.AddListener(OnFindSpawnEnemiesButton);
    }

    private void OnDisable()
    {
        _playButton.onClick.RemoveListener(OnPlayButton);
        _shopButton.onClick.RemoveListener(OnShopButton);
        _leaderboardButton.onClick.RemoveListener(OnLeaderboardButton);
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

    public void OnLeaderboardButton()
    {
        LeaderboardButtonClick?.Invoke();
    }

    public void OnSettingButton()
    {
        SettingButtonClick?.Invoke();
    }

    public void OnFindSpawnEnemiesButton()
    {
        FindSpawnEnemiesButtonClick?.Invoke();
    }

    public void TurnOffAllButton()
    {
        _playButton.enabled = false;
        _shopButton.enabled = false;
        _leaderboardButton.enabled = false;
        _settingButton.enabled = false;
        _findSpawnEnemiesButton.enabled = false;
    }

    public void EnabletShopButton()
    {
        _shopButton.enabled = true;
    }

    public void EnabletButtleButton()
    {
        _shopButton.enabled = false;
        _playButton.enabled = true;
    }

    public void EnabletAllButton()
    {
        _playButton.enabled = true;
        _shopButton.enabled = true;
        _leaderboardButton.enabled = true;
        _settingButton.enabled = true;
        _findSpawnEnemiesButton.enabled = true;
    }
}
