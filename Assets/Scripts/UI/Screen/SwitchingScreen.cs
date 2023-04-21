using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class SwitchingScreen : UIScreenAnimator
{
    [SerializeField] private Button _switchingButton;
    [SerializeField] private List<Goods> _buildings;
    [SerializeField] private BuilderView _builderView;
    [SerializeField] private Transform _itenContainer;

    public event UnityAction SwitchingButtonClick;

    private void OnEnable()
    {
        _switchingButton.onClick.AddListener(OnSwitchingButton);
    }

    private void OnDisable()
    {
        _switchingButton.onClick.RemoveListener(OnSwitchingButton);
    }

    private void Start()
    {
        for (int i = 0; i < _buildings.Count; i++)
        {
            AddItem(_buildings[i]);
        }
    }

    public void OnSwitchingButton()
    {
        SwitchingButtonClick?.Invoke();
    }

    private void AddItem(Goods building)
    {
        var view = Instantiate(_builderView, _itenContainer);
        view.Render(building);
    }
}
