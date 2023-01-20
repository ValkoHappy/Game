using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Turret))]
public class ShootTransition : PeacefulConstructionTransition
{
    private Turret _turret;

    private void Start()
    {
        _turret = GetComponent<Turret>();
    }

    private void Update()
    {
        if (_turret.FindTarget())
            NeedTransit = true;
    }

}
