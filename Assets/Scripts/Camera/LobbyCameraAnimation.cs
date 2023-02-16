using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using RTS_Cam;

public class LobbyCameraAnimation : MonoBehaviour
{
    [SerializeField] private Vector3[] _waypoints;
    [SerializeField] private float _duration;

    private RTS_Camera rTS_Camera;

    private void Awake()
    {
        rTS_Camera = GetComponent<RTS_Camera>();
    }

    private void Start()
    {
        rTS_Camera.enabled= false;
        RotationCamera();
    }

    public void RotationCamera()
    {
        transform.DOPath(_waypoints, _duration, PathType.Linear).SetOptions(true);
        rTS_Camera.enabled = true;
    }
}
