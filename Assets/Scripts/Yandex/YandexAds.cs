using Agava.YandexGames;
using System;
using UnityEngine;

public class YandexAds : MonoBehaviour
{
    [SerializeField] private SoundSettings _soundSettings;

    private float _minScale = 0f;
    private float _maxScale = 1f;

    public event Action RewardShown;

    public void ShowInterstitial()
    {
#if UNITY_WEBGL && !UNITY_EDITOR
        InterstitialAd.Show(OnAdOpen, OnIterstitialAddClose);
#endif
    }

    public void OnAdOpen()
    {
        Time.timeScale = _minScale;
        _soundSettings.Mute();
    }

    public void OnIterstitialAddClose(bool value)
    {
        Time.timeScale = _maxScale;
        _soundSettings.Load();
    }
}
