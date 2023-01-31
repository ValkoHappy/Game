using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class RecoilAnimation : MonoBehaviour
{
    [SerializeField] private float _recoilDistance;
    [SerializeField] private float _recoilSpeed;

    public void StartRecoil(float delayBetween)
    {
        transform.DOLocalMoveZ(transform.localPosition.z - _recoilDistance, delayBetween / _recoilSpeed).SetLoops(2, LoopType.Yoyo);
    }
}
