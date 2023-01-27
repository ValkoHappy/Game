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

    [SerializeField] public PeacefulConstruction PeacefulConstruction;

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
        Debug.Log(PeacefulConstruction.transform.position);
    }
}
