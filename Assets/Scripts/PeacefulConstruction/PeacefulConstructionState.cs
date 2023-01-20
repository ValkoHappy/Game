using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PeacefulConstructionState : State
{
    //[SerializeField] private PeacefulConstructionTransition[] _transitions;

    //public PeacefulConstructionTransition[] Transitions => _transitions;
    //public virtual void Enter()
    //{
    //    if (enabled == false)
    //    {
    //        enabled = true;

    //        foreach (var transition in _transitions)
    //        {
    //            transition.enabled = true;
    //        }
    //    }
    //}

    //public void Exit()
    //{
    //    if (enabled == true)
    //    {
    //        foreach (var transition in _transitions)
    //        {
    //            transition.enabled = false;
    //        }
    //    }

    //    enabled = false;
    //}
    //public PeacefulConstructionState GetNextState()
    //{
    //    foreach (var transition in Transitions)
    //    {
    //        if (transition.NeedTransit)
    //        {
    //            return transition.TargetState;
    //        }
    //    }

    //    return null;
    //}
}
