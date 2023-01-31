using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SolarGeneratorAnimation : MonoBehaviour
{
    [SerializeField] private float _duration;

    public void Start()
    {
        transform.DOLocalRotate(new Vector3(-120, 0, 0), _duration).SetEase(Ease.Linear).SetLoops(-1, LoopType.Yoyo);
    }
}
