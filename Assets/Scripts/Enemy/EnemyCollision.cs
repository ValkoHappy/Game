using System;
using UnityEngine;

[RequireComponent(typeof(HealthHandler), typeof(BoxCollider))]
public class EnemyCollision : MonoBehaviour, IDamageable
{
    private Enemy _enemy;
    private HealthHandler _healthContainer;

    public event Action<EnemyCollision> Died;

    private void Awake()
    {
        _healthContainer = GetComponent<HealthHandler>();
        _enemy = GetComponentInParent<Enemy>();
    }

    private void OnEnable()
    {
        _healthContainer.Died += OnDied;
    }

    private void OnDisable()
    {
        _healthContainer.Died -= OnDied;
    }

    public bool IsAlive()
    {
        if (_healthContainer.Health <= 0)
            return false;
        else
            return true;
    }

    public bool ApplayDamage(Rigidbody rigidbody, int damage, int force)
    {
        if (_enemy.CurrentState != _enemy.BrokenState)
        {
            _enemy.ApplayDamage(rigidbody, damage, force);
            return true;
        }
        return false;
    }

    protected void OnDied()
    {
        enabled = false;
        Died?.Invoke(this);
    }
}
