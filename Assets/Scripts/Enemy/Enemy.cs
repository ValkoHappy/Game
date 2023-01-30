using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Animator), typeof(FoundBuildings))]
public class Enemy : MonoBehaviour
{
    [SerializeField] private EnemyState _firstState;

    private FoundBuildings _buildings;
    private EnemyState _currentState;
    private Animator _animator;

    [SerializeField] public PeacefulConstruction TargetConstruction;

    private void Awake()
    {
        _buildings = GetComponent<FoundBuildings>();
        _animator = GetComponent<Animator>();
        TargetConstruction = FindObjectOfType<PeacefulConstruction>();
    }

    private void Start()
    {
        _currentState = _firstState;
        _buildings.SortEnemies();
        TargetConstruction = _buildings.TargetConstruction;
        _currentState.Enter(TargetConstruction, _animator);


    }

    private void Update()
    {
        _buildings.SortEnemies();
        TargetConstruction = _buildings.TargetConstruction;
        _currentState.Enter(TargetConstruction, _animator);

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
            _currentState.Enter(TargetConstruction, _animator);
    }
}
