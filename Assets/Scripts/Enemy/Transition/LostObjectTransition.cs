using UnityEngine;

namespace Scripts.Enemy.Transition
{
    public class LostObjectTransition : EnemyTransition
    {
        [SerializeField] private float _minimumLostDistance;

        private void Update()
        {
            if (Vector3.Distance(transform.position, PeacefulConstruction.transform.position) > _minimumLostDistance
                || PeacefulConstruction.IsAlive == false)
                NeedTransit = true;
        }
    }
}