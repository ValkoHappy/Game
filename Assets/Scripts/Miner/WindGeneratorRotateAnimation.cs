using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class WindGeneratorRotateAnimation : MinerAnimation
{
    public override void OnDeliveryBuilding()
    {
        transform.DORotate(new Vector3(0, 0, 360), Duration, RotateMode.FastBeyond360).SetEase(Ease.Linear).SetLoops(-1);
    }
}
