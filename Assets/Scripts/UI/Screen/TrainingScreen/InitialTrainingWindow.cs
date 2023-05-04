using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class InitialTrainingWindow : UIScreenAnimator
{
    [SerializeField] private Button _refuseToStudyButton;
    [SerializeField] private Button _resumeButton;
    [SerializeField] private YandexAds _yandexAds;

    public event UnityAction ResumeButtonClick;
    public event UnityAction RefuseToStudyButtonClick;

    private void OnEnable()
    {
        _resumeButton.onClick.AddListener(OnResumeButton);
        _refuseToStudyButton.onClick.AddListener(OnRefuseToStudyButtonClick);
    }

    private void OnDisable()
    {
        _resumeButton.onClick.RemoveListener(OnResumeButton);
        _refuseToStudyButton.onClick.RemoveListener(OnRefuseToStudyButtonClick);
    }

    public void OnOpenScreen()
    {
        OpenScreen();
    }

    public void OnRefuseToStudyButtonClick()
    {
        _yandexAds.ShowInterstitial();
        RefuseToStudyButtonClick?.Invoke();
    }

    private void OnResumeButton()
    {
        ResumeButtonClick?.Invoke();
        CloseScreen();
    }
}
