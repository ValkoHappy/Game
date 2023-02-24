using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OilGeneratorRotateAnimation : MinerAnimation
{
    public override void OnDeliveryBuilding()
    {
        TweenAnimation = transform.DOLocalRotate(new Vector3(-105, 0, 0), Duration).SetEase(Ease.Linear).SetLoops(Repetitions, LoopType.Yoyo);
    }
}
