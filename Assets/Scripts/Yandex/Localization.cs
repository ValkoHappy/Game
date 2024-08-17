using System;
using System.Collections.Generic;
using Lean.Localization;
using UnityEngine;

namespace Scripts.Yandex
{
    public class Localization : MonoBehaviour
    {
        private const string Language = "Language";

        [SerializeField] private LeanLocalization _leanLocalization;

        private string _currentLanguage;

        private Dictionary<string, string> _language = new()
        {
            { "ru", "Russian" },
            { "en", "English" },
            { "tr", "Turkish" },
        };

        public event Action LanguageChanged;

        public void SetLanguage(string value)
        {
            if (_language.ContainsKey(value))
            {
                _leanLocalization.SetCurrentLanguage(_language[value]);
                _currentLanguage = value;
                LanguageChanged?.Invoke();
                PlayerPrefs.SetString(Language, _currentLanguage);
            }
        }
    }
}