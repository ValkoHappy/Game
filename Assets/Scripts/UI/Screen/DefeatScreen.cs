using System;
using UnityEngine;
using UnityEngine.UI;

public class DefeatScreen : UIScreenAnimator
{
    [SerializeField] private Button _resumeButton;
    [SerializeField] private Button _restartButton;

    [SerializeField] private YandexAds _yandexAds;
    [SerializeField] private ButtonRewardAd _buttonRewardAd;
    [SerializeField] private EnemyHandler _enemyHandler;
    [SerializeField] private BuildingsHandler _buildingsHandler;
    [SerializeField] private BuildingsGrid _buildingsGrid;
    [SerializeField] private Spawner _spawner;
    [SerializeField] private LevelReward _levelReward;
    [SerializeField] private StarsScore _starsScore;
    [SerializeField] private SaveSystem _saveSystem;

    public event Action Resumed;
    public event Action Restarted;

    private void OnEnable()
    {
        _buildingsHandler.BuildingsBroked += OnOpenDefeat;
        _resumeButton.onClick.AddListener(OnResumeButton);
        _restartButton.onClick.AddListener(OnShowRewardAd);

        _buttonRewardAd.Shown += OnRestart;
    }

    private void OnDisable()
    {
        _buildingsHandler.BuildingsBroked -= OnOpenDefeat;
        _resumeButton.onClick.RemoveListener(OnResumeButton);
        _restartButton.onClick.RemoveListener(OnShowRewardAd);

        _buttonRewardAd.Shown -= OnRestart;
    }

    private void OnOpenDefeat()
    {
        _yandexAds.ShowInterstitial();
        OnOpen();
    }

    private void OnResumeButton()
    {
        _starsScore.RemoveAllBuildingsCount();
        Resumed?.Invoke();
        _buildingsHandler.OnDestroyAllBuildings();
        _buildingsGrid.RemoveGrid();
        _enemyHandler.OnDestroyEnemies();
        _buildingsGrid.CreateTowerHall();
        _levelReward.ReturnSpentResources();
        _spawner.StartLevel();
        _saveSystem.Save();
    }

    private void OnShowRewardAd()
    {
        _buttonRewardAd.ShowRewardAd();
    }

    private void OnRestart()
    {
        Restarted?.Invoke();
        _enemyHandler.OnDestroyEnemies();
        _buildingsHandler.OnCreateSavedBuildings();
        _spawner.StartLevel();
    }
}
