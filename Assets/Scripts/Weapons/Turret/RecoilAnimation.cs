using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class RecoilAnimation : MonoBehaviour
{
    [SerializeField] private float _recoilDistance;

    public void StartRecoil(float delayBetween)
    {
        transform.DOLocalMoveZ(transform.localPosition.z - _recoilDistance, delayBetween / 5).SetLoops(2, LoopType.Yoyo);
    }
}
