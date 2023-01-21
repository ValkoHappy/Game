using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackState : EnemyState
{
    [SerializeField] private float _attackForce;
    [SerializeField] private float _attackDelay;

    private Coroutine _attack;

    private void Start()
    {
        StartShoot();
    }

    private void StartShoot()
    {
        if (_attack != null)
        {
            StopCoroutine(_attack);
        }
        _attack = StartCoroutine(Attack());
    }

    private IEnumerator Attack()
    {
        var waitForSecounds = new WaitForSeconds(_attackDelay);
        Animator.SetTrigger("Attack1");
        PeacefulConstruction.ApplyDamage(_attackForce);
        yield return waitForSecounds;
        StartShoot();
    }
}
