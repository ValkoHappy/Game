using System;
using UnityEngine;

public abstract class CameraAnimation : MonoBehaviour
{
    [SerializeField] private float _duration;

    private float _time = 0;
    private bool _isPlaying = true;

    public event Action AnimationFinished;

    public float Duration => _duration;

    private void Update()
    {
        if (_isPlaying)
        {
            _time += Time.deltaTime;

            if (_time >= _duration)
            {
                AnimationFinished?.Invoke();
                _time = 0;
                _isPlaying = false;
            }
        }
    }

    protected void Play()
    {
        _isPlaying = true;
    }

    public abstract void OnRotationCamera();
}
