using System;
using Scripts.UI.Screen;
using UnityEngine;
using UnityEngine.UI;

namespace Scripts.UI.Building
{
    public class MovementScreen : UIScreenAnimator
    {
        [SerializeField] private Button _saveButton;
        [SerializeField] private Button _delateButton;

        private MoveSelection _moveSelection;

        public event Action MadeChoiced;

        private void Awake()
        {
            _moveSelection = FindObjectOfType<MoveSelection>();
        }

        private void Start()
        {
            OnOpen();
        }

        private void OnEnable()
        {
            _saveButton.onClick.AddListener(OnSaveButtonClick);
            _delateButton.onClick.AddListener(OnDelateButtonClick);
        }

        private void OnDisable()
        {
            _saveButton.onClick.RemoveListener(OnSaveButtonClick);
            _delateButton.onClick.RemoveListener(OnDelateButtonClick);
        }

        private void OnSaveButtonClick()
        {
            _moveSelection.SetBuildingModeInsert();
            MadeChoiced?.Invoke();
        }

        private void OnDelateButtonClick()
        {
            _moveSelection.SetBuildingModeDelete();
            MadeChoiced?.Invoke();
        }
    }
}