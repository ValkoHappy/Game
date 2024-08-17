using DG.Tweening;
using Scripts.SO;
using UnityEngine;

namespace Scripts.Camera
{
    public class MovingCameraSpawnEnemies : CameraAnimation
    {
        [SerializeField] private Vector3[] _waypointsUp;
        [SerializeField] private Vector3[] _waypointsDown;
        [SerializeField] private Vector3[] _waypointsLeft;
        [SerializeField] private Vector3[] _waypointsRight;
        [SerializeField] private Vector3 _endWaypoint;
        [SerializeField] private float _delay;
        [SerializeField] private float _duration;
        [SerializeField] private Spawner.Spawner _spawner;
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
                _tween = transform.DOLocalPath(_waypointsUp, _delay);
            else if (_spawner.Level.SpawnSide == Level.Side.Down)
                _tween = transform.DOLocalPath(_waypointsDown, _delay);
            else if (_spawner.Level.SpawnSide == Level.Side.Left)
                _tween = transform.DOLocalPath(_waypointsLeft, _delay);
            else if (_spawner.Level.SpawnSide == Level.Side.Right)
                _tween = transform.DOLocalPath(_waypointsRight, _delay);

            _tween = transform.DOLocalMove(_endWaypoint, _duration).SetDelay(_delay);
        }
    }
}