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
    float time = 0;

    private void Awake()
    {
        rTS_Camera = GetComponent<RTS_Camera>();
    }

    private void Start()
    {
        rTS_Camera.enabled = false;
        RotationCamera();
    }

    private void Update()
    {
        if(rTS_Camera.enabled == false)
        {
            time += Time.deltaTime;
            if (time >= _duration)
                rTS_Camera.enabled = true;
        }
    }

    public void RotationCamera()
    {
        transform.DOLocalPath(_waypoints, _duration, PathType.Linear);
    }
}
