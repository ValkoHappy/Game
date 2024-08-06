using UnityEngine;
using UnityEngine.Events;
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

    public event UnityAction ResumeButtonClick;
    public event UnityAction RestartButtonClick;

    private void OnEnable()
    {
        _buildingsHandler.AllBuildingsBroked += OpenDefeatScreen;
        _resumeButton.onClick.AddListener(OnResumeButton);
        _restartButton.onClick.AddListener(OnRestartButtonReward);

        _buttonRewardAd.ShowReward += OnRestartButton;
    }

    private void OnDisable()
    {
        _buildingsHandler.AllBuildingsBroked -= OpenDefeatScreen;
        _resumeButton.onClick.RemoveListener(OnResumeButton);
        _restartButton.onClick.RemoveListener(OnRestartButtonReward);

        _buttonRewardAd.ShowReward -= OnRestartButton;
    }

    private void OpenDefeatScreen()
    {
        _yandexAds.ShowInterstitial();
        OpenScreen();
    }

    private void OnResumeButton()
    {
        _starsScore.RemoveAllBuildingsCount();
        ResumeButtonClick?.Invoke();
        _buildingsHandler.OnDestroyAllBuildings();
        _buildingsGrid.RemoveGrid();
        _enemyHandler.OnDestroyEnemies();
        _buildingsGrid.CreateTowerHall();
        _levelReward.ReturnSpentResources();
        _spawner.StartLevel();
        _saveSystem.Save();
    }

    private void OnRestartButtonReward()
    {
        _buttonRewardAd.ShowRewardAd();
    }

    private void OnRestartButton()
    {
        RestartButtonClick?.Invoke();
        _enemyHandler.OnDestroyEnemies();
        _buildingsHandler.OnCreateSavedBuildings();
        _spawner.StartLevel();
    }
}
