using UnityEngine;

public class Game : MonoBehaviour
{
    [SerializeField] private MainMenuScreen _mainMenuScreen;
    [SerializeField] private ShopScreen _shopScreen;
    [SerializeField] private SettingMenuScreen _settingMenuScreen;
    [SerializeField] private VictoryScreen _victoryScreen;
    [SerializeField] private DefeatScreen _featScreen;
    [SerializeField] private MobileControllerScreen _mobileControllerScreen;

    [SerializeField] private Spawner _spawner;
    [SerializeField] private EnemyManager _enemyManager;
    [SerializeField] private BuildingsManager _buildingsManager;
    [SerializeField] private BuildingsGrid _buildingsGrid;
    [SerializeField] private StarsScore _starsScore;
    [SerializeField] private LobbyCameraAnimation _limbCameraAnimation;
    [SerializeField] private AlertScreen _alertScreen;
    [SerializeField] private LevelReward _levelReward;

    private void Start()
    {
        _mainMenuScreen.Close();
        _shopScreen.Close();
        _settingMenuScreen.Close();
        _spawner.ShowLevel();
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
        _buildingsGrid.DeliveredBuilding += OnOpenShop;
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
        _buildingsGrid.DeliveredBuilding -= OnOpenShop;
    }

    private void OnAllEnemiesKilled()
    {
        _starsScore.ShowStars();
        _levelReward.CalculateReward();
        _victoryScreen.Open();
        //_victoryScreen.OnStartEffect();
        _mobileControllerScreen.Close();
        _spawner.NextLevel();
    }

    private void OnAllBuildingsDestroyed()
    {
        _featScreen.Open();
        _mobileControllerScreen.Close();
    }

    private void OnStartGame()
    {
        if (_buildingsManager.IsTownHallBuilt())
        {
            _mainMenuScreen.Close();
            //_buildingsManager.OnSaveBuildings();
            _spawner.StartLevel();
            _starsScore.CloseStars();
            _starsScore.RemoveAllBuildingsDiedCount();
            _mobileControllerScreen.Open();
        }
        else
        {
            _alertScreen.Open();
        }
    }

    private void OnShopScreen()
    {
        _mainMenuScreen.Close();
        _shopScreen.Open();
    }

    private void OnCloseShop()
    {
        _shopScreen.Close();
        _mobileControllerScreen.Open();
    }

    private void OnOpenShop()
    {
        _shopScreen.Open();
        _mobileControllerScreen.Close();
    }

    private void OnOpenMainMenu()
    {
        _mainMenuScreen.Open();
    }

    private void OnMainMenuScreen()
    {
        _shopScreen.Close();
        _settingMenuScreen.Close();
        _victoryScreen.Close();
        _featScreen.Close();
        _mainMenuScreen.Open();
    }
    private void OnMenuAfterFightScreen()
    {
        _spawner.ShowLevel();
        _levelReward.GetReward();
        _enemyManager.OnDestroyEnemies();
        _buildingsManager.OnDestroyAllBuildings();
        _buildingsGrid.RemoveGrid();
        _victoryScreen.Close();
        _mainMenuScreen.Open();
        _spawner.SwitchAnotherMap();
    }

    private void OnRepeatBattleForAdvertising()
    {
        _enemyManager.OnDestroyEnemies();
        _buildingsManager.OnCreateSavedBuildings();
        _featScreen.Close();
        _mainMenuScreen.Open();
    }

    private void OnRepeatBattle()
    {
        _buildingsManager.OnDestroyAllBuildings();
        _buildingsGrid.RemoveGrid();
        _enemyManager.OnDestroyEnemies();
        _featScreen.Close();
        _mainMenuScreen.Open();
        _levelReward.ReturnAfterLosing();
    }

    private void OnSettingMenuScreen()
    {
        _settingMenuScreen.Open();
    }
}
