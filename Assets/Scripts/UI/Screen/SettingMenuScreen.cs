using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class SettingMenuScreen : UIScreenAnimator
{
    [SerializeField] private Button _exitButton;
    [SerializeField] private Button _russianButton;
    [SerializeField] private Button _englishButton;
    [SerializeField] private Button _turkishButton;
    [SerializeField] private Localization _localization;

    private const string ru = "ru";
    private const string en = "en";
    private const string tr = "tr";

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

    private void OnExitButton()
    {
        ExitButtonClick?.Invoke();
        CloseScreen();
    }

    private void SetLanguagesRu()
    {
        _localization.SetLanguage(ru);
    }

    private void SetLanguagesEn()
    {
        _localization.SetLanguage(en);
    }

    private void SetLanguagesTr()
    {
        _localization.SetLanguage(tr);
    }
}
