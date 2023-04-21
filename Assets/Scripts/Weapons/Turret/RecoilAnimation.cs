using UnityEngine;
using DG.Tweening;

public class RecoilAnimation : MonoBehaviour
{
    [SerializeField] private float _recoilDistance;
    [SerializeField] private float _recoilSpeed;
    [SerializeField] private int _numberOfRepetitions = 2;

    public void StartRecoil(float delayBetween)
    {
        transform.DOLocalMoveZ(transform.localPosition.z - _recoilDistance, delayBetween / _recoilSpeed).SetLoops(_numberOfRepetitions, LoopType.Yoyo);
    }
}
