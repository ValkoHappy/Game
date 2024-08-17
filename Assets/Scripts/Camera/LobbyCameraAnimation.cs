using DG.Tweening;
using UnityEngine;

namespace Scripts.Camera
{
    public class LobbyCameraAnimation : CameraAnimation
    {
        [SerializeField] private Vector3[] _waypoints;

        private Tween _tween;

        private void Start()
        {
            OnRotationCamera();
        }

        public override void OnRotationCamera()
        {
            _tween.Kill();
            _tween = transform.DOLocalPath(_waypoints, Duration, PathType.Linear);
        }
    }
}