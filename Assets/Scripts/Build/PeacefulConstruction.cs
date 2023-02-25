using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(HealthContainer) /*typeof(BoxCollider)*/)]
public class PeacefulConstruction : MonoBehaviour
{
    [SerializeField] private float _bounceForce;
    [SerializeField] private float _bounceRadius;

    public event UnityAction<PeacefulConstruction> Die;

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
        Die?.Invoke(this);
        Break();
        enabled = false;
        //Destroy(gameObject);
    }

    private void Break()
    {
        BuildingDetail[] buildingDetails = GetComponentsInChildren<BuildingDetail>();

        foreach (var detail in buildingDetails)
        {
            detail.Bounce(_bounceForce, transform.position, _bounceRadius);
        }
    }
}
