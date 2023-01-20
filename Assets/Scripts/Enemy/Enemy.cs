using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Animator))]
public class Enemy : StateMachine
{
    [SerializeField] private State _firstState;

    private State _currentState;
    private Animator _animator;

    public PeacefulConstruction PeacefulConstruction { get; private set; }

    public event UnityAction<Enemy> Died;

    protected override void OnDied()
    {
        enabled = false;

        Died?.Invoke(this);
    }

    private void Awake()
    {
        _animator = GetComponent<Animator>();
        PeacefulConstruction = FindObjectOfType<PeacefulConstruction>();
    }

    private void Start()
    {
        _currentState = _firstState;
        _currentState.Enter(PeacefulConstruction, _animator);
    }

    private void Update()
    {
        if (_currentState != null)
            return;

        State nextState = _currentState.GetNextState();
        if (nextState != null)
            Transit(nextState);

    }

    private void Transit(State nextState)
    {
        if (_currentState != null)
            _currentState.Exit();

        _currentState = nextState;

        if (_currentState != null)
            _currentState.Enter(PeacefulConstruction, _animator);
    }
}
