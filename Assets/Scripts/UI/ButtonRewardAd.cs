using System;
using Agava.YandexGames;
using UnityEngine;

namespace Scripts.UI
{
    public class ButtonRewardAd : MonoBehaviour
    {
        [SerializeField] private SoundSettings _soundSettings;

        public event Action Shown;

        public void ShowRewardAd()
        {
#if UNITY_WEBGL && !UNITY_EDITOR
        VideoAd.Show(() => _soundSettings.Mute(), AddCoin, () => _soundSettings.Load(), null);
#endif
        }

        public void AddCoin()
        {
            Shown?.Invoke();
        }
    }
}