using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class ButtleScreen : ScreenUI
{
    [SerializeField] private Button _exitButton;
    [SerializeField] private Button _settingsButton;

    public event UnityAction ExitButtonClick;
    public event UnityAction SettingsButtonClick;

    private void OnEnable()
    {
        _exitButton.onClick.AddListener(OnExitButton);
        _settingsButton.onClick.AddListener(OnSettingsButton);
    }

    private void OnDisable()
    {
        _exitButton.onClick.RemoveListener(OnExitButton);
        _settingsButton.onClick.RemoveListener(OnSettingsButton);
    }

    public void OnExitButton()
    {
        ExitButtonClick?.Invoke();
    }

    public void OnSettingsButton()
    {
        SettingsButtonClick?.Invoke();
    }
}
