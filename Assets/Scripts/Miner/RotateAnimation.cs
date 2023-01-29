using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class RotateAnimation : MonoBehaviour
{
    [SerializeField] private float _duration;

    public void Start()
    {
        transform.DORotate(new Vector3(0,0, 360), _duration, RotateMode.FastBeyond360).SetEase(Ease.Linear).SetLoops(-1);
    }
}
