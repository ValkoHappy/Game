using Scripts.Build;
using Scripts.Enemy.Transition;
using UnityEngine;

namespace Scripts.Enemy.States
{
    public class EnemyState : MonoBehaviour
    {
        [SerializeField] private EnemyTransition[] _transitions;

        public PeacefulConstruction PeacefulConstruction { get; private set; }
        public Animator Animator { get; private set; }
        public Rigidbody Rigidbody { get; private set; }
        public EnemyTransition[] Transitions => _transitions;

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
                    return transition.TargetState;
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
}