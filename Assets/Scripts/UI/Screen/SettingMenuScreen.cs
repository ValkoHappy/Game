using System;
using UnityEngine;
using UnityEngine.UI;

public class SettingMenuScreen : UIScreenAnimator
{
    private const string Ru = "ru";
    private const string En = "en";
    private const string Tr = "tr";

    [SerializeField] private Button _exitButton;
    [SerializeField] private Button _russianButton;
    [SerializeField] private Button _englishButton;
    [SerializeField] private Button _turkishButton;
    [SerializeField] private Localization _localization;

    public event Action ExitButtonClick;

    private void OnEnable()
    {
        _exitButton.onClick.AddListener(OnExitButtonClick);
        _russianButton.onClick.AddListener(OnSetLanguagesRu);
        _englishButton.onClick.AddListener(OnSetLanguagesEn);
        _turkishButton.onClick.AddListener(OnSetLanguagesTr);
    }

    private void OnDisable()
    {
        _exitButton.onClick.RemoveListener(OnExitButtonClick);
        _russianButton.onClick.RemoveListener(OnSetLanguagesRu);
        _englishButton.onClick.RemoveListener(OnSetLanguagesEn);
        _turkishButton.onClick.RemoveListener(OnSetLanguagesTr);
    }

    private void OnExitButtonClick()
    {
        ExitButtonClick?.Invoke();
        OnClose();
    }

    private void OnSetLanguagesRu()
    {
        _localization.SetLanguage(Ru);
    }

    private void OnSetLanguagesEn()
    {
        _localization.SetLanguage(En);
    }

    private void OnSetLanguagesTr()
    {
        _localization.SetLanguage(Tr);
    }
}
