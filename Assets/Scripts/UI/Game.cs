using Scripts.UI.Screen;
using UnityEngine;

namespace Scripts.UI
{
    public class Game : MonoBehaviour
    {
        [SerializeField] private MainMenuScreen _mainMenuScreen;
        [SerializeField] private ShopScreen _shopScreen;
        [SerializeField] private SettingMenuScreen _settingMenuScreen;
        [SerializeField] private VictoryScreen _victoryScreen;
        [SerializeField] private DefeatScreen _featScreen;
        [SerializeField] private MobileControllerScreen _mobileControllerDownScreen;
        [SerializeField] private ButtleScreen _buttleScreen;
        [SerializeField] private LeaderboardScreen _leaderboardScreen;
        [SerializeField] private BuildingsRemovalScreen _buildingsRemovalScreen;

        private void OnEnable()
        {
            _mainMenuScreen.Playing += OnStart;
            _mainMenuScreen.ShopOpening += OnShopScreen;
            _mainMenuScreen.SettingOpening += OnSettingMenuScreen;
            _mainMenuScreen.LeaderboardOpening += OnOpenLeaderboardScreen;
            _shopScreen.Exited += OnMainMenuScreenAfterShop;
            _shopScreen.BuildingsDeleted += OnOpenBuildingsRemovalScreen;
            _victoryScreen.BonusGetted += OnMenuAfterFightScreen;
            _victoryScreen.Resumed += OnMenuAfterFightScreen;
            _featScreen.Resumed += OnRepeatBattle;
            _featScreen.Restarted += OnRepeatBattleForAdvertising;
            _buildingsRemovalScreen.Exited += OnCloseBuildingsRemovalScreen;
            _buttleScreen.Exited += OnExitOfFight;
            _buttleScreen.SettingsOpened += OnSettingMenuScreen;
        }

        private void OnDisable()
        {
            _mainMenuScreen.Playing -= OnStart;
            _mainMenuScreen.ShopOpening -= OnShopScreen;
            _mainMenuScreen.SettingOpening -= OnSettingMenuScreen;
            _mainMenuScreen.LeaderboardOpening -= OnOpenLeaderboardScreen;
            _shopScreen.Exited -= OnMainMenuScreenAfterShop;
            _shopScreen.BuildingsDeleted += OnOpenBuildingsRemovalScreen;
            _victoryScreen.BonusGetted -= OnMenuAfterFightScreen;
            _victoryScreen.Resumed -= OnMenuAfterFightScreen;
            _featScreen.Resumed -= OnRepeatBattle;
            _featScreen.Restarted -= OnRepeatBattleForAdvertising;
            _buildingsRemovalScreen.Exited -= OnCloseBuildingsRemovalScreen;
            _buttleScreen.Exited -= OnExitOfFight;
            _buttleScreen.SettingsOpened -= OnSettingMenuScreen;
        }

        private void OnStart()
        {
            _mainMenuScreen.OnClose();
            _buttleScreen.OnOpen();
        }

        private void OnShopScreen()
        {
            _mainMenuScreen.OnClose();
            _shopScreen.Open();
            _mobileControllerDownScreen.OnClose();
        }

        private void OnMainMenuScreenAfterShop()
        {
            _shopScreen.OnClose();
            _mainMenuScreen.OnOpen();
            _mobileControllerDownScreen.OnOpen();
        }

        private void OnExitOfFight()
        {
            _buttleScreen.OnClose();
            _mainMenuScreen.OnOpen();
        }

        private void OnMenuAfterFightScreen()
        {
            _victoryScreen.OnClose();
            _mainMenuScreen.OnOpen();
        }

        private void OnRepeatBattleForAdvertising()
        {
            _featScreen.OnClose();
            _mainMenuScreen.OnOpen();
        }

        private void OnRepeatBattle()
        {
            _featScreen.OnClose();
            _mainMenuScreen.OnOpen();
        }

        private void OnSettingMenuScreen()
        {
            _settingMenuScreen.OnOpen();
        }

        private void OnOpenLeaderboardScreen()
        {
            _leaderboardScreen.OpenAuthorizationPanel();
        }

        private void OnOpenBuildingsRemovalScreen()
        {
            _shopScreen.OnClose();
            _mobileControllerDownScreen.OnOpen();
            _buildingsRemovalScreen.OnOpen();
        }

        private void OnCloseBuildingsRemovalScreen()
        {
            _mobileControllerDownScreen.OnClose();
            _buildingsRemovalScreen.OnClose();
            _shopScreen.OnOpen();
        }
    }
}