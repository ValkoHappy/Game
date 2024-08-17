using Scripts.Build;
using Scripts.Enemy.States;
using UnityEngine;

namespace Scripts.Enemy.Transition
{
    public class EnemyTransition : MonoBehaviour
    {
        [SerializeField] private EnemyState _targetState;

        public EnemyState TargetState => _targetState;
        public bool NeedTransit { get; protected set; }
        protected PeacefulConstruction PeacefulConstruction { get; private set; }

        protected virtual void OnEnable()
        {
            NeedTransit = false;
        }

        public void Init(PeacefulConstruction peacefulConstruction)
        {
            PeacefulConstruction = peacefulConstruction;
        }
    }
}
