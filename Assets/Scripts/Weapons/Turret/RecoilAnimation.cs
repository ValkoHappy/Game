using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class RecoilAnimation : MonoBehaviour
{
    [SerializeField] private float _recoilDistance;

    public void StartRecoil(float delayBetween)
    {
        transform.DOMoveZ(transform.localPosition.z - _recoilDistance, delayBetween / 4).SetLoops(2, LoopType.Yoyo);
    }
}
