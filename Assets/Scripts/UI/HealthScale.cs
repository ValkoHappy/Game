using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthScale : UIScreenAnimator
{
    [SerializeField] private HealthContainer _healthContainer;
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
        _healthContainer.HealthChanged += ChangeHealth;
        _healthContainer.MaxHealthChanged += OnMaxHealthChanged;
        _peacefulConstruction.BuildingRestored += OnMaxHealthChanged;
    }

    private void OnDisable()
    {
        _healthContainer.HealthChanged -= ChangeHealth;
        _healthContainer.MaxHealthChanged -= OnMaxHealthChanged;
        _peacefulConstruction.BuildingRestored -= OnMaxHealthChanged;
    }

    public void OnMaxHealthChanged()
    {
        _slider.value = _slider.maxValue;
        CloseScreen();
    }

    private void ChangeHealth(int health)
    {
        OpenScreen();
        float currentHealth = Mathf.SmoothDamp(_slider.value, health, ref _currentVelocity, _maxHealthTransitionTime * Time.deltaTime);
        _slider.value = health;

        if(_slider.value == _slider.minValue)
            CloseScreen();
    }
}
