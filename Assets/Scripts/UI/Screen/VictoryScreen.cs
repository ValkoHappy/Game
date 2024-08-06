using System;
using UnityEngine;
using UnityEngine.UI;

public class VictoryScreen : UIScreenAnimator
{
    [SerializeField] private Button _resumeButton;
    [SerializeField] private Button _bonusButton;

    [SerializeField] private Spawner _spawner;
    [SerializeField] private EnemyHandler _enemyHandler;
    [SerializeField] private BuildingsHandler _buildingsHandler;
    [SerializeField] private BuildingsGrid _buildingsGrid;
    [SerializeField] private SaveSystem _saveSystem;
    [SerializeField] private MovingCameraSpawnEnemies _movingCameraSpawnEnemies;
    [SerializeField] private LevelReward _levelReward;
    [SerializeField] private StarsScore _starsScore;
    [SerializeField] private YandexAds _yandexAds;
    [SerializeField] private ButtonRewardAd _buttonRewardAd;

    public event Action Resumed;
    public event Action BonusGetted;

    private void OnEnable()
    {
        _enemyHandler.AllEnemiesKilled += OnOpen;

        _resumeButton.onClick.AddListener(OnResumeButton);
        _bonusButton.onClick.AddListener(ReawardAd);

        _buttonRewardAd.Shown += OnBonusButton;
    }

    private void OnDisable()
    {
        _enemyHandler.AllEnemiesKilled -= OnOpen;

        _resumeButton.onClick.RemoveListener(OnResumeButton);
        _bonusButton.onClick.RemoveListener(ReawardAd);

        _buttonRewardAd.Shown -= OnBonusButton;
    }

    private void OnResumeButton()
    {
        _yandexAds.ShowInterstitial();
        Resumed?.Invoke();
        _levelReward.ClaimReward();
        OnMenuAfterFightScreen();
    }

    private void ReawardAd()
    {
        _buttonRewardAd.ShowRewardAd();
    }

    private void OnBonusButton()
    {
        _levelReward.OnClaimDouble();
        BonusGetted?.Invoke();
        OnMenuAfterFightScreen();
    }

    private void OnMenuAfterFightScreen()
    {
        _starsScore.RemoveAllBuildingsCount();
        _spawner.ShowLevel();
        _enemyHandler.OnDestroyEnemies();
        _buildingsHandler.OnDestroyAllBuildings();
        _buildingsGrid.RemoveGrid();
        _buildingsGrid.CreateTowerHall();
        _saveSystem.Save();

        if (_spawner.CheckMaximumLevel())
            _saveSystem.ResetLevel();

        _spawner.StartLevel();
        _movingCameraSpawnEnemies.OnRotationCamera();
    }
}
