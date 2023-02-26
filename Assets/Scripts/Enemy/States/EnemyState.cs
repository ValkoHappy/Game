using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyState : MonoBehaviour
{
    [SerializeField] private EnemyTransition[] _transitions;
    public EnemyTransition[] Transitions => _transitions;
    public PeacefulConstruction PeacefulConstruction { get; private set; }
    public Animator Animator { get; private set; }
    public Rigidbody Rigidbody { get; private set; }

    public void Enter(PeacefulConstruction peacefulConstruction, Animator animator, Rigidbody rigidbody)
    {
        if (enabled == false)
        {
            PeacefulConstruction = peacefulConstruction;
            Animator = animator;
            Rigidbody = rigidbody;

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
