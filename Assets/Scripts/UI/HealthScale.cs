using Scripts.Build;
using Scripts.Health;
using Scripts.UI.Screen;
using UnityEngine;
using UnityEngine.UI;

namespace Scripts.UI
{
    public class HealthScale : UIScreenAnimator
    {
        [SerializeField] private HealthHandler _healthContainer;
        [SerializeField] private PeacefulConstruction _peacefulConstruction;
        [SerializeField] private CanvasGroup _canvasGroup;
        [SerializeField] private Slider _slider;

        private float _currentVelocity;
        private float _maxHealthTransitionTime = 100;

        private void Start()
        {
            _slider.maxValue = _healthContainer.MaxHealth;
            _slider.value = _slider.maxValue;
        }

        private void OnEnable()
        {
            _healthContainer.HealthChanged += OnChangeHealth;
            _healthContainer.MaxHealthChanged += OnSetMaxValue;
            _peacefulConstruction.BuildingRestored += OnSetMaxValue;
        }

        private void OnDisable()
        {
            _healthContainer.HealthChanged -= OnChangeHealth;
            _healthContainer.MaxHealthChanged -= OnSetMaxValue;
            _peacefulConstruction.BuildingRestored -= OnSetMaxValue;
        }

        public void OnSetMaxValue()
        {
            _slider.value = _slider.maxValue;
            OnClose();
        }

        private void OnChangeHealth(int health)
        {
            OnOpen();
            float currentHealth = Mathf.SmoothDamp(_slider.value, health, ref _currentVelocity, _maxHealthTransitionTime * Time.deltaTime);
            _slider.value = health;

            if (_slider.value == _slider.minValue)
                OnClose();
        }
    }
}