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

    private void OnEnable()
    {
        _mainMenuScreen.PlayButtonClick += OnStartGame;
        _mainMenuScreen.ShopButtonClick += OnShopScreen;
        _mainMenuScreen.SettingButtonClick += OnSettingMenuScreen;
        _mainMenuScreen.LeaderboardButtonClick += OnOpenLeaderboardScreen;
        _shopScreen.ExitButtonClick += OnMainMenuScreenAfterShop;
        _victoryScreen.BonusButtonClick += OnMenuAfterFightScreen;
        _victoryScreen.ResumeButtonClick += OnMenuAfterFightScreen;
        _featScreen.ResumeButtonClick += OnRepeatBattle;
        _featScreen.RestartButtonClick += OnRepeatBattleForAdvertising;

        _buttleScreen.ExitButtonClick += ExitOfFight;
        _buttleScreen.SettingsButtonClick += OnSettingMenuScreen;
    }

    private void OnDisable()
    {
        _mainMenuScreen.PlayButtonClick -= OnStartGame;
        _mainMenuScreen.ShopButtonClick -= OnShopScreen;
        _mainMenuScreen.SettingButtonClick -= OnSettingMenuScreen;
        _mainMenuScreen.LeaderboardButtonClick -= OnOpenLeaderboardScreen;
        _shopScreen.ExitButtonClick -= OnMainMenuScreenAfterShop;
        _victoryScreen.BonusButtonClick -= OnMenuAfterFightScreen;
        _victoryScreen.ResumeButtonClick -= OnMenuAfterFightScreen;
        _featScreen.ResumeButtonClick -= OnRepeatBattle;
        _featScreen.RestartButtonClick -= OnRepeatBattleForAdvertising;

        _buttleScreen.ExitButtonClick -= ExitOfFight;
        _buttleScreen.SettingsButtonClick -= OnSettingMenuScreen;
    }

    private void OnStartGame()
    {
        _mainMenuScreen.CloseScreen();
        _buttleScreen.OpenScreen();
    }

    private void OnShopScreen()
    {
        _mainMenuScreen.CloseScreen();
        _shopScreen.OpenScreen();
        _mobileControllerDownScreen.CloseScreen();
    }

    private void OnMainMenuScreenAfterShop()
    {
        _shopScreen.CloseScreen();
        _mainMenuScreen.OpenScreen();
        _mobileControllerDownScreen.OpenScreen();
    }

    private void ExitOfFight()
    {
        _buttleScreen.CloseScreen();
        _mainMenuScreen.OpenScreen();
    }

    private void OnMenuAfterFightScreen()
    {
        _victoryScreen.CloseScreen();
        _mainMenuScreen.OpenScreen();
    }

    private void OnRepeatBattleForAdvertising()
    {
        _featScreen.CloseScreen();
        _mainMenuScreen.OpenScreen();
    }

    private void OnRepeatBattle()
    {
        _featScreen.CloseScreen();
        _mainMenuScreen.OpenScreen();
    }

    private void OnSettingMenuScreen()
    {
        _settingMenuScreen.OpenScreen();
    }

    private void OnOpenLeaderboardScreen()
    {
        _leaderboardScreen.OpenAuthorizationPanel();
    }
}
