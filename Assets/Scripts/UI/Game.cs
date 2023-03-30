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

    [SerializeField] private Spawner _spawner;
    [SerializeField] private EnemyManager _enemyManager;
    [SerializeField] private BuildingsManager _buildingsManager;
    [SerializeField] private BuildingsGrid _buildingsGrid;
    [SerializeField] private StarsScore _starsScore;
    [SerializeField] private LobbyCameraAnimation _limbCameraAnimation;
    [SerializeField] private LevelReward _levelReward;
    [SerializeField] private SaveSystem _saveSystem;
    [SerializeField] private YandexAds _yandexAds;

    private void Awake()
    {
        _saveSystem.Load();
    }

    private void Start()
    {
        _mainMenuScreen.Close();
        _shopScreen.Close();
        _settingMenuScreen.Close();
        _spawner.ShowLevel();
        _spawner.StartLevel();

    }

    private void OnEnable()
    {
        _limbCameraAnimation.AnimationIsFinished += OnOpenMainMenu;

        _enemyManager.AllEnemiesKilled += OnAllEnemiesKilled;
        _buildingsManager.AllBuildingsDestroyed += OnAllBuildingsDestroyed;

        _mainMenuScreen.PlayButtonClick += OnStartGame;
        _mainMenuScreen.ShopButtonClick += OnShopScreen;
        _mainMenuScreen.SettingButtonClick += OnSettingMenuScreen;

        _shopScreen.ExitButtonClick += OnMainMenuScreen;

        _settingMenuScreen.ExitButtonClick += OnMainMenuScreen;

        _victoryScreen.ResumeButtonClick += OnMenuAfterFightScreen;

        _featScreen.ResumeButtonClick += OnRepeatBattle;
        _featScreen.RestartButtonClick += OnRepeatBattleForAdvertising;

        _buildingsGrid.CreatedBuilding += OnCloseShop;
        _buildingsGrid.RemoveBuilding += OnOpenShop;
        _buildingsGrid.DeliveredBuilding += OnOpenShop;

        _spawner.LevelStarted += _levelReward.ClearContainerSpent;

        _spawner.ÑurrentLevelExceedsCount += OnSwitchingScreen;

        _switchingScreen.SwitchingButtonClick += OnSwitchAnotherMap;

        _buttleScreen.ExitButtonClick += ExitOfFight;
    }

    private void OnDisable()
    {
        _limbCameraAnimation.AnimationIsFinished -= OnOpenMainMenu;

        _enemyManager.AllEnemiesKilled -= OnAllEnemiesKilled;
        _buildingsManager.AllBuildingsDestroyed -= OnAllBuildingsDestroyed;

        _mainMenuScreen.PlayButtonClick -= OnStartGame;
        _mainMenuScreen.ShopButtonClick -= OnShopScreen;
        _mainMenuScreen.SettingButtonClick -= OnSettingMenuScreen;

        _shopScreen.ExitButtonClick -= OnMainMenuScreen;

        _settingMenuScreen.ExitButtonClick -= OnMainMenuScreen;

        _victoryScreen.ResumeButtonClick -= OnMenuAfterFightScreen;

        _featScreen.ResumeButtonClick -= OnRepeatBattle;
        _featScreen.RestartButtonClick -= OnRepeatBattleForAdvertising;

        _buildingsGrid.CreatedBuilding -= OnCloseShop;
        _buildingsGrid.RemoveBuilding -= OnOpenShop;
        _buildingsGrid.DeliveredBuilding -= OnOpenShop;

        _spawner.LevelStarted -= _levelReward.ClearContainerSpent;

        _spawner.ÑurrentLevelExceedsCount -= OnSwitchingScreen;

        _switchingScreen.SwitchingButtonClick -= OnSwitchAnotherMap;

        _buttleScreen.ExitButtonClick -= ExitOfFight;
    }

    private void OnAllEnemiesKilled()
    {
        _starsScore.ShowStars();
        _levelReward.CalculateReward();
        _levelReward.GetReward();
        _victoryScreen.Open();
        //_victoryScreen.OnStartEffect();
        _mobileControllerDownScreen.Open();
        _spawner.NextLevel();
        _buttleScreen.Close();
    }

    private void OnAllBuildingsDestroyed()
    {
        _featScreen.Open();
        _mobileControllerDownScreen.Open();
        //_yandexAds.ShowInterstitial();
        _buttleScreen.Close();
    }

    private void OnStartGame()
    {
        _mainMenuScreen.Close();
        _enemyManager.OnEnemies();
        _starsScore.CloseStars();
        _starsScore.RemoveAllBuildingsDiedCount();
        _mobileControllerDownScreen.Open();
        _buttleScreen.Open();
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
        _shopScreen.Open();
        _mobileControllerDownScreen.Close();
    }

    private void OnOpenMainMenu()
    {
        _mainMenuScreen.Open();
        _mobileControllerDownScreen.Open();
    }

    private void OnMainMenuScreen()
    {
        _shopScreen.Close();
        _settingMenuScreen.Close();
        _victoryScreen.Close();
        _featScreen.Close();
        _mainMenuScreen.Open();
        _mobileControllerDownScreen.Open();

        //_yandexAds.ShowInterstitial();
    }

    private void ExitOfFight()
    {
        _enemyManager.OnDestroyEnemies();
        _buildingsManager.OnCreateSavedBuildings();
        _buttleScreen.Close();
        _mainMenuScreen.Open();
        _mobileControllerDownScreen.Open();
        _spawner.StartLevel();
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
        _mobileControllerDownScreen.Open();
        _spawner.StartLevel();
    }

    private void OnRepeatBattleForAdvertising()
    {
        //_yandexAds.ShowRewardAd();
        _enemyManager.OnDestroyEnemies();
        _buildingsManager.OnCreateSavedBuildings();
        _featScreen.Close();
        _mainMenuScreen.Open();
        _mobileControllerDownScreen.Open();
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
        _mobileControllerDownScreen.Open();
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
}
