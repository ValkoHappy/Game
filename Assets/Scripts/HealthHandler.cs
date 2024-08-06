using System;
using UnityEngine;

public class HealthHandler : MonoBehaviour
{
    [SerializeField] private int _health;

    private int _maxHealth;

    public event Action<int> HealthChanged;
    public event Action MaxHealthChanged;
    public event Action Died;

    public int Health => _health;
    public int MaxHealth => _maxHealth;

    private void Awake()
    {
        _maxHealth = _health;
    }

    public void TakeDamage(int damageAmount)
    {
        if(_health <= 0 || damageAmount <= 0)
            return;

        _health = Math.Max(0, _health - damageAmount);
        HealthChanged?.Invoke(_health);

        if (_health == 0)
            Died?.Invoke();
    }

    public void Clear()
    {
        _health = _maxHealth;
        MaxHealthChanged?.Invoke();
    }
}
