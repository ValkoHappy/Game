using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class HealthContainer : MonoBehaviour
{
    [SerializeField] private int _health;

    private int _maxHealth;
    public int Health => _health;
    public event UnityAction<int> HealthChanged;
    public event UnityAction Died;

    private void Start()
    {
        _maxHealth = _health;
    }

    public void TakeDamage(int damageAmount)
    {
        _health -= damageAmount;

        if (_health <= 0)
        {
            _health = 0;
            Died?.Invoke();
        }

        HealthChanged?.Invoke(_health);
    }

    public void ResetHealth()
    {
        _health = _maxHealth;
    }
}
