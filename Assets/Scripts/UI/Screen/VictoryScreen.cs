using UnityEngine;
using UnityEngine.Events;
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

    public event UnityAction ResumeButtonClick;
    public event UnityAction BonusButtonClick;

    private void OnEnable()
    {
        _enemyHandler.AllEnemiesKilled += OpenScreen;

        _resumeButton.onClick.AddListener(OnResumeButton);
        _bonusButton.onClick.AddListener(OnBonusButton);
    }

    private void OnDisable()
    {
        _enemyHandler.AllEnemiesKilled -= OpenScreen;

        _resumeButton.onClick.RemoveListener(OnResumeButton);
        _bonusButton.onClick.RemoveListener(OnBonusButton);
    }

    public void OnResumeButton()
    {
        ResumeButtonClick?.Invoke();
        OnMenuAfterFightScreen();
        _levelReward.ClaimReward();
    }

    public void OnBonusButton()
    {
        BonusButtonClick?.Invoke();
        OnMenuAfterFightScreen();
        _levelReward.ClaimDoubleReward();
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
        if (_spawner.ChecForMaximumLevel())
            _saveSystem.ResetLevel();
        _spawner.StartLevel();
        _movingCameraSpawnEnemies.RotationCamera();
    }
}
