using UnityEngine;
using UnityEngine.Events;
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

    public event UnityAction PlayButtonClick;
    public event UnityAction ShopButtonClick;
    public event UnityAction LeaderboardButtonClick;
    public event UnityAction SettingButtonClick;
    public event UnityAction FindSpawnEnemiesButtonClick;

    private void OnEnable()
    {
        _lobbyCameraAnimation.AnimationIsFinished += OpenScreen;

        _playButton.onClick.AddListener(OnPlayButton);
        _shopButton.onClick.AddListener(OnShopButton);
        _leaderboardButton.onClick.AddListener(OnLeaderboardButton);
        _settingButton.onClick.AddListener(OnSettingButton);
        _findSpawnEnemiesButton.onClick.AddListener(OnFindSpawnEnemiesButton);
    }

    private void OnDisable()
    {
        _lobbyCameraAnimation.AnimationIsFinished -= OpenScreen;

        _playButton.onClick.RemoveListener(OnPlayButton);
        _shopButton.onClick.RemoveListener(OnShopButton);
        _leaderboardButton.onClick.RemoveListener(OnLeaderboardButton);
        _settingButton.onClick.RemoveListener(OnSettingButton);
        _findSpawnEnemiesButton.onClick.AddListener(OnFindSpawnEnemiesButton);
    }

    private void OnPlayButton()
    {
        PlayButtonClick?.Invoke();
        _enemyHandler.OnEnemies();
        _starsScore.CloseStars();
        _starsScore.RemoveAllBuildingsDiedCount();
        _groundAudio.OnFightClip();
    }

    private void OnShopButton()
    {
        ShopButtonClick?.Invoke();
    }

    private void OnLeaderboardButton()
    {
        LeaderboardButtonClick?.Invoke();
    }

    private void OnSettingButton()
    {
        SettingButtonClick?.Invoke();
    }

    private void OnFindSpawnEnemiesButton()
    {
        FindSpawnEnemiesButtonClick?.Invoke();
        _movingCameraSpawnEnemies.RotationCamera();
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
}
