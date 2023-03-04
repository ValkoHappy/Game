using RTS_Cam;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Game : MonoBehaviour
{
    [SerializeField] private MainMenuScreen _mainMenuScreen;
    [SerializeField] private ShopScreen _shopScreen;
    [SerializeField] private SettingMenuScreen _settingMenuScreen;
    [SerializeField] private VictoryScreen _victoryScreen;
    [SerializeField] private DefeatScreen _featScreen;

    [SerializeField] private RTS_Camera _rTS_Camera;
    [SerializeField] private Spawner _spawner;
    [SerializeField] private EnemyManager _enemyManager;
    [SerializeField] private BuildingsManager _buildingsManager;

    private bool _isInitialLaunch = true;

    private void Start()
    {
        _mainMenuScreen.Close();
        _shopScreen.Close();
        _settingMenuScreen.Close();
    }

    private void Update()
    {
        if (_rTS_Camera.enabled == true && _isInitialLaunch == true)
        {
            _mainMenuScreen.Open();
            _isInitialLaunch = false;
        }
    }

    private void OnEnable()
    {
        _enemyManager.AllEnemiesKilled += OnAllEnemiesKilled;
        _buildingsManager.AllBuildingsDestroyed += OnAllBuildingsDestroyed;

        _mainMenuScreen.PlayButtonClick += OnStartGame;
        _mainMenuScreen.ShopButtonClick += OnShopScreen;
        _mainMenuScreen.SettingButtonClick += OnSettingMenuScreen;

        _shopScreen.ExitButtonClick += OnMainMenuScreen;

        _settingMenuScreen.ExitButtonClick += OnMainMenuScreen;

        _victoryScreen.ResumeButtonClick += OnMenuAfterFightScreen;

        _featScreen.ResumeButtonClick += OnMenuAfterFightScreen;
    }

    private void OnDisable()
    {
        _enemyManager.AllEnemiesKilled -= OnAllEnemiesKilled;
        _buildingsManager.AllBuildingsDestroyed -= OnAllBuildingsDestroyed;

        _mainMenuScreen.PlayButtonClick -= OnStartGame;
        _mainMenuScreen.ShopButtonClick -= OnShopScreen;
        _mainMenuScreen.SettingButtonClick -= OnSettingMenuScreen;

        _shopScreen.ExitButtonClick -= OnMainMenuScreen;

        _settingMenuScreen.ExitButtonClick -= OnMainMenuScreen;

        _victoryScreen.ResumeButtonClick -= OnMenuAfterFightScreen;

        _featScreen.ResumeButtonClick -= OnMenuAfterFightScreen;
    }

    private void OnAllEnemiesKilled()
    {
        _victoryScreen.Open();
    }

    private void OnAllBuildingsDestroyed()
    {
        _featScreen.Open();
    }

    private void OnStartGame()
    {
        _mainMenuScreen.Close();
        _buildingsManager.OnSaveBuildings();
        _spawner.StartNextLevel();
    }

    private void OnShopScreen()
    {
        _mainMenuScreen.Close();
        _shopScreen.Open();
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
        _enemyManager.OnDestroyEnemies();
        _buildingsManager.OnCreateSavedBuildings();
        _victoryScreen.Close();
        _featScreen.Close();
        _mainMenuScreen.Open();
    }

    private void OnSettingMenuScreen()
    {
        _settingMenuScreen.Open();
    }
}
