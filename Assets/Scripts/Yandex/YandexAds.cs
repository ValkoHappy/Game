#pragma warning disable

using System;
using Agava.YandexGames;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.Events;
using UnityEngine.UI;

public class YandexAds : MonoBehaviour
{
    public void ShowInterstitial()
    {
        InterstitialAd.Show(OnAdOpen, OnIterstitialAddClose);
    }

    public void ShowRewardAd()
    {
        VideoAd.Show(OnAdOpen, OnAdClose);
    }

    public void OnAdOpen()
    {
        Time.timeScale = 0;
        AudioListener.volume = 0;
    }

    public void OnAdClose()
    {
        Time.timeScale = 1;
        AudioListener.volume = 1;
    }

    public void OnIterstitialAddClose(bool value)
    {
        Time.timeScale = 1;
        AudioListener.volume = 1;
    }
}
