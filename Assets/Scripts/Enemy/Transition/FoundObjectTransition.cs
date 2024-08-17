using UnityEngine;

namespace Scripts.Enemy.Transition
{
    public class FoundObjectTransition : EnemyTransition
    {
        [SerializeField] private float _foundDistance;

        private void Update()
        {
            if (Vector3.Distance(PeacefulConstruction.transform.position, transform.position) < _foundDistance)
                NeedTransit = true;
        }
    }
}