using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiedObjectTransition : EnemyTransition
{
    private void Update()
    {
        if (PeacefulConstruction.IsAlive() == false)
            NeedTransit = true;
    }
}
