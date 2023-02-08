using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class HealthContainer : MonoBehaviour, IDamageable
{
    [SerializeField] private int _health;

    public int Health => _health;
    public event UnityAction<int> HealthChanged;
    public event UnityAction Died;

    //public void TakeDamage(int value)
    //{

    //}

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
}
