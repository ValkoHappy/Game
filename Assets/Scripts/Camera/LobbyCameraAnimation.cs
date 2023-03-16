using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.Events;

public class LobbyCameraAnimation : MonoBehaviour
{
    [SerializeField] private Vector3[] _waypoints;
    [SerializeField] private float _duration;

    private float _time = 0;
    private bool _isPlaying = true;

    public event UnityAction AnimationIsFinished;

    private void Start()
    {
        RotationCamera();
    }

    private void Update()
    {
        if (_isPlaying)
        {
            _time += Time.deltaTime;
            if (_time >= _duration)
            {
                AnimationIsFinished?.Invoke();
                _time = 0;
                Debug.Log("fdgfg");
                _isPlaying = false;
            }
        }
    }

    public void RotationCamera()
    {
        transform.DOLocalPath(_waypoints, _duration, PathType.Linear);
    }

}
