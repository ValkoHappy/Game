using UnityEngine;

namespace Scripts.Enemy.Transition
{
    public class ApproachedObjectTransition : EnemyTransition
    {
        [SerializeField] private float _approachedDistance;

        private void Update()
        {
            if (PeacefulConstruction != null)
            {
                if (Vector3.Distance(PeacefulConstruction.transform.position, transform.position) < _approachedDistance)
                    NeedTransit = true;
            }
        }
    }
}