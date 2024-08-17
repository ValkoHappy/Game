using System.Collections;
using UnityEngine;

namespace Scripts.Enemy.States
{
    public class AttackState : EnemyState
    {
        private const string Attack = "Attack";

        [SerializeField] private float _attackForce;
        [SerializeField] private float _attackDelay;

        private Coroutine _attackCoroutine;
        private WaitForSeconds _attackWait;

        private void Awake()
        {
            _attackWait = new WaitForSeconds(_attackDelay);
        }

        private void OnEnable()
        {
            if (enabled)
                StartCombat();
        }

        private void OnDisable()
        {
            if (_attackCoroutine != null)
                StopCoroutine(_attackCoroutine);
        }

        private void StartCombat()
        {
            if (_attackCoroutine != null)
            {
                StopCoroutine(_attackCoroutine);
                _attackCoroutine = null;
            }

            _attackCoroutine = StartCoroutine(ExecuteCombat());
        }

        private IEnumerator ExecuteCombat()
        {
            Animator.SetTrigger(Attack);

            PeacefulConstruction.ApplyDamage(_attackForce);
            yield return _attackWait;

            if (PeacefulConstruction.IsAlive)
                StartCombat();
        }
    }
}