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

    public event UnityAction ResumeButtonClick;
    public event UnityAction RestartButtonClick;

    private void OnEnable()
    {
        _buildingsHandler.AllBuildingsBroked += OpenScreen;

        _resumeButton.onClick.AddListener(OnResumeButton);
        _restartButton.onClick.AddListener(OnRestartButton);
    }

    private void OnDisable()
    {
        _buildingsHandler.AllBuildingsBroked -= OpenScreen;

        _resumeButton.onClick.RemoveListener(OnResumeButton);
        _restartButton.onClick.RemoveListener(OnRestartButton);
    }

    private void OnResumeButton()
    {
        ResumeButtonClick?.Invoke();
        _buildingsHandler.OnDestroyAllBuildings();
        _buildingsGrid.RemoveGrid();
        _enemyHandler.OnDestroyEnemies();
        _buildingsGrid.CreateTowerHall();
        _levelReward.ReturnSpentResources();
        _spawner.StartLevel();
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
