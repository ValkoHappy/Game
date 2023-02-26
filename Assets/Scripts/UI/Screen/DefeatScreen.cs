using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class DefeatScreen : ScreenUI
{
    [SerializeField] private Button _resumeButton;
    [SerializeField] private Button _restartButton;

    public event UnityAction ResumeButtonClick;
    public event UnityAction RestartButtonClick;

    private void OnEnable()
    {
        _resumeButton.onClick.AddListener(OnResumeButton);
        _restartButton.onClick.AddListener(OnRestartButton);
    }

    private void OnDisable()
    {
        _resumeButton.onClick.RemoveListener(OnResumeButton);
        _restartButton.onClick.RemoveListener(OnRestartButton);
    }

    public void OnResumeButton()
    {
        ResumeButtonClick?.Invoke();
    }

    public void OnRestartButton()
    {
        RestartButtonClick?.Invoke();
    }
}
