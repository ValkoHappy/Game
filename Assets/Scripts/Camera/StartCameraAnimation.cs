using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class StartCameraAnimation : MonoBehaviour
{
    [SerializeField] private float _duration;

    private void Start()
    {
        RotationCamera();
    }

    public void RotationCamera()
    {
        transform.DOLocalRotate(new Vector3(0, 360, 0), _duration, RotateMode.LocalAxisAdd).SetEase(Ease.Linear).SetLoops(-1);
    }
}
