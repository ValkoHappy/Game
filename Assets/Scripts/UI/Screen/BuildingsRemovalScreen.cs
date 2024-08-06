using System;
using UnityEngine;
using UnityEngine.UI;

public class BuildingsRemovalScreen : UIScreenAnimator
{
    [SerializeField] private Button _exitButton;
    [SerializeField] private BuildingRemover _removal;

    public event Action Exited;

    private void OnEnable()
    {
        _exitButton.onClick.AddListener(OnExitButtonClick);
    }

    private void OnDisable()
    {
        _exitButton.onClick.RemoveListener(OnExitButtonClick);
    }

    private void OnExitButtonClick()
    {
        Exited?.Invoke();
        _removal.enabled = false;
    }
}
