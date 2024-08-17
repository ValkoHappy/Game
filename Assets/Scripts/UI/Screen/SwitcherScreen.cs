using System;
using System.Collections.Generic;
using Scripts.SO;
using UnityEngine;
using UnityEngine.UI;

namespace Scripts.UI.Screen
{
    public class SwitcherScreen : UIScreenAnimator
    {
        [SerializeField] private Button _switchingButton;
        [SerializeField] private List<Goods> _buildings;
        [SerializeField] private BuilderView _builderView;
        [SerializeField] private Transform _itenContainer;

        [SerializeField] private Spawner.Spawner _spawner;

        public event Action Switching;

        private void OnEnable()
        {
            _spawner.MaximumLevelChanged += OnOpen;
            _switchingButton.onClick.AddListener(OnSwitchingButtonClick);
        }

        private void OnDisable()
        {
            _spawner.MaximumLevelChanged -= OnOpen;
            _switchingButton.onClick.RemoveListener(OnSwitchingButtonClick);
        }

        private void Start()
        {
            for (int i = 0; i < _buildings.Count; i++)
            {
                AddItem(_buildings[i]);
            }
        }

        public void OnSwitchingButtonClick()
        {
            Switching?.Invoke();
            _spawner.SwitchAnotherMap();
            OnClose();
        }

        private void AddItem(Goods building)
        {
            var view = Instantiate(_builderView, _itenContainer);
            view.Render(building);
        }
    }
}