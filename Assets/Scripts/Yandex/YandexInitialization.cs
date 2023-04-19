using System.Collections;
using Agava.YandexGames;
using UnityEngine;
using UnityEngine.Events;

public class YandexInitialization : MonoBehaviour
{
    [SerializeField] private Localization _localization;

    public event UnityAction PlayerAuthorized;
    public event UnityAction Completed;

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
}
