using Agava.YandexGames;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LeaderboardScreen : UIScreenAnimator
{
    private const string AllGold = "AllGold";
    private const string LeaderboardName = "Coins";
    private const string Anonimus = "Anonimus";

    [SerializeField] private Button _exitButton1;
    [SerializeField] private Button _exitButton2;
    [SerializeField] private Button _authorizationButton;
    [SerializeField] private GameObject _authorizationPanel;
    [SerializeField] private GameObject _leaderboardPanel;
    [SerializeField] private GameObject[] _players;
    [SerializeField] private TMP_Text[] _ranks;
    [SerializeField] private TMP_Text[] _leaderNames;
    [SerializeField] private TMP_Text[] _scoreList;

    private int _playerScore = 0;

    private void Start()
    {
        _playerScore = PlayerPrefs.GetInt(AllGold);
    }

    private void OnEnable()
    {
        _exitButton1.onClick.AddListener(OnExitButton);
        _exitButton2.onClick.AddListener(OnExitButton);
        _authorizationButton.onClick.AddListener(OpenPanel);
    }

    private void OnDisable()
    {
        _exitButton1.onClick.RemoveListener(OnExitButton);
        _exitButton2.onClick.RemoveListener(OnExitButton);
        _authorizationButton.onClick.RemoveListener(OpenPanel);
    }

    public void OnExitButton()
    {
        OnClose();
    }

    public void OpenAuthorizationPanel()
    {
        OnOpen();

#if UNITY_WEBGL && !UNITY_EDITOR
        if (!PlayerAccount.IsAuthorized)
        {
            _authorizationPanel.SetActive(true);
            _leaderboardPanel.SetActive(false);
        }
        else
            Open();
#endif
    }

    public void Open()
    {
        PlayerAccount.RequestPersonalProfileDataPermission();

        if (!PlayerAccount.IsAuthorized)
            PlayerAccount.Authorize();

        Leaderboard.GetEntries(LeaderboardName, (result) =>
        {
            int leadersNumber = result.entries.Length >= _leaderNames.Length ? _leaderNames.Length : result.entries.Length;
            for (int i = 0; i < leadersNumber; i++)
            {
                _players[i].SetActive(true);
                string name = result.entries[i].player.publicName;
                if (string.IsNullOrEmpty(name))
                    name = Anonimus;

                _leaderNames[i].text = name;
                _scoreList[i].text = result.entries[i].formattedScore;
                _ranks[i].text = result.entries[i].rank.ToString();
            }
        });
    }

    public void SetScore()
    {
        if (YandexGamesSdk.IsInitialized)
            Leaderboard.GetPlayerEntry(LeaderboardName, OnSuccessCallback);
    }

    private void OpenPanel()
    {
#if UNITY_WEBGL && !UNITY_EDITOR
        SetLeaderboardScore();
        OpenYandexLeaderboard();

        if (PlayerAccount.IsAuthorized)
        {
            _authorizationPanel.SetActive(false);
            _leaderboardPanel.SetActive(true);
        }
#endif
    }

    private void OnSuccessCallback(LeaderboardEntryResponse result)
    {
        if (result == null || _playerScore > result.score)
            Leaderboard.SetScore(LeaderboardName, _playerScore);
    }
}
