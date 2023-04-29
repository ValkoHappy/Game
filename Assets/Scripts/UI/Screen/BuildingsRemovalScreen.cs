using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class BuildingsRemovalScreen : UIScreenAnimator
{
    [SerializeField] private Button _exitButton;
    [SerializeField] private BuildingRemover _removal;

    public event UnityAction ExitButtonClick;

    private void OnEnable()
    {
        _exitButton.onClick.AddListener(OnExitButton);
    }

    private void OnDisable()
    {
        _exitButton.onClick.RemoveListener(OnExitButton);
    }

    private void OnExitButton()
    {
        ExitButtonClick?.Invoke();
        _removal.enabled = false;
    }
}
