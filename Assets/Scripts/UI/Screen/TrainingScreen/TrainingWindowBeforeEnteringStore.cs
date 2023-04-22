using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class TrainingWindowBeforeEnteringStore : UIScreenAnimator
{
    [SerializeField] private GameObject _indicator;
    [SerializeField] private Button _openPanelButton;
    [SerializeField] private Button _resumeButton;

    [SerializeField] private MainMenuScreen _menuScreen;

    public event UnityAction ResumeButtonClick;

    private void Start()
    {
        _menuScreen.TurnOffAllButton();
    }

    private void OnEnable()
    {
        _resumeButton.onClick.AddListener(OnResumeButton);
        if (_openPanelButton != null)
            _openPanelButton.onClick.AddListener(OnOpenScreen);
    }

    private void OnDisable()
    {
        _resumeButton.onClick.RemoveListener(OnResumeButton);
        if (_openPanelButton != null)
            _openPanelButton.onClick.RemoveListener(OnOpenScreen);
    }

    public void OnOpenScreen()
    {
        _menuScreen.EnabletShopButton();
        OpenScreen();
        if (_indicator != null)
            _indicator.SetActive(true);
    }

    private void OnResumeButton()
    {
        ResumeButtonClick?.Invoke();
        CloseScreen();
        if (_indicator != null)
            _indicator.SetActive(false);
        _menuScreen.EnabletButtleButton();
    }
}
