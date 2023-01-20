using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyTransition : Transition
{
    protected PeacefulConstruction PeacefulConstruction { get; private set; }

    public void Init(PeacefulConstruction peacefulConstruction)
    {
        PeacefulConstruction = peacefulConstruction;
    }
}
