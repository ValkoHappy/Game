using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PeacefulConstruction : StateMachine
{
    public event UnityAction<PeacefulConstruction> Died;

    public bool _isAlive;

    public bool IsAlive()
    {
        if (_healthContainer.Health <= 0)
        {
            return _isAlive = false;
        }
        else
        {
            return _isAlive = true;
        }
    }

    protected override void OnDied()
    {
        enabled = false;
        Destroy(gameObject);
        Died?.Invoke(this);
    }
}
