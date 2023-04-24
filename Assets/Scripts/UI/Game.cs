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
    [SerializeField] private EnemyHandler _enemyManager;
    [SerializeField] private BuildingsHandler _buildingsManager;
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
        _groundAudio.OnÑalmClip();

    }

    private void OnEnable()
    {
        _limbCameraAnimation.AnimationIsFinished += OnOpenMainMenu;
        _limbCameraAnimation.AnimationIsFinished += MovingCamera;

        _enemyManager.AllEnemiesKilled += OnAllEnemiesKilled;
        _buildingsManager.AllBuildingsBroked += OnAllBuildingsDestroyed;

        _mainMenuScreen.PlayButtonClick += OnStartGame;
        _mainMenuScreen.ShopButtonClick += OnShopScreen;
        _mainMenuScreen.SettingButtonClick += OnSettingMenuScreen;
        _mainMenuScreen.FindSpawnEnemiesButtonClick += MovingCamera;
        _mainMenuScreen.LeaderboardButtonClick += OnOpenLeaderboardScreen;

        _shopScreen.ExitButtonClick += OnMainMenuScreenAfterShop;

        _settingMenuScreen.ExitButtonClick +=  _settingMenuScreen.CloseScreen;

        _victoryScreen.ResumeButtonClick += ÑontinueAfterWinning;
        _victoryScreen.BonusButtonClick += ÑontinueAfterWinningForAdvertising;
        _victoryScreen.BonusButtonClick += OnMenuAfterFightScreen;
        _victoryScreen.ResumeButtonClick += OnMenuAfterFightScreen;

        _featScreen.ResumeButtonClick += OnRepeatBattle;
        _featScreen.RestartButtonClick += OnRepeatBattleForAdvertising;

        _buildingsGrid.CreatedBuilding += OnCloseShop;
        _buildingsGrid.RemoveBuilding += OnOpenShop;
        _buildingsGrid.DeliveredBuilding += OnOpenShop;

        _spawner.LevelStarted += _levelReward.ClearSpentResources;

        _spawner.CurrentLevelExceedsCount += OnSwitchingScreen;

        _switchingScreen.SwitchingButtonClick += OnSwitchAnotherMap;

        _buttleScreen.ExitButtonClick += ExitOfFight;
        _buttleScreen.SettingsButtonClick += OnSettingMenuScreen;
    }

    private void OnDisable()
    {
        _limbCameraAnimation.AnimationIsFinished -= OnOpenMainMenu;
        _limbCameraAnimation.AnimationIsFinished -= MovingCamera;

        _enemyManager.AllEnemiesKilled -= OnAllEnemiesKilled;
        _buildingsManager.AllBuildingsBroked -= OnAllBuildingsDestroyed;

        _mainMenuScreen.PlayButtonClick -= OnStartGame;
        _mainMenuScreen.ShopButtonClick -= OnShopScreen;
        _mainMenuScreen.SettingButtonClick -= OnSettingMenuScreen;
        _mainMenuScreen.FindSpawnEnemiesButtonClick -= MovingCamera;
        _mainMenuScreen.LeaderboardButtonClick -= OnOpenLeaderboardScreen;

        _shopScreen.ExitButtonClick -= OnMainMenuScreenAfterShop;

        _settingMenuScreen.ExitButtonClick -= _settingMenuScreen.CloseScreen;

        _victoryScreen.ResumeButtonClick -= ÑontinueAfterWinning;
        _victoryScreen.BonusButtonClick -= ÑontinueAfterWinningForAdvertising;
        _victoryScreen.BonusButtonClick -= OnMenuAfterFightScreen;
        _victoryScreen.ResumeButtonClick -= OnMenuAfterFightScreen;

        _featScreen.ResumeButtonClick -= OnRepeatBattle;
        _featScreen.RestartButtonClick -= OnRepeatBattleForAdvertising;

        _buildingsGrid.CreatedBuilding -= OnCloseShop;
        _buildingsGrid.RemoveBuilding -= OnOpenShop;
        _buildingsGrid.DeliveredBuilding -= OnOpenShop;

        _spawner.LevelStarted -= _levelReward.ClearSpentResources;

        _spawner.CurrentLevelExceedsCount -= OnSwitchingScreen;

        _switchingScreen.SwitchingButtonClick -= OnSwitchAnotherMap;

        _buttleScreen.ExitButtonClick -= ExitOfFight;
        _buttleScreen.SettingsButtonClick -= OnSettingMenuScreen;
    }

    private void OnAllEnemiesKilled()
    {
        _starsScore.ShowStars();
        _levelReward.CalculateReward();
        _victoryScreen.OpenScreen();
        //_victoryScreen.OnStartEffect();
        _spawner.NextLevel();
        _buttleScreen.CloseScreen();
        _groundAudio.OnÑalmClip();
    }

    private void OnAllBuildingsDestroyed()
    {
        _featScreen.OpenScreen();
        _buttleScreen.CloseScreen();
        _groundAudio.OnÑalmClip();
    }

    private void OnStartGame()
    {
        _mainMenuScreen.CloseScreen();
        _enemyManager.OnEnemies();
        _starsScore.CloseStars();
        _starsScore.RemoveAllBuildingsDiedCount();
        //_mobileControllerDownScreen.Open();
        _buttleScreen.OpenScreen();
        _groundAudio.OnFightClip();
    }

    private void OnShopScreen()
    {
        _mainMenuScreen.CloseScreen();
        _shopScreen.OpenScreen();
        _mobileControllerDownScreen.CloseScreen();
    }

    private void OnCloseShop()
    {
        _shopScreen.CloseScreen();
        _mobileControllerDownScreen.OpenScreen();
    }

    private void OnOpenShop()
    {
        _mobileControllerDownScreen.CloseScreen();
        _shopScreen.OpenScreen();
    }

    private void OnOpenMainMenu()
    {
        _mainMenuScreen.OpenScreen();
        _mobileControllerDownScreen.OpenScreen();
    }

    private void OnMainMenuScreenAfterShop()
    {
        _shopScreen.CloseScreen();
        _mainMenuScreen.OpenScreen();
        _mobileControllerDownScreen.OpenScreen();
    }

    private void ExitOfFight()
    {
        _enemyManager.OnDestroyEnemies();
        _buildingsManager.OnCreateSavedBuildings();
        _buttleScreen.CloseScreen();
        _mainMenuScreen.OpenScreen();
        _spawner.StartLevel();
        _groundAudio.OnÑalmClip();
        _yandexAds.ShowInterstitial();
    }

    private void OnMenuAfterFightScreen()
    {
        _spawner.ShowLevel();
        _enemyManager.OnDestroyEnemies();
        _buildingsManager.OnDestroyAllBuildings();
        _buildingsGrid.RemoveGrid();
        _victoryScreen.CloseScreen();
        _mainMenuScreen.OpenScreen();
        _buildingsGrid.CreateTowerHall();
        _saveSystem.Save();
        if (_spawner.ChecForMaximumLevel())
            _saveSystem.ResetLevel();
        _spawner.StartLevel();
        _movingCameraSpawnEnemies.RotationCamera();
        if (_trainingScreen != null)
            _trainingScreen.OpenEndTutorialWindow();
    }

    private void ÑontinueAfterWinning()
    {
        _levelReward.ClaimReward();
    }

    private void ÑontinueAfterWinningForAdvertising()
    {
        _levelReward.ClaimDoubleReward();
    }

    private void OnRepeatBattleForAdvertising()
    {
        _yandexAds.ShowRewardAd();
        _enemyManager.OnDestroyEnemies();
        _buildingsManager.OnCreateSavedBuildings();
        _featScreen.CloseScreen();
        _mainMenuScreen.OpenScreen();
        _spawner.StartLevel();
    }

    private void OnRepeatBattle()
    {
        //_yandexAds.ShowInterstitial();
        _buildingsManager.OnDestroyAllBuildings();
        _buildingsGrid.RemoveGrid();
        _enemyManager.OnDestroyEnemies();
        _featScreen.CloseScreen();
        _mainMenuScreen.OpenScreen();
        _buildingsGrid.CreateTowerHall();
        _levelReward.ReturnSpentResources();
        _spawner.StartLevel();
    }

    private void OnSettingMenuScreen()
    {
        _settingMenuScreen.OpenScreen();
    }

    private void OnSwitchingScreen()
    {
        _switchingScreen.OpenScreen();
    }

    private void OnSwitchAnotherMap()
    {
        _switchingScreen.CloseScreen();
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
