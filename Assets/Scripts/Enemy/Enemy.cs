using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Animator), typeof(Rigidbody), typeof(FoundBuildings))]
public class Enemy : MonoBehaviour
{
    [SerializeField] private EnemyState _firstState;
    [SerializeField] private BrokenState _brokenState;

    private FoundBuildings _buildings;
    private EnemyState _currentState;
    private Animator _animator;
    private Rigidbody _rigidbody;
    private HealthContainer _healthContainer;

    private PeacefulConstruction _targetConstruction;

    private void Awake()
    {
        _buildings = GetComponent<FoundBuildings>();
        _animator = GetComponent<Animator>();
        _rigidbody = GetComponent<Rigidbody>();
        _targetConstruction = FindObjectOfType<PeacefulConstruction>();
        _healthContainer = GetComponentInChildren<HealthContainer>();
    }

    private void Start()
    {
        _currentState = _firstState;
        _targetConstruction = _buildings.TargetConstruction;
        _currentState.Enter(_targetConstruction, _animator, _rigidbody);
    }

    private void Update()
    {
        _targetConstruction = _buildings.TargetConstruction;

        foreach (var transition in _currentState.Transitions)
        {
            transition.enabled = true;
            transition.Init(_targetConstruction);
        }

        if (_currentState == null)
            return;

        EnemyState nextState = _currentState.GetNextState();

        if (nextState != null)
            Transit(nextState);

        if(_healthContainer.Health <= 0 && _currentState != _brokenState)
        {
            Transit(_brokenState);
        }
    }

    private void Transit(EnemyState nextState)
    {
        if (_currentState != null)
            _currentState.Exit();

        _currentState = nextState;

        if (_currentState != null)
            _currentState.Enter(_targetConstruction, _animator, _rigidbody);
    }

    //public void ApplyDamage(Rigidbody attachedBody, float force)
    //{
    //    Vector3 direction = (transform.position - attachedBody.position);
    //    direction.y = 0;
    //    _rigidbody.AddForce(direction.normalized * force, ForceMode.Impulse);
    //}
}
