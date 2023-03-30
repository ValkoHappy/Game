using Lean.Localization;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class SettingMenuScreen : ScreenUI
{
    [SerializeField] private Button _exitButton;
    [SerializeField] private Button _russianButton;
    [SerializeField] private Button _englishButton;
    [SerializeField] private Button _turkishButton;
    [SerializeField] private LeanLocalization _leanLocalization;

    private const string ru = "Russian";
    private const string en = "English";
    private const string tr = "Turkish";

    public event UnityAction ExitButtonClick;

    private void OnEnable()
    {
        _exitButton.onClick.AddListener(OnExitButton);
        _russianButton.onClick.AddListener(SetLanguagesRu);
        _englishButton.onClick.AddListener(SetLanguagesEn);
        _turkishButton.onClick.AddListener(SetLanguagesTr);
    }

    private void OnDisable()
    {
        _exitButton.onClick.RemoveListener(OnExitButton);
        _russianButton.onClick.RemoveListener(SetLanguagesRu);
        _englishButton.onClick.RemoveListener(SetLanguagesEn);
        _turkishButton.onClick.RemoveListener(SetLanguagesTr);
    }

    public void OnExitButton()
    {
        ExitButtonClick?.Invoke();
    }

    private void SetLanguagesRu()
    {
        _leanLocalization.SetCurrentLanguage(ru);
    }

    private void SetLanguagesEn()
    {
        _leanLocalization.SetCurrentLanguage(en);
    }

    private void SetLanguagesTr()
    {
        _leanLocalization.SetCurrentLanguage(tr);
    }
}
