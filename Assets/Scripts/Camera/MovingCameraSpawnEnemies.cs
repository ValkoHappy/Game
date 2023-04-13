using DG.Tweening;
using UnityEngine;
using UnityEngine.Events;

public class MovingCameraSpawnEnemies : MonoBehaviour
{
    [SerializeField] private Vector3[] _waypointsUp;
    [SerializeField] private Vector3[] _waypointsDown;
    [SerializeField] private Vector3[] _waypointsLeft;
    [SerializeField] private Vector3[] _waypointsRight;
    [SerializeField] private Vector3 _endWaypoint;
    [SerializeField] private float _duration;
    [SerializeField] private float _duration2;
    [SerializeField] private Spawner _spawner;

    private float _time = 0;
    private bool _isPlaying = false;

    public event UnityAction AnimationIsFinished;

    private void Update()
    {
        if (_isPlaying)
        {
            _time += Time.deltaTime;
            if (_time >= _duration + _duration2)
            {
                AnimationIsFinished?.Invoke();
                _time = 0;
                _isPlaying = false;
            }
        }
    }

    public void RotationCamera()
    {
        _isPlaying = true;
        if (_spawner.Level.SpawnSide == Level.Side.Up)
        {
            transform.DOLocalPath(_waypointsUp, _duration);
        }
        else if(_spawner.Level.SpawnSide == Level.Side.Down)
        {
            transform.DOLocalPath(_waypointsDown, _duration);
        }
        else if (_spawner.Level.SpawnSide == Level.Side.Left)
        {
            transform.DOLocalPath(_waypointsLeft, _duration);
        }
        else if (_spawner.Level.SpawnSide == Level.Side.Right)
        {
            transform.DOLocalPath(_waypointsRight, _duration);
        }
        transform.DOLocalMove(_endWaypoint, _duration2).SetDelay(_duration);
    }
}
