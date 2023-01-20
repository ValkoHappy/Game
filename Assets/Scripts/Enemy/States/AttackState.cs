using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackState : EnemyState
{
    [SerializeField] private float _attackForce;
    [SerializeField] private float _attackDelay;

    private Coroutine _coroutine;

    private void OnEnable()
    {
        _coroutine = StartCoroutine(Attack());
    }

    private IEnumerator Attack()
    {
        while (enabled)
        {
            Animator.SetTrigger("Attack1");
            PeacefulConstruction.ApplyDamage(_attackForce);
            yield return new WaitForSeconds(_attackDelay);
        }
    }

    private void OnDisable()
    {
        StopCoroutine(_coroutine);
    }
}
