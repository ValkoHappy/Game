using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class DefeatScreen : UIScreenAnimator
{
    [SerializeField] private Button _resumeButton;
    [SerializeField] private Button _restartButton;

    [SerializeField] private YandexAds _yandexAds;
    [SerializeField] private EnemyHandler _enemyHandler;
    [SerializeField] private BuildingsHandler _buildingsHandler;
    [SerializeField] private BuildingsGrid _buildingsGrid;
    [SerializeField] private Spawner _spawner;
    [SerializeField] private LevelReward _levelReward;
    [SerializeField] private StarsScore _starsScore;
    [SerializeField] private SaveSystem _saveSystem;

    private int _counter = 0;
    private int numberOfRepetitions = 1;

    public event UnityAction ResumeButtonClick;
    public event UnityAction RestartButtonClick;

    private void OnEnable()
    {
        _buildingsHandler.AllBuildingsBroked += OpenDefeatScreen;
        _resumeButton.onClick.AddListener(OnResumeButton);
        _restartButton.onClick.AddListener(OnRestartButton);
    }

    private void OnDisable()
    {
        _buildingsHandler.AllBuildingsBroked -= OpenDefeatScreen;
        _resumeButton.onClick.RemoveListener(OnResumeButton);
        _restartButton.onClick.RemoveListener(OnRestartButton);
    }

    private void OpenDefeatScreen()
    {
        OpenScreen();
        if (_counter >= numberOfRepetitions)
        {
            _yandexAds.ShowInterstitial();
            _counter = 0;
        }
        else
        {
            _counter++;
        }
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

    private void OnRestartButton()
    {
        RestartButtonClick?.Invoke();
        _yandexAds.ShowRewardAd();
        _enemyHandler.OnDestroyEnemies();
        _buildingsHandler.OnCreateSavedBuildings();
        _spawner.StartLevel();
    }
}
