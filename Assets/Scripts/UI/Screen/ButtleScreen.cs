using System;
using UnityEngine;
using UnityEngine.UI;

public class ButtleScreen : UIScreenAnimator
{
    [SerializeField] private Button _exitButton;
    [SerializeField] private Button _settingsButton;

    [SerializeField] private EnemyHandler _enemyHandler;
    [SerializeField] private BuildingsHandler _buildingsHandler;
    [SerializeField] private Spawner _spawner;
    [SerializeField] private GroundAudio _groundAudio;
    [SerializeField] private YandexAds _yandexAds;

    public event Action Exited;
    public event Action SettingsOpened;

    private void OnEnable()
    {
        _enemyHandler.AllEnemiesKilled += OnClose;
        _buildingsHandler.BuildingsBroked += OnClose;

        _exitButton.onClick.AddListener(OnExitButtonClick);
        _settingsButton.onClick.AddListener(OnSettingsButtonClick);
    }

    private void OnDisable()
    {
        _enemyHandler.AllEnemiesKilled -= OnClose;
        _buildingsHandler.BuildingsBroked -= OnClose;

        _exitButton.onClick.RemoveListener(OnExitButtonClick);
        _settingsButton.onClick.RemoveListener(OnSettingsButtonClick);
    }

    private void OnExitButtonClick()
    {
        Exited?.Invoke();
        _enemyHandler.OnDestroyEnemies();
        _buildingsHandler.OnCreateSavedBuildings();
        _spawner.StartLevel();
        _groundAudio.On—almClip();
        _yandexAds.ShowInterstitial();
    }

    private void OnSettingsButtonClick()
    {
        SettingsOpened?.Invoke();
    }
}
