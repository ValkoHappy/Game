using UnityEngine;
using DG.Tweening;

public class StartCameraAnimation : MonoBehaviour
{
    [SerializeField] private float _duration;

    private int rotationAngle = 360;
    private int numberOfRepetitions = -1;

    private void Start()
    {
        RotationCamera();
    }

    public void RotationCamera()
    {
        transform.DOLocalRotate(new Vector3(0, rotationAngle, 0), _duration, RotateMode.LocalAxisAdd).SetEase(Ease.Linear).SetLoops(numberOfRepetitions);
    }
}
