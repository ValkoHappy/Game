using System.Collections;
using Agava.YandexGames;
using UnityEngine;

public class YandexInitialization : MonoBehaviour
{
    private const string Language = "Language";

    [SerializeField] private Localization _localization;
    [SerializeField] private YandexAds _yandexAds;

    private float _wait = 0.5f;

#if UNITY_WEBGL && !UNITY_EDITOR
    private void Awake()
    {
        YandexGamesSdk.CallbackLogging = true;
    }

    private IEnumerator Start()
    {
        yield return YandexGamesSdk.Initialize(OnInitialized);
        yield return new WaitForSeconds(_wait);

        if (_yandexAds != null)
            _yandexAds.ShowInterstitial();
    }

    private void OnInitialized()
    {
        if(PlayerPrefs.HasKey(Language))
            _localization.SetLanguage(Language);
        else
            _localization.SetLanguage(YandexGamesSdk.Environment.i18n.lang);
    }
#endif
}
