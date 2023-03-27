using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthScale : MonoBehaviour
{
    [SerializeField] private HealthContainer _healthContainer;
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
    }

    private void OnDisable()
    {
        _healthContainer.HealthChanged -= ChangeHealth;
        _healthContainer.MaxHealthChanged -= OnMaxHealthChanged;
    }

    private void ChangeHealth(int health)
    {
        _canvasGroup.alpha = 1;
        float currentHealth = Mathf.SmoothDamp(_slider.value, health, ref currentVelocity, 100 * Time.deltaTime);
        _slider.value = health;

        if(_slider.value == _slider.minValue)
            _canvasGroup.alpha = 0;
    }

    private void OnMaxHealthChanged()
    {
        _slider.value = _slider.maxValue;
        _canvasGroup.alpha = 0;
    }

}
