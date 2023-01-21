using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyState : MonoBehaviour
{
    [SerializeField] private EnemyTransition[] _transitions;

    protected PeacefulConstruction PeacefulConstruction { get; private set; }
    protected Animator Animator { get; private set; }

    public void Enter(PeacefulConstruction peacefulConstruction, Animator animator)
    {
        if (enabled == false)
        {
            PeacefulConstruction = peacefulConstruction;
            Animator = animator;

            enabled = true;

            foreach (var transition in _transitions)
            {
                transition.enabled = true;
                transition.Init(PeacefulConstruction);
            }
        }
    }

    public EnemyState GetNextState()
    {
        foreach (var transition in _transitions)
        {
            if (transition.NeedTransit)
            {
                return transition.TargetState;
            }
        }

        return null;
    }

    public void Exit()
    {
        if (enabled == true)
        {
            foreach (var transition in _transitions)
            {
                transition.enabled = false;
            }
        }

        enabled = false;
    }
}
