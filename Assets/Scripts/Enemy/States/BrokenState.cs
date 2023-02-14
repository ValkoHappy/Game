using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class BrokenState : EnemyState
{
    public event UnityAction Died;

    private void Update()
    {
        
    }

    public void ApplyDamage(Rigidbody attachedBody, float force)
    {
        SetAnimation();
        Vector3 direction = (transform.position - attachedBody.position);
        direction.y = 0;
        Rigidbody.AddForce(direction.normalized * force, ForceMode.Impulse);
        Died?.Invoke();
    }
}
