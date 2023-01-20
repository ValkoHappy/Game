using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleTransition : PeacefulConstructionTransition
{
    private void Update()
    {
        if (Turret.FindTarget() == null)
            NeedTransit = true;
    }
}
