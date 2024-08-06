using Agava.YandexGames;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.Events;

public class ButtonRewardAd : MonoBehaviour
{
    [SerializeField] private SoundSettings _soundSettings;

    public event UnityAction ShowReward;

    public void ShowRewardAd()
    {
#if UNITY_WEBGL && !UNITY_EDITOR
        VideoAd.Show(() => _soundSettings.Mute(), AddCoin, () => _soundSettings.Load(), null);
#endif
        //VideoAd.Show(OnAdOpen, OnAdClose);
    }

    public void AddCoin()
    {
        ShowReward?.Invoke();
    }
}
