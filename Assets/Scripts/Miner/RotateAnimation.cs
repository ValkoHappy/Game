using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class RotateAnimation : MonoBehaviour
{
    [SerializeField] private float _recoilDistance;

    public void Start()
    {
        Tween tween = transform.DOLocalRotate(new Vector3(0,0, 360), 2, RotateMode.FastBeyond360).SetOptions(true);
        tween.SetEase(Ease.Linear).SetLoops(-1);
    }
}
