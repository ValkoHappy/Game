using UnityEngine;
using UnityEngine.UI;

public class GameLoader : MonoBehaviour
{
    private const string Map = "Map";

    [SerializeField] private Button _continueButton;
    [SerializeField] private Button _newPlayButton;
    [SerializeField] private Button _settingButton;
    [SerializeField] private Button _leaderboardButton;
    [SerializeField] private SaveSystem _saveSystem;
    [SerializeField] private SettingMenuScreen _settingMenuScreen;
    [SerializeField] private LeaderboardScreen _leaderboardScreen;
    [SerializeField] private SceneNext _sceneNext;
    [SerializeField] private YandexAds _yandexAds;

    private void Awake()
    {
        _saveSystem.LoadScene();

        if (PlayerPrefs.HasKey(Map))
            _continueButton.gameObject.SetActive(true);
        else
            _continueButton.gameObject.SetActive(false);
    }

    private void OnEnable()
    {
        _saveSystem.SaveNotFounded += OnOffContinueButton;
        _settingMenuScreen.ExitButtonClick += OCloseSettingScreen;
        _settingButton.onClick.AddListener(OnOpenSettingScreen);
        _continueButton.onClick.AddListener(OnOpenPlayMenu);
        _leaderboardButton.onClick.AddListener(OnOpenLeaderboardScreen);
        _newPlayButton.onClick.AddListener(OnResetPlay);
    }

    private void OnDisable()
    {
        _saveSystem.SaveNotFounded -= OnOffContinueButton;
        _settingMenuScreen.ExitButtonClick -= OCloseSettingScreen;
        _settingButton.onClick.RemoveListener(OnOpenSettingScreen);
        _leaderboardButton.onClick.RemoveListener(OnOpenLeaderboardScreen);
        _continueButton.onClick.RemoveListener(OnOpenPlayMenu);
        _newPlayButton.onClick.RemoveListener(OnResetPlay);
    }

    private void OnOffContinueButton()
    {
        _continueButton.gameObject.SetActive(true);
    }

    private void OnOpenPlayMenu()
    {
        _yandexAds.ShowInterstitial();
        _sceneNext.Open();
    }

    private void OnOpenSettingScreen()
    {
        _settingMenuScreen.OnOpen();
    }

    private void OCloseSettingScreen()
    {
        _settingMenuScreen.OnClose();
    }

    private void OnResetPlay()
    {
        _saveSystem.Clear();
        _saveSystem.LoadScene();
        _sceneNext.Open();
    }

    private void OnOpenLeaderboardScreen()
    {
        _leaderboardScreen.OpenAuthorizationPanel();
    }
}
