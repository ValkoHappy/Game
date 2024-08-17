using DG.Tweening;
using UnityEngine;

namespace Scripts.Camera
{
    public class StartCameraAnimation : MonoBehaviour
    {
        [SerializeField] private float _duration;

        private Tween _tween;

        private int _rotationAngle = 360;
        private int _numberOfRepetitions = -1;

        private void Start()
        {
            RotationCamera();
        }

        public void RotationCamera()
        {
            _tween.Kill();
            _tween = transform.DOLocalRotate(
                new Vector3(0, _rotationAngle, 0),
                _duration, RotateMode.LocalAxisAdd)
                .SetEase(Ease.Linear).SetLoops(_numberOfRepetitions);
        }
    }
}