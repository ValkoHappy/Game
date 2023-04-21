using UnityEngine;
using UnityEngine.EventSystems;
using System.Collections;

public class ZoomButton : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    [SerializeField] protected Camera _camera;

    protected CameraMover _cameraMover;
    private Coroutine _zoom;
    private bool _isZooming = false;

    private void Awake()
    {
        _cameraMover = _camera.GetComponent<CameraMover>();
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if (_zoom != null)
        {
            StopCoroutine(_zoom);
            _zoom = StartCoroutine(Zoom());
        }
        else
        {
            _zoom = StartCoroutine(Zoom());
        }
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        if (_zoom != null)
        {
            _isZooming = false;
            StopCoroutine(_zoom);
        }
    }

    public virtual IEnumerator Zoom()
    {
        _isZooming = true;
        while (_isZooming)
        {
            _cameraMover.ZoomIn();
            yield return null;
        }
    }
}