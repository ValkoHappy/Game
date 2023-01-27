using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(HealthContainer), typeof(BoxCollider))]
public class EnemyCollision : MonoBehaviour
{
    private bool _isAlive;
    public HealthContainer _healthContainer { get; protected set; }

    public event UnityAction<EnemyCollision> Died;

    private void Awake()
    {
        _healthContainer = GetComponent<HealthContainer>();
    }

    private void OnEnable()
    {
        _healthContainer.Died += OnDied;
    }

    private void OnDisable()
    {
        _healthContainer.Died -= OnDied;
    }

    protected void OnDied()
    {
        enabled = false;

        Died?.Invoke(this);
    }

    public bool IsAlive()
    {
        if (_healthContainer.Health <= 0)
        {
            return _isAlive = false;
        }
        else
        {
            return _isAlive = true;
        }
    }

    public void ApplyDamage(float damage)
    {
        _healthContainer.TakeDamage((int)damage);
    }
}
