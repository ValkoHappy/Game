using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class FinishScreen : UIScreenAnimator
{
    [SerializeField] private Button _switchingButton;
    [SerializeField] private Spawner _spawner;

    public event Action Switching;

    private void OnEnable()
    {
        _spawner.MaximumLevelChanged += OnOpen;
        _switchingButton.onClick.AddListener(OnSwitchingButton);
    }

    private void OnDisable()
    {
        _spawner.MaximumLevelChanged -= OnOpen;
        _switchingButton.onClick.RemoveListener(OnSwitchingButton);
    }


    public void OnSwitchingButton()
    {
        Switching?.Invoke();
        SceneManager.LoadScene(0);
        OnClose();
    }
}
