using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Camera))]
public class CameraControler : MonoBehaviour
{
    [SerializeField] private Transform _root;
    [SerializeField] private Transform _pivot;
    [SerializeField] private Transform _target;

    [SerializeField] private float _moveSpeed;
    [SerializeField] private float _moveSmooth;
    [SerializeField] private float _zoomSpeed;
    [SerializeField] private float _zoomSmooth;

    [SerializeField] private float _zoomMin;
    [SerializeField] private float _zoomMax;

    [SerializeField] private float _right;
    [SerializeField] private float _left;
    [SerializeField] private float _up;
    [SerializeField] private float _down;
    [SerializeField] private float _angle;
    [SerializeField] private float _zoom;
    [SerializeField] private float _zOffset;

    private Vector2 _zoomPositionOnScreen;
    private Vector3 _zoomPositionInWorld;
    private float _zoomBaseValue;
    private float _zoomBaseDistance;


    private Vector3 _center = Vector3.zero;
    private Camera _camera;
    private Controls _inputs;
    private bool _isMoving;
    private bool _isZooming;

    private void Awake()
    {
        _camera = GetComponent<Camera>();
        _inputs = new Controls();
        _camera.orthographic = true;
        _camera.nearClipPlane = 0;
    }

    private void Start()
    {
        Initialize(_center, _right, _left, _up, _down, _angle, _zoom, _zoomMin, _zoomMax);
    }

    private void OnEnable()
    {
        _inputs.Enable();
        _inputs.Main.Move.started += _ => MoveStarted();
        _inputs.Main.Move.canceled += _ => MoveCamceled();
        _inputs.Main.TouchZoom.started += _ => ZoomStarted();
        _inputs.Main.TouchZoom.canceled += _ => ZoomCamceled();
    }

    private void OnDisable()
    {
        _inputs.Main.Move.started -= _ => MoveStarted();
        _inputs.Main.Move.canceled -= _ => MoveCamceled();
        _inputs.Main.TouchZoom.started -= _ => ZoomStarted();
        _inputs.Main.TouchZoom.canceled -= _ => ZoomCamceled();
        _inputs.Disable();
    }

    public void Initialize(Vector3 center, float right, float left, float up, float down, float angle, float zoom, float zoomMin, float zoomMax)
    {
        _center = center;
        _right = right;
        _left = left;
        _up = up;
        _down = down;
        _angle = angle;
        _zoom = zoom;
        _zoomMin = zoomMin;
        _zoomMax = zoomMax;

        _isMoving = false;
        _isZooming = false;

        _camera.orthographicSize = _zoom;

        _root.position = _center;
        _root.localEulerAngles = Vector3.zero;

        _pivot.localPosition = Vector3.zero;
        _pivot.localEulerAngles = new Vector3(_angle, 0, 0);

        _target.localPosition = new Vector3(0, 0, _zOffset);
        _target.localEulerAngles = Vector3.zero;
    }

    private void MoveStarted()
    {
        _isMoving = true;
    }

    private void MoveCamceled()
    {
        _isMoving = false;
    }

    private void ZoomStarted()
    {
        Vector2 touch0 = _inputs.Main.TouchPosition0.ReadValue<Vector2>();
        Vector2 touch1 = _inputs.Main.TouchPosition1.ReadValue<Vector2>();
        _zoomPositionOnScreen = Vector2.Lerp(touch0, touch1, 0.5f);
        _zoomPositionInWorld = CameraScreenPositionToPlane(_zoomPositionOnScreen);
        _zoomBaseValue = _zoom;

        touch0.x /= Screen.width;
        touch1.x /= Screen.width;

        touch0.y /= Screen.height;
        touch1.y /= Screen.height;

        _zoomBaseDistance = Vector2.Distance(touch0, touch1);

        _isZooming = true;
    }

    private void ZoomCamceled()
    {
        _isZooming = false;
    }

    private void Update()
    {
        if (Input.touchSupported == false)
        {
            float mouseScroll = _inputs.Main.MouseScroll.ReadValue<float>();

            if (mouseScroll > 0)
                _zoom -= 3f * Time.deltaTime;
            else if (mouseScroll < 0)
                _zoom += 3f * Time.deltaTime;
        }
        if (_isZooming)
        {
            Vector2 touch0 = _inputs.Main.TouchPosition0.ReadValue<Vector2>();
            Vector2 touch1 = _inputs.Main.TouchPosition1.ReadValue<Vector2>();

            touch0.x /= Screen.width;
            touch1.x /= Screen.width;

            touch0.y /= Screen.height;
            touch1.y /= Screen.height;

            float currentDistance = Vector2.Distance(touch0, touch1);
            float deltaDistance = currentDistance - _zoomBaseDistance;
            _zoom = _zoomBaseValue - (deltaDistance * _zoomSpeed * Time.deltaTime);

            Vector3 zoomCenter = CameraScreenPositionToPlane(_zoomPositionOnScreen);
            _root.position += (_zoomPositionInWorld - zoomCenter);
        }
        else if (_isMoving)
        {
            Vector2 move = _inputs.Main.MoveDelta.ReadValue<Vector2>();
            if (move != Vector2.zero)
            {
                move.x /= Screen.width;
                move.y /= Screen.height;
                _root.position -= _root.right.normalized * move.x * _moveSpeed * Time.deltaTime;
                _root.position -= _root.forward.normalized * move.y * _moveSpeed * Time.deltaTime;

            }
        }

        AdjustBouns();

        if (_camera.transform.position != _target.position)
            _camera.transform.position = Vector3.Lerp(_camera.transform.position, _target.position, _moveSmooth * Time.deltaTime);
        if (_camera.transform.rotation != _target.rotation)
            _camera.transform.rotation = _target.rotation;
        if (_camera.orthographicSize != _zoom)
            _camera.orthographicSize = Mathf.Lerp(_camera.orthographicSize, _zoom, _zoomSmooth * Time.deltaTime);
    }

    private void AdjustBouns()
    {
        if (_zoom < _zoomMin)
            _zoom = _zoomMin;
        if( _zoom > _zoomMax)
            _zoom = _zoomMax;

        float height = PlaneOrtographicSize();
        float width = height * _camera.aspect;

        if (height > (_up + _down) / 2f)
        {
            float n = (_up + _down) / 2f;
            _zoom = n * Mathf.Sin(_angle * Mathf.Deg2Rad) / _camera.aspect;
        }

        if (width > (_right + _left) / 2f)
        {
            float n = (_right + _left) / 2f;
            _zoom = n * Mathf.Sin(_angle * Mathf.Deg2Rad) / _camera.aspect;
        }

        height = PlaneOrtographicSize();
        width = height * _camera.aspect;

        Vector3 upperRightCorner = _root.position + _root.right.normalized * width + _root.forward.normalized * height; 
        Vector3 upperLeftCorner = _root.position - _root.right.normalized * width + _root.forward.normalized * height;
        Vector3 lowerRightCorner = _root.position + _root.right.normalized * width - _root.forward.normalized * height;
        Vector3 lowerLeftCorner = _root.position - _root.right.normalized * width - _root.forward.normalized * height;

        if (upperRightCorner.x > _center.x + _right)
            _root.position += Vector3.left * Mathf.Abs(upperRightCorner.x - (_center.x + _right));
        if (upperLeftCorner.x < _center.x - _left)
            _root.position += Vector3.right * Mathf.Abs((_center.x - _left) - upperLeftCorner.x);

        if (lowerRightCorner.z > _center.z + _up)
            _root.position += Vector3.back * Mathf.Abs(lowerRightCorner.z - (_center.z + _up));
        if (upperLeftCorner.z < _center.z - _down)
            _root.position += Vector3.forward * Mathf.Abs((_center.z - _down) - lowerLeftCorner.z);
    }

    private float PlaneOrtographicSize()
    {
        float height = _zoom * 2f;
        return height / Mathf.Sin(_angle * Mathf.Deg2Rad) / 2;
    }

    private Vector3 CameraScreenPositionToWorld(Vector2 position)
    {
        float height = _camera.orthographicSize * 2f;
        float width = _camera.aspect * height;
        Vector3 ancher = _camera.transform.position - (_camera.transform.right.normalized * width / 2f) - (_camera.transform.up.normalized * height / 2f);

        return ancher + (_camera.transform.right.normalized * position.x / Screen.width * width) + (_camera.transform.up.normalized * position.y / Screen.height * height);
    }
    private Vector3 CameraScreenPositionToPlane(Vector2 position)
    {
        Vector3 point = CameraScreenPositionToWorld(position);
        float height = point.y / _root.position.y;
        float positionX = height / Mathf.Sin(_angle * Mathf.Deg2Rad);
        return point + _camera.transform.forward.normalized * positionX;
    }
}
