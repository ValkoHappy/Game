using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(HealthContainer) /*typeof(BoxCollider)*/)]
public class PeacefulConstruction : MonoBehaviour
{
    public event UnityAction<PeacefulConstruction> Died;

    public event UnityAction Damaged;
    private HealthContainer _healthContainer;
    private bool _isAlive;

    private void Awake()
    {
        _healthContainer = GetComponent<HealthContainer>();
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

    private void OnEnable()
    {
        _healthContainer.Died += OnDied;
    }

    private void OnDisable()
    {
        _healthContainer.Died -= OnDied;
    }

    public void ApplyDamage(float damage)
    {
        _healthContainer.TakeDamage((int)damage);
        Damaged?.Invoke();
    }

    private void OnDied()
    {
        //enabled = false;
        //Destroy(gameObject);
        Died?.Invoke(this);
    }
}
