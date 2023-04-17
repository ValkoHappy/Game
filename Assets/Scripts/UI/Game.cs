using Unity.VisualScripting;
using UnityEngine;

public class Game : MonoBehaviour
{
    [SerializeField] private MainMenuScreen _mainMenuScreen;
    [SerializeField] private ShopScreen _shopScreen;
    [SerializeField] private SettingMenuScreen _settingMenuScreen;
    [SerializeField] private VictoryScreen _victoryScreen;
    [SerializeField] private DefeatScreen _featScreen;
    [SerializeField] private MobileControllerScreen _mobileControllerDownScreen;
    [SerializeField] private SwitchingScreen _switchingScreen;
    [SerializeField] private ButtleScreen _buttleScreen;
    [SerializeField] private LeaderboardScreen _leaderboardScreen;

    [SerializeField] private Spawner _spawner;
    [SerializeField] private EnemyManager _enemyManager;
    [SerializeField] private BuildingsManager _buildingsManager;
    [SerializeField] private BuildingsGrid _buildingsGrid;
    [SerializeField] private StarsScore _starsScore;
    [SerializeField] private LobbyCameraAnimation _limbCameraAnimation;
    [SerializeField] private LevelReward _levelReward;
    [SerializeField] private SaveSystem _saveSystem;
    [SerializeField] private YandexAds _yandexAds;
    [SerializeField] private GroundAudio _groundAudio;
    [SerializeField] private MovingCameraSpawnEnemies _movingCameraSpawnEnemies;
    [SerializeField] private TrainingScreen _trainingScreen;

    private void Awake()
    {
        _saveSystem.Load();
    }

    private void Start()
    {
        _spawner.ShowLevel();
        _spawner.StartLevel();
        _groundAudio.On—almClip();

    }

    private void OnEnable()
    {
        _limbCameraAnimation.AnimationIsFinished += OnOpenMainMenu;
        _limbCameraAnimation.AnimationIsFinished += MovingCamera;

        _enemyManager.AllEnemiesKilled += OnAllEnemiesKilled;
        _buildingsManager.AllBuildingsDestroyed += OnAllBuildingsDestroyed;

        _mainMenuScreen.PlayButtonClick += OnStartGame;
        _mainMenuScreen.ShopButtonClick += OnShopScreen;
        _mainMenuScreen.SettingButtonClick += OnSettingMenuScreen;
        _mainMenuScreen.FindSpawnEnemiesButtonClick += MovingCamera;
        _mainMenuScreen.LeaderboardButtonClick += OnOpenLeaderboardScreen;

        _shopScreen.ExitButtonClick += OnMainMenuScreenAfterShop;

        _settingMenuScreen.ExitButtonClick +=  _settingMenuScreen.Close;

        _victoryScreen.ResumeButtonClick += —ontinueAfterWinning;
        _victoryScreen.BonusButtonClick += —ontinueAfterWinningForAdvertising;
        _victoryScreen.BonusButtonClick += OnMenuAfterFightScreen;
        _victoryScreen.ResumeButtonClick += OnMenuAfterFightScreen;

        _featScreen.ResumeButtonClick += OnRepeatBattle;
        _featScreen.RestartButtonClick += OnRepeatBattleForAdvertising;

        _buildingsGrid.CreatedBuilding += OnCloseShop;
        _buildingsGrid.RemoveBuilding += OnOpenShop;
        _buildingsGrid.DeliveredBuilding += OnOpenShop;

        _spawner.LevelStarted += _levelReward.ClearContainerSpent;

        _spawner.—urrentLevelExceedsCount += OnSwitchingScreen;

        _switchingScreen.SwitchingButtonClick += OnSwitchAnotherMap;

        _buttleScreen.ExitButtonClick += ExitOfFight;
        _buttleScreen.SettingsButtonClick += OnSettingMenuScreen;
    }

    private void OnDisable()
    {
        _limbCameraAnimation.AnimationIsFinished -= OnOpenMainMenu;
        _limbCameraAnimation.AnimationIsFinished -= MovingCamera;

        _enemyManager.AllEnemiesKilled -= OnAllEnemiesKilled;
        _buildingsManager.AllBuildingsDestroyed -= OnAllBuildingsDestroyed;

        _mainMenuScreen.PlayButtonClick -= OnStartGame;
        _mainMenuScreen.ShopButtonClick -= OnShopScreen;
        _mainMenuScreen.SettingButtonClick -= OnSettingMenuScreen;
        _mainMenuScreen.FindSpawnEnemiesButtonClick -= MovingCamera;
        _mainMenuScreen.LeaderboardButtonClick -= OnOpenLeaderboardScreen;

        _shopScreen.ExitButtonClick -= OnMainMenuScreenAfterShop;

        _settingMenuScreen.ExitButtonClick -= _settingMenuScreen.Close;

        _victoryScreen.ResumeButtonClick -= —ontinueAfterWinning;
        _victoryScreen.BonusButtonClick -= —ontinueAfterWinningForAdvertising;
        _victoryScreen.BonusButtonClick -= OnMenuAfterFightScreen;
        _victoryScreen.ResumeButtonClick -= OnMenuAfterFightScreen;

        _featScreen.ResumeButtonClick -= OnRepeatBattle;
        _featScreen.RestartButtonClick -= OnRepeatBattleForAdvertising;

        _buildingsGrid.CreatedBuilding -= OnCloseShop;
        _buildingsGrid.RemoveBuilding -= OnOpenShop;
        _buildingsGrid.DeliveredBuilding -= OnOpenShop;

        _spawner.LevelStarted -= _levelReward.ClearContainerSpent;

        _spawner.—urrentLevelExceedsCount -= OnSwitchingScreen;

        _switchingScreen.SwitchingButtonClick -= OnSwitchAnotherMap;

        _buttleScreen.ExitButtonClick -= ExitOfFight;
        _buttleScreen.SettingsButtonClick -= OnSettingMenuScreen;
    }

    private void OnAllEnemiesKilled()
    {
        _starsScore.ShowStars();
        _levelReward.CalculateReward();
        _victoryScreen.Open();
        //_victoryScreen.OnStartEffect();
        _spawner.NextLevel();
        _buttleScreen.Close();
        _groundAudio.On—almClip();
    }

    private void OnAllBuildingsDestroyed()
    {
        _featScreen.Open();
        _buttleScreen.Close();
        _groundAudio.On—almClip();
    }

    private void OnStartGame()
    {
        _mainMenuScreen.Close();
        _enemyManager.OnEnemies();
        _starsScore.CloseStars();
        _starsScore.RemoveAllBuildingsDiedCount();
        //_mobileControllerDownScreen.Open();
        _buttleScreen.Open();
        _groundAudio.OnFightClip();
    }

    private void OnShopScreen()
    {
        _mainMenuScreen.Close();
        _shopScreen.Open();
        _mobileControllerDownScreen.Close();
    }

    private void OnCloseShop()
    {
        _shopScreen.Close();
        _mobileControllerDownScreen.Open();
    }

    private void OnOpenShop()
    {
        _mobileControllerDownScreen.Close();
        _shopScreen.Open();
    }

    private void OnOpenMainMenu()
    {
        _mainMenuScreen.Open();
        _mobileControllerDownScreen.Open();
    }

    private void OnMainMenuScreenAfterShop()
    {
        _shopScreen.Close();
        _mainMenuScreen.Open();
        _mobileControllerDownScreen.Open();
    }

    private void ExitOfFight()
    {
        _enemyManager.OnDestroyEnemies();
        _buildingsManager.OnCreateSavedBuildings();
        _buttleScreen.Close();
        _mainMenuScreen.Open();
        _spawner.StartLevel();
        _groundAudio.On—almClip();
        _yandexAds.ShowInterstitial();
    }

    private void OnMenuAfterFightScreen()
    {
        _spawner.ShowLevel();
        _enemyManager.OnDestroyEnemies();
        _buildingsManager.OnDestroyAllBuildings();
        _buildingsGrid.RemoveGrid();
        _victoryScreen.Close();
        _mainMenuScreen.Open();
        _buildingsGrid.CreateTowerHall();
        _saveSystem.Save();
        if (_spawner.ChecForMaximumLevel())
            _saveSystem.ResetLevel();
        _spawner.StartLevel();
        _movingCameraSpawnEnemies.RotationCamera();
        if (_trainingScreen != null)
            _trainingScreen.OpenEndTutorialWindow();
    }

    private void —ontinueAfterWinning()
    {
        _levelReward.GetReward();
    }

    private void —ontinueAfterWinningForAdvertising()
    {
        _levelReward.GetDoubleReward();
    }

    private void OnRepeatBattleForAdvertising()
    {
        _yandexAds.ShowRewardAd();
        _enemyManager.OnDestroyEnemies();
        _buildingsManager.OnCreateSavedBuildings();
        _featScreen.Close();
        _mainMenuScreen.Open();
        _spawner.StartLevel();
    }

    private void OnRepeatBattle()
    {
        _buildingsManager.OnDestroyAllBuildings();
        _buildingsGrid.RemoveGrid();
        _enemyManager.OnDestroyEnemies();
        _featScreen.Close();
        _mainMenuScreen.Open();
        _buildingsGrid.CreateTowerHall();
        _levelReward.ReturnAfterLosing();
        _spawner.StartLevel();
    }

    private void OnSettingMenuScreen()
    {
        _settingMenuScreen.Open();
    }

    private void OnSwitchingScreen()
    {
        _switchingScreen.Open();
    }

    private void OnSwitchAnotherMap()
    {
        _switchingScreen.Close();
        _spawner.SwitchAnotherMap();
    }

    private void MovingCamera()
    {
        _movingCameraSpawnEnemies.RotationCamera();
    }

    private void OnOpenLeaderboardScreen()
    {
        _leaderboardScreen.OpenAuthorizationPanel();

    }
}
