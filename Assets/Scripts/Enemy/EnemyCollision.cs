using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(HealthContainer), typeof(BoxCollider))]
public class EnemyCollision : MonoBehaviour, IDamageable
{
    private Enemy _enemy;
    private bool _isAlive;
    private HealthContainer _healthContainer;

    public event UnityAction<EnemyCollision> Died;

    public HealthContainer HealthContainer => _healthContainer;

    private void Awake()
    {
        _healthContainer = GetComponent<HealthContainer>();
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
        {
            return _isAlive = false;
        }
        else
        {
            return _isAlive = true;
        }
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
        _isAlive = false;
        Died?.Invoke(this);
    }
}
