using DG.Tweening;
using UnityEngine;

public class MovingCameraSpawnEnemies : CameraAnimation
{
    [SerializeField] private Vector3[] _waypointsUp;
    [SerializeField] private Vector3[] _waypointsDown;
    [SerializeField] private Vector3[] _waypointsLeft;
    [SerializeField] private Vector3[] _waypointsRight;
    [SerializeField] private Vector3 _endWaypoint;
    [SerializeField] private float _duration;
    [SerializeField] private float _duration2;
    [SerializeField] private Spawner _spawner;
    [SerializeField] private LobbyCameraAnimation _animation;

    private Tween _tween;

    private void OnEnable()
    {
        _animation.AnimationFinished += OnRotationCamera;
    }

    private void OnDisable()
    {
        _animation.AnimationFinished -= OnRotationCamera;
    }

    public override void OnRotationCamera()
    {
        _tween.Kill();
        Play();

        if (_spawner.Level.SpawnSide == Level.Side.Up)
            _tween = transform.DOLocalPath(_waypointsUp, _duration);
        else if (_spawner.Level.SpawnSide == Level.Side.Down)
            _tween = transform.DOLocalPath(_waypointsDown, _duration);
        else if (_spawner.Level.SpawnSide == Level.Side.Left)
            _tween = transform.DOLocalPath(_waypointsLeft, _duration);
        else if (_spawner.Level.SpawnSide == Level.Side.Right)
            _tween = transform.DOLocalPath(_waypointsRight, _duration);

        _tween = transform.DOLocalMove(_endWaypoint, _duration2).SetDelay(_duration);
    }
}
