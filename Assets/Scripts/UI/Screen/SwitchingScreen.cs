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

    [SerializeField] private Spawner _spawner;

    public event UnityAction SwitchingButtonClick;

    private void OnEnable()
    {
        _spawner.ÑurrentLevelExceedsCount += OpenScreen;
        _switchingButton.onClick.AddListener(OnSwitchingButton);
    }

    private void OnDisable()
    {
        _spawner.ÑurrentLevelExceedsCount -= OpenScreen;
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
        _spawner.SwitchAnotherMap();
        CloseScreen();
    }

    private void AddItem(Goods building)
    {
        var view = Instantiate(_builderView, _itenContainer);
        view.Render(building);
    }
}
