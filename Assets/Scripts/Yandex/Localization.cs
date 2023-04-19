using Lean.Localization;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Localization : MonoBehaviour
{
    [SerializeField] private LeanLocalization _leanLocalization;

    public event UnityAction LanguageChanged;

    private Dictionary<string, string> _language = new()
    {
        { "ru", "Russian" },
        { "en", "English" },
        { "tr", "Turkish" },
    };

    public void SetLanguage(string value)
    {
        if (_language.ContainsKey(value))
        {
            _leanLocalization.SetCurrentLanguage(_language[value]);
            LanguageChanged?.Invoke();
        }
    }
}
