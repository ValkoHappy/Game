using System.Collections;
using UnityEngine;

public class AttackState : EnemyState
{
    [SerializeField] private float _attackForce;
    [SerializeField] private float _attackDelay;

    private Coroutine _attackCoroutine;
    private const string _attack = "Attack";

    private void OnEnable()
    {
        if(enabled)
            StartAttack();
    }

    private void OnDisable()
    {
        if (_attackCoroutine != null)
        {
            StopCoroutine(_attackCoroutine);
        }
    }

    private void Update()
    {
    }

    private void StartAttack()
    {
        if (_attackCoroutine != null)
        {
            StopCoroutine(_attackCoroutine);
        }
        _attackCoroutine = StartCoroutine(Attack());
    }

    private IEnumerator Attack()
    {
        Animator.SetTrigger(_attack);
        var waitForSecounds = new WaitForSeconds(_attackDelay);
        PeacefulConstruction.ApplyDamage(_attackForce);
        yield return waitForSecounds;

        if(PeacefulConstruction.IsAlive())
            StartAttack();
    }
}
