using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
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
    [SerializeField] private Spawner _spawner;

    public void RotationCamera()
    {

        Debug.Log(_spawner.Level.SpawnSide);
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
        transform.DOLocalMove(_endWaypoint, _duration/2).SetDelay(_duration);
    }
}
