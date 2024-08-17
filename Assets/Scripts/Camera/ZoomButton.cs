using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Scripts.Camera
{
    public class ZoomButton : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
    {
        [SerializeField] private UnityEngine.Camera _camera;
        [SerializeField] private ZoomType _zoomType;

        protected CameraMover _cameraMover;
        private Coroutine _zoom;

        public enum ZoomType
        {
            In,
            Out,
        }

        private void Awake()
        {
            _cameraMover = _camera.GetComponent<CameraMover>();
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            if (_zoom != null)
            {
                StopCoroutine(_zoom);
                _zoom = null;
            }

            _zoom = StartCoroutine(Zoom());
        }

        public void OnPointerUp(PointerEventData eventData)
        {
            if (_zoom != null)
            {
                StopCoroutine(_zoom);
            }
        }

        public IEnumerator Zoom()
        {
            if (_zoomType == ZoomType.In)
            {
                while (_camera.fieldOfView > _cameraMover.MinZoom)
                {
                    _cameraMover.ZoomIn();
                    yield return null;
                }
            }
            else if (_zoomType == ZoomType.Out)
            {
                while (_camera.fieldOfView < _cameraMover.MaxZoom)
                {
                    _cameraMover.ZoomOut();
                    yield return null;
                }
            }
        }
    }
}