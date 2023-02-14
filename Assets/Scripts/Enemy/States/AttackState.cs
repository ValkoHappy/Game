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
        StartAttack();
    }

    private void Update()
    {
    }

    private void StartAttack()
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
        SetAnimation();
        PeacefulConstruction.ApplyDamage(_attackForce);
        yield return waitForSecounds;
        StartAttack();
    }
}
