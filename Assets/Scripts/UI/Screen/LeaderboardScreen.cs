using Agava.YandexGames;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class LeaderboardScreen : ScreenUI
{
    [SerializeField] private Button _exitButton1;
    [SerializeField] private Button _exitButton2;
    [SerializeField] private Button _authorizationButton;
    [SerializeField] private GameObject _authorizationPanel;
    [SerializeField] private GameObject _leaderboardPanel;
    [SerializeField] private TMP_Text[] _ranks;
    [SerializeField] private TMP_Text[] _leaderNames;
    [SerializeField] private TMP_Text[] _scoreList;
    [SerializeField] private string _leaderboardName = "Coins";

    private int _playerScore = 0;

    private void OnEnable()
    {
        _exitButton1.onClick.AddListener(OnExitButton);
        _exitButton2.onClick.AddListener(OnExitButton);
        _authorizationButton.onClick.AddListener(OpenLeaderboardPanel);
    }

    private void OnDisable()
    {
        _exitButton1.onClick.RemoveListener(OnExitButton);
        _exitButton2.onClick.RemoveListener(OnExitButton);
        _authorizationButton.onClick.RemoveListener(OpenLeaderboardPanel);
    }

    public void OnExitButton()
    {
        Close();
        _authorizationPanel.SetActive(true);
        _leaderboardPanel.SetActive(false);
    }

    public void OpenAuthorizationPanel()
    {
        Open();
#if UNITY_WEBGL && !UNITY_EDITOR
        if (!PlayerAccount.IsAuthorized)
#endif
        {
            _authorizationPanel.SetActive(true);
            _leaderboardPanel.SetActive(false);
        }
#if UNITY_WEBGL && !UNITY_EDITOR
        else
        {
            _authorizationPanel.SetActive(false);
            _leaderboardPanel.SetActive(true);
        }
#endif
    }

    private void OpenLeaderboardPanel()
    {
        _authorizationPanel.SetActive(false);
        _leaderboardPanel.SetActive(true);
#if UNITY_WEBGL && !UNITY_EDITOR
        SetLeaderboardScore();
        OpenYandexLeaderboard();
#endif
    }

    public void OpenYandexLeaderboard()
    {
        PlayerAccount.RequestPersonalProfileDataPermission();
        if (!PlayerAccount.IsAuthorized)
            PlayerAccount.Authorize();

        Leaderboard.GetEntries(_leaderboardName, (result) =>
        {

            int leadersNumber = result.entries.Length >= _leaderNames.Length ? _leaderNames.Length : result.entries.Length;
            for (int i = 0; i < leadersNumber; i++)
            {
                string name = result.entries[i].player.publicName;
                if (string.IsNullOrEmpty(name))
                    name = "Anonimus";

                _leaderNames[i].text = name;
                _scoreList[i].text = result.entries[i].formattedScore;
                _ranks[i].text = result.entries[i].rank.ToString();
            }
        });
    }

    public void SetLeaderboardScore()
    {
        if (YandexGamesSdk.IsInitialized)
        {
            Leaderboard.GetPlayerEntry(_leaderboardName, OnSuccessCallback);
        }
    }

    private void OnSuccessCallback(LeaderboardEntryResponse result)
    {
        if (result == null || _playerScore > result.score)
        {
            Leaderboard.SetScore(_leaderboardName, _playerScore);
        }
    }
}
