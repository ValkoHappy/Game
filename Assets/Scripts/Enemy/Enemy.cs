using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Animator))]
public class Enemy : MonoBehaviour
{
    [SerializeField] private EnemyState _firstState;

    private EnemyState _currentState;
    private Animator _animator;
    private bool _isAlive;

    public HealthContainer _healthContainer { get; protected set; }
    public PeacefulConstruction PeacefulConstruction { get; private set; }

    public event UnityAction<Enemy> Died;

    protected void OnDied()
    {
        enabled = false;

        Died?.Invoke(this);
    }

    public bool IsAlive()
    {
        if(_healthContainer.Health <= 0)
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

    private void Awake()
    {
        _animator = GetComponent<Animator>();
        PeacefulConstruction = FindObjectOfType<PeacefulConstruction>();
        _healthContainer = GetComponent<HealthContainer>();
    }

    private void Start()
    {
        _currentState = _firstState;
        _currentState.Enter(PeacefulConstruction, _animator);
    }

    private void Update()
    {
        if (_currentState == null)
            return;

        EnemyState nextState = _currentState.GetNextState();

        if (nextState != null)
            Transit(nextState);
    }

    private void Transit(EnemyState nextState)
    {
        if (_currentState != null)
            _currentState.Exit();

        _currentState = nextState;

        if (_currentState != null)
            _currentState.Enter(PeacefulConstruction, _animator);
    }

    public void ApplyDamage(float damage)
    {
        _healthContainer.TakeDamage((int)damage);
    }
}
