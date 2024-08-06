using Agava.YandexGames;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.Events;

public class YandexAds : MonoBehaviour
{
    [SerializeField] private SoundSettings _soundSettings;

    public event UnityAction ShowReward;

    public void ShowInterstitial()
    {
#if UNITY_WEBGL && !UNITY_EDITOR
        InterstitialAd.Show(OnAdOpen, OnIterstitialAddClose);
#endif
    }

//    public void ShowRewardAd()
//    {
//#if UNITY_WEBGL && !UNITY_EDITOR
//        VideoAd.Show(() => _soundSettings.Mute(), AddCoin, () => _soundSettings.Load(), null);
//#endif
//        //VideoAd.Show(OnAdOpen, OnAdClose);
//    }

//    public void AddCoin()
//    {
//        ShowReward?.Invoke();
//    }

    public void OnAdOpen()
    {
        Time.timeScale = 0;
        _soundSettings.Mute();
    }

    //public void OnAdClose()
    //{
    //    Time.timeScale = 1;
    //    AudioListener.volume = 1;
    //}

    public void OnIterstitialAddClose(bool value)
    {
        Time.timeScale = 1;
        _soundSettings.Load();
    }
}
