using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Turret))]
public class PeacefulConstructionTransition : Transition
{
    private Turret _turret;

    protected Turret Turret => _turret;

    private void Start()
    {
        _turret = GetComponent<Turret>();
    }
}
