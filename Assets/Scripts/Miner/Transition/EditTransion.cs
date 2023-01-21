using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EditTransion : Transition
{
    private bool _isBuildingPlace;

    public bool IsBuildingPlace => _isBuildingPlace;

    private void Update()
    {
        if (_isBuildingPlace)
            NeedTransit = true;
    }
}
