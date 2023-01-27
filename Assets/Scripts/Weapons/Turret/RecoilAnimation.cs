using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class RecoilAnimation : MonoBehaviour
{
    [SerializeField] private float _recoilDistance;

    public void StartRecoil(float delayBetween)
    {
        transform.DOMoveZ(transform.position.z - _recoilDistance, delayBetween / 2).SetLoops(2, LoopType.Yoyo);
    }
}
