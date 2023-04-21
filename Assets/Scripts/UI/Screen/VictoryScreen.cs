using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class VictoryScreen : UIScreenAnimator
{
    [SerializeField] private Button _resumeButton;
    [SerializeField] private Button _bonusButton;

    public event UnityAction ResumeButtonClick;
    public event UnityAction BonusButtonClick;

    private void OnEnable()
    {
        _resumeButton.onClick.AddListener(OnResumeButton);
        _bonusButton.onClick.AddListener(OnBonusButton);
    }

    private void OnDisable()
    {
        _resumeButton.onClick.RemoveListener(OnResumeButton);
        _bonusButton.onClick.RemoveListener(OnBonusButton);
    }

    public void OnResumeButton()
    {
        ResumeButtonClick?.Invoke();
    }

    public void OnBonusButton()
    {
        BonusButtonClick?.Invoke();
    }
}
