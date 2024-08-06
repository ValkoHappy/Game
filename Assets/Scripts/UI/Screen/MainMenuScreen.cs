using System;
using UnityEngine;
using UnityEngine.UI;

public class MainMenuScreen : UIScreenAnimator
{
    [SerializeField] private Button _playButton;
    [SerializeField] private Button _shopButton;
    [SerializeField] private Button _leaderboardButton;
    [SerializeField] private Button _settingButton;
    [SerializeField] private Button _findSpawnEnemiesButton;

    [SerializeField] private EnemyHandler _enemyHandler;
    [SerializeField] private StarsScore _starsScore;
    [SerializeField] private GroundAudio _groundAudio;
    [SerializeField] private MovingCameraSpawnEnemies _movingCameraSpawnEnemies;
    [SerializeField] private LobbyCameraAnimation _lobbyCameraAnimation;

    public event Action Playing;
    public event Action ShopOpening;
    public event Action LeaderboardOpening;
    public event Action SettingOpening;
    public event Action SpawnEnemiesFinded;

    private void OnEnable()
    {
        _lobbyCameraAnimation.AnimationFinished += OnOpen;

        _playButton.onClick.AddListener(OnPlayButtonClick);
        _shopButton.onClick.AddListener(OnShopButtonClick);
        _leaderboardButton.onClick.AddListener(OnLeaderboardButtonClick);
        _settingButton.onClick.AddListener(OnSettingButtonClick);
        _findSpawnEnemiesButton.onClick.AddListener(OnFindSpawnEnemiesButton);
    }

    private void OnDisable()
    {
        _lobbyCameraAnimation.AnimationFinished -= OnOpen;

        _playButton.onClick.RemoveListener(OnPlayButtonClick);
        _shopButton.onClick.RemoveListener(OnShopButtonClick);
        _leaderboardButton.onClick.RemoveListener(OnLeaderboardButtonClick);
        _settingButton.onClick.RemoveListener(OnSettingButtonClick);
        _findSpawnEnemiesButton.onClick.AddListener(OnFindSpawnEnemiesButton);
    }

    public void TurnOffAllButton()
    {
        _playButton.enabled = false;
        _shopButton.enabled = false;
        _leaderboardButton.enabled = false;
        _settingButton.enabled = false;
        _findSpawnEnemiesButton.enabled = false;
    }

    public void EnabletShopButton()
    {
        _shopButton.enabled = true;
    }

    public void EnabletButtleButton()
    {
        _shopButton.enabled = false;
        _playButton.enabled = true;
    }

    public void EnabletAllButton()
    {
        _playButton.enabled = true;
        _shopButton.enabled = true;
        _leaderboardButton.enabled = true;
        _settingButton.enabled = true;
        _findSpawnEnemiesButton.enabled = true;
    }

    private void OnPlayButtonClick()
    {
        Playing?.Invoke();
        _enemyHandler.OnEnemies();
        _starsScore.Close();
        _starsScore.RemoveAllBuildingsDiedCount();
        _groundAudio.OnFightClip();
    }

    private void OnShopButtonClick()
    {
        ShopOpening?.Invoke();
    }

    private void OnLeaderboardButtonClick()
    {
        LeaderboardOpening?.Invoke();
    }

    private void OnSettingButtonClick()
    {
        SettingOpening?.Invoke();
    }

    private void OnFindSpawnEnemiesButton()
    {
        SpawnEnemiesFinded?.Invoke();
        _movingCameraSpawnEnemies.OnRotationCamera();
    }
}
