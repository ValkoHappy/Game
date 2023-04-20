using System.Collections;
using Agava.YandexGames;
using UnityEngine;

public class YandexInitialization : MonoBehaviour
{
    [SerializeField] private Localization _localization;

#if UNITY_WEBGL && !UNITY_EDITOR
    private void Awake()
    {

        YandexGamesSdk.CallbackLogging = true;
    }

    private IEnumerator Start()
    {
        yield return YandexGamesSdk.Initialize(OnInitialized);
    }

    private void OnInitialized()
    {
        _localization.SetLanguage(YandexGamesSdk.Environment.i18n.lang);
    }
#endif
}
