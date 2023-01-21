using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyTransition : MonoBehaviour
{
    [SerializeField] private EnemyState _targetState;

    public EnemyState TargetState => _targetState;

    protected PeacefulConstruction PeacefulConstruction { get; private set; }

    public bool NeedTransit { get; protected set; }

    protected virtual void OnEnable()
    {
        NeedTransit = false;
    }

    public void Init(PeacefulConstruction peacefulConstruction)
    {
        PeacefulConstruction = peacefulConstruction;
    }
}
