using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthScale : ScreenUI
{
    [SerializeField] private HealthContainer _healthContainer;
    [SerializeField] private PeacefulConstruction _peacefulConstruction;
    [SerializeField] private CanvasGroup _canvasGroup;
    [SerializeField] private Slider _slider;

    private float currentVelocity;

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

    private void ChangeHealth(int health)
    {
        _canvasGroup.alpha = 1;
        float currentHealth = Mathf.SmoothDamp(_slider.value, health, ref currentVelocity, 100 * Time.deltaTime);
        _slider.value = health;

        if(_slider.value == _slider.minValue)
            _canvasGroup.alpha = 0;
    }

    public void OnMaxHealthChanged()
    {
        Debug.Log("fdgdf");
        _slider.value = _slider.maxValue;
        _canvasGroup.alpha = 0;
    }

}
