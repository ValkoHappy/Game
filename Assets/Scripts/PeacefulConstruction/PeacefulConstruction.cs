using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PeacefulConstruction : StateMachine
{
    public event UnityAction<PeacefulConstruction> Died;

    protected override void OnDied()
    {
        enabled = false;
        Destroy(gameObject);
        Died?.Invoke(this);
    }
}
