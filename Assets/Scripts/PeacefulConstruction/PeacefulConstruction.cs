using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PeacefulConstruction : StateMachine
{
    //[SerializeField] private PeacefulConstructionState _firstState;

    //private PeacefulConstructionState _currentState;

    //private void Start()
    //{
    //    _currentState = _firstState;
    //    _currentState.Enter();
    //}

    //private void Update()
    //{
    //    if (_currentState != null)
    //        return;

    //    PeacefulConstructionState nextState = _currentState.GetNextState();
    //    if (nextState != null)
    //        Transit(nextState);

    //}

    //private void Transit(PeacefulConstructionState nextState)
    //{
    //    if (_currentState != null)
    //        _currentState.Exit();

    //    _currentState = nextState;

    //    if (_currentState != null)
    //        _currentState.Enter();
    //}
}
