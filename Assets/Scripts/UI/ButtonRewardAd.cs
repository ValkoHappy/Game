using Scripts.Yandex;
using System;
using UnityEngine;
using YG;

namespace Scripts.UI
{
    public class ButtonRewardAd : MonoBehaviour
    {
        private const int RewardIndex = 1;

        [SerializeField] private SoundSettings _soundSettings;
        [SerializeField] private YandexAds _andexAds;

        public event Action Shown;

        private void OnEnable()
        {
            YandexGame.RewardVideoEvent += GetReward;
        }

        private void OnDisable()
        {
            YandexGame.RewardVideoEvent -= GetReward;
        }

        public void ShowRewardAd()
        {
            //#if UNITY_WEBGL && !UNITY_EDITOR
            _andexAds.ShowRewardAd(RewardIndex/*, GameConstants.RebornPlayer*/);
            //#endif
        }

        private void GetReward(int index)
        {
            if (RewardIndex == index)
                Shown?.Invoke();
        }
    }
}