using Scripts.UI;
using System;
using UnityEngine;
using YG;

namespace Scripts.Yandex
{
    public class YandexAds : MonoBehaviour
    {
        [SerializeField] private GameStopControl _gameStopControl;
        [SerializeField] private bool _isMainMenuScene = false;

        private bool _isPlaying = false;
        private string _currentId = string.Empty;

        public event Action<string> ShownAd;
        public event Action<string> Rewarded;

        public bool IsPlaying => _isPlaying;

        private void OnEnable()
        {
            YandexGame.OpenFullAdEvent += OnOpenFullAd;
            YandexGame.CloseFullAdEvent += OnCloseFullAd;

            YandexGame.OpenVideoEvent += OnOpenVideoAd;
            YandexGame.CloseVideoEvent += OnCloseVideoAd;
        }

        private void OnDisable()
        {
            YandexGame.OpenFullAdEvent -= OnOpenFullAd;
            YandexGame.CloseFullAdEvent -= OnCloseFullAd;

            YandexGame.OpenVideoEvent -= OnOpenVideoAd;
            YandexGame.CloseVideoEvent -= OnCloseVideoAd;
        }

        public void ShowInterstitial(/*string adId*/)
        {
            if (!_isPlaying)
            {
                YandexGame.FullscreenShow();
                //_currentId = adId;
            }
        }

        public void ShowRewardAd(int index/*, string adId*/)
        {
            if (!_isPlaying)
            {
                YandexGame.RewVideoShow(index);
                //_currentId = adId;
            }
        }

        private void OnOpenFullAd()
        {
            Open(_currentId, SDKConstants.Inter);
        }

        private void OnCloseFullAd()
        {
            Close(SDKConstants.Inter);
        }

        private void OnOpenVideoAd()
        {
            Open(_currentId, SDKConstants.Reward);
        }

        private void OnCloseVideoAd()
        {
            Close(SDKConstants.Reward);
        }

        public void Open(string adId, string sourseId)
        {
            _isPlaying = true;

            ShownAd?.Invoke(adId);

            _gameStopControl.AddPauseSourse(new PauseSourse(sourseId));

            //if (_isMainMenuScene || (_pauseScreen != null && _pauseScreen.IsOpenPanel))
            //    return;

            YandexGame.GameplayStop();
        }

        public void Close(string sourseId)
        {
            _gameStopControl.RemovePauseSourse(new PauseSourse(sourseId));

            _isPlaying = false;

            YandexGame.GameplayStart();
        }

        public void OnIterstitialAddClose(bool value)
        {
            Close(SDKConstants.Inter);
        }
    }
}