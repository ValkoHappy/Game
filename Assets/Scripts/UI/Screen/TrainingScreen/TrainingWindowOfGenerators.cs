using System;
using Scripts.Build;
using UnityEngine;
using UnityEngine.UI;

namespace Scripts.UI.Screen.TrainingScreen
{
    public class TrainingWindowOfGenerators : UIScreenAnimator
    {
        private const string Oil = "Oil";

        [SerializeField] private GameObject _indicator;
        [SerializeField] private Button _resumeButton;
        [SerializeField] private GameObject _shop;
        [SerializeField] private BuildingsGrid _buildingGrid;

        public event Action Resumed;

        private void OnEnable()
        {
            _resumeButton.onClick.AddListener(OnResumeButtonClick);
            _buildingGrid.BuildingSupplied += OnOpen;
        }

        private void OnDisable()
        {
            _resumeButton.onClick.RemoveListener(OnResumeButtonClick);
            _buildingGrid.BuildingSupplied -= OnOpen;
        }

        private void OnOpen(Build.Building building)
        {
            if (building.tag == Oil)
            {
                _shop.SetActive(false);
                OnOpen();

                if (_indicator != null)
                    _indicator.SetActive(true);
            }
        }

        private void OnResumeButtonClick()
        {
            _shop.SetActive(true);
            Resumed?.Invoke();
            OnClose();

            if (_indicator != null)
                _indicator.SetActive(false);
        }
    }
}