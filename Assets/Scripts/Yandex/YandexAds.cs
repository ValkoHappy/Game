using Agava.YandexGames;
using Scripts.UI;
using UnityEngine;

namespace Scripts.Yandex
{
    public class YandexAds : MonoBehaviour
    {
        [SerializeField] private SoundSettings _soundSettings;

        private float _minScale = 0f;
        private float _maxScale = 1f;

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
}