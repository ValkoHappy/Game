using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using YG;

namespace Scripts.Yandex
{
    public class YandexInitialization : MonoBehaviour
    {
        private const string Language = "Language";

        [SerializeField] private Localization _localization;
        [SerializeField] private YandexAds _yandexAds;

        private float _delay = 1f;

        //#if UNITY_WEBGL && !UNITY_EDITOR
        private void OnEnable()
        {
            YandexGame.GetDataEvent += OnInitialize;
        }

        private void OnDisable()
        {
            YandexGame.GetDataEvent -= OnInitialize;
        }

        private void OnInitialize()
        {
            //SaveGame.GetCloudSaveData();
            _localization.SetLanguage(YandexGame.lang.ToLower());
            YandexGame.GameplayStop();
            StartCoroutine(SwitchScene());
        }
//#endif

        private IEnumerator SwitchScene()
        {
            yield return new WaitForSeconds(_delay);

            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
    }
}
