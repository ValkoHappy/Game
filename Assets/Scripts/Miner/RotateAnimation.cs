using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class RotateAnimation : MonoBehaviour
{
    [SerializeField] private float _duration;

    public void Start()
    {
        Tween tween = transform.DOLocalRotate(new Vector3(0,0, _duration), 2);
        tween.SetEase(Ease.Linear).SetLoops(-1);
    }
}
