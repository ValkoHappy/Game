using Scripts.Build;
using UnityEngine;
using UnityEngine.AI;

namespace Scripts.Enemy.States
{
    [RequireComponent(typeof(NavMeshAgent))]
    [RequireComponent(typeof(FoundBuildings))]
    public class ChaseState : EnemyState
    {
        private const string Run = "Run";

        private FoundBuildings _foundBuildings;
        private NavMeshAgent _agent;
        private PeacefulConstruction _targetConstruction;

        private float _maxValue = 0.01f;
        private float _minValue = 0.0f;

        private void Awake()
        {
            _foundBuildings = GetComponent<FoundBuildings>();
            _agent = GetComponent<NavMeshAgent>();
        }

        private void OnEnable()
        {
            Animator.SetFloat(Run, _maxValue);
            _agent.enabled = true;
        }

        private void OnDisable()
        {
            Animator.SetFloat(Run, _minValue);
            _agent.enabled = false;
        }

        private void Update()
        {
            _targetConstruction = _foundBuildings.TargetConstruction;

            if (_targetConstruction != null)
                _agent.SetDestination(_targetConstruction.transform.position);
        }
    }
}