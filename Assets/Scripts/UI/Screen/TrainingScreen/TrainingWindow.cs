using System;
using UnityEngine;
using UnityEngine.UI;

public class TrainingWindow : UIScreenAnimator
{
    [SerializeField] private GameObject _indicator;
    [SerializeField] private Button _openPanelButton;
    [SerializeField] private Button _resumeButton;

    public event Action Resumed;

    private void OnEnable()
    {
        _resumeButton.onClick.AddListener(OnResumeButtonClick);

        if (_openPanelButton != null)
            _openPanelButton.onClick.AddListener(OnOpen);
    }

    private void OnDisable()
    {
        _resumeButton.onClick.RemoveListener(OnResumeButtonClick);

        if (_openPanelButton != null)
            _openPanelButton.onClick.RemoveListener(OnOpen);
    }

    public override void OnOpen()
    {
        base.OnOpen();

        if(_indicator != null)
            _indicator.SetActive(true);
    }

    private void OnResumeButtonClick()
    {
        Resumed?.Invoke();
        OnClose();

        if (_indicator != null)
            _indicator.SetActive(false);
    }
}
