using System;
using UnityEngine;
using UnityEngine.UI;

public class InitialTrainingWindow : UIScreenAnimator
{
    [SerializeField] private Button _refuseToStudyButton;
    [SerializeField] private Button _resumeButton;
    [SerializeField] private YandexAds _yandexAds;

    public event Action Resumed;
    public event Action StudyRefused;

    private void OnEnable()
    {
        _resumeButton.onClick.AddListener(OnResumeButtonClick);
        _refuseToStudyButton.onClick.AddListener(OnRefuseToStudyButtonClick);
    }

    private void OnDisable()
    {
        _resumeButton.onClick.RemoveListener(OnResumeButtonClick);
        _refuseToStudyButton.onClick.RemoveListener(OnRefuseToStudyButtonClick);
    }

    public void OnRefuseToStudyButtonClick()
    {
        _yandexAds.ShowInterstitial();
        StudyRefused?.Invoke();
    }

    private void OnResumeButtonClick()
    {
        Resumed?.Invoke();
        OnClose();
    }
}
