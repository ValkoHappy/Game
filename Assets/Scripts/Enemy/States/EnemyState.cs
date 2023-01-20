using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyState : State
{
    [SerializeField] private Transition[] _transitions;

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
}
