using RTS_Cam;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Game : MonoBehaviour
{
    [SerializeField] private MainMenuScreen _mainMenuScreen;
    [SerializeField] private ShopScreen _shopScreen;
    [SerializeField] private SettingMenuScreen _settingMenuScreen;

    [SerializeField] private RTS_Camera _rTS_Camera;
    [SerializeField] private Spawner _spawner;

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
        _mainMenuScreen.PlayButtonClick += OnStartGame;
        _mainMenuScreen.ShopButtonClick += OnShopScreen;
        _mainMenuScreen.SettingButtonClick += OnSettingMenuScreen;

        _shopScreen.ExitButtonClick += OnMainMenuScreen;

        _settingMenuScreen.ExitButtonClick += OnMainMenuScreen;
    }

    private void OnDisable()
    {
        _mainMenuScreen.PlayButtonClick -= OnStartGame;
        _mainMenuScreen.ShopButtonClick -= OnShopScreen;
        _mainMenuScreen.SettingButtonClick -= OnSettingMenuScreen;

        _shopScreen.ExitButtonClick -= OnMainMenuScreen;

        _settingMenuScreen.ExitButtonClick -= OnMainMenuScreen;
    }

    private void OnStartGame()
    {
        _mainMenuScreen.Close();
        _spawner.StartSpawn();
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
        _mainMenuScreen.Open();
    }

    private void OnSettingMenuScreen()
    {
        _settingMenuScreen.Open();
    }
}
