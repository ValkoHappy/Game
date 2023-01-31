using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class BrokenState : EnemyState
{
    public event UnityAction Died;

    private void Update()
    {
        //Animator.SetTrigger("");
        Died?.Invoke();
    }
}
