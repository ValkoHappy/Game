using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class SwitchingScreen : ScreenUI
{
    [SerializeField] private Button _switchingButton;

    public event UnityAction SwitchingButtonClick;

    private void OnEnable()
    {
        _switchingButton.onClick.AddListener(OnSwitchingButton);
    }

    private void OnDisable()
    {
        _switchingButton.onClick.RemoveListener(OnSwitchingButton);
    }

    public void OnSwitchingButton()
    {
        SwitchingButtonClick?.Invoke();
    }
}
