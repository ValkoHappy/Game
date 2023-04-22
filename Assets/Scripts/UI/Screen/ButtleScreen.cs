using UnityEngine;
using UnityEngine.Events;
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

    public event UnityAction ExitButtonClick;
    public event UnityAction SettingsButtonClick;

    private void OnEnable()
    {
        _enemyHandler.AllEnemiesKilled += CloseScreen;
        _buildingsHandler.AllBuildingsBroked += CloseScreen;

        _exitButton.onClick.AddListener(OnExitButton);
        _settingsButton.onClick.AddListener(OnSettingsButton);
    }

    private void OnDisable()
    {
        _enemyHandler.AllEnemiesKilled -= CloseScreen;
        _buildingsHandler.AllBuildingsBroked -= CloseScreen;

        _exitButton.onClick.RemoveListener(OnExitButton);
        _settingsButton.onClick.RemoveListener(OnSettingsButton);
    }

    private void OnExitButton()
    {
        ExitButtonClick?.Invoke();
        _enemyHandler.OnDestroyEnemies();
        _buildingsHandler.OnCreateSavedBuildings();
        _spawner.StartLevel();
        _groundAudio.On—almClip();
        _yandexAds.ShowInterstitial();
    }

    private void OnSettingsButton()
    {
        SettingsButtonClick?.Invoke();
    }
}
