using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(HealthContainer))]
public class StateMachine : MonoBehaviour
{
    [SerializeField] private State _firstState;

    private State _currentState;
    public event UnityAction Damaged;
    public HealthContainer _healthContainer { get; protected set; }

    private void Awake()
    {
        _healthContainer = GetComponent<HealthContainer>();
    }

    private void Start()
    {
        _currentState = _firstState;
        _currentState.Enter();
    }

    private void Update()
    {
        if (_currentState == null)
            return;

        State nextState = _currentState.GetNextState();

        if (nextState != null)
            Transit(nextState);
    }

    private void OnEnable()
    {
        _healthContainer.Died += OnDied;
    }

    private void OnDisable()
    {
        _healthContainer.Died -= OnDied;
    }

    protected virtual void OnDied()
    {
        //enabled = false;
        Destroy(gameObject);
    }

    protected void Transit(State nextState)
    {
        if (_currentState != null)
            _currentState.Exit();

        _currentState = nextState;

        if (_currentState != null)
            _currentState.Enter();
    }

    public void ApplyDamage(float damage)
    {
        _healthContainer.TakeDamage((int)damage);
        Damaged?.Invoke();
    }
}
