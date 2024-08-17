using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Scripts.Camera
{
    [RequireComponent(typeof(Image))]
    public class Joystick : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IDragHandler
    {
        private Image _joystick;
        private Image _touch;
        private Vector2 _inputPos;

        private int _speedIncrease = 2;
        private float _maxInputMagnitude = 1.0f;
        private float _joystickSizeFactor = 2.0f;

        private void Awake()
        {
            _joystick = GetComponent<Image>();
            _touch = transform.GetChild(0).GetComponent<Image>();
        }

        public void OnDrag(PointerEventData eventData)
        {
            Vector2 position;

            if (RectTransformUtility.ScreenPointToLocalPointInRectangle(_joystick.rectTransform, eventData.position, eventData.pressEventCamera, out position))
            {
                position.x /= _joystick.rectTransform.sizeDelta.x;
                position.y /= _joystick.rectTransform.sizeDelta.x;
            }

            _inputPos = new(position.x, position.y);
            _inputPos = (_inputPos.magnitude > _maxInputMagnitude) ? _inputPos.normalized : _inputPos;
            _touch.rectTransform.anchoredPosition = new(_inputPos.x * (_joystick.rectTransform.sizeDelta.x / _joystickSizeFactor), _inputPos.y * (_joystick.rectTransform.sizeDelta.y / _joystickSizeFactor));
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            OnDrag(eventData);
        }

        public void OnPointerUp(PointerEventData eventData)
        {
            _inputPos = Vector2.zero;
            _touch.rectTransform.anchoredPosition = Vector2.zero;
        }

        public Vector3 GetDirection()
        {
            Vector3 direction = Vector3.zero;

            if (_inputPos.x != 0)
                direction.x = _inputPos.x;

            if (_inputPos.y != 0)
                direction.z = _inputPos.y;

            return direction * _speedIncrease;
        }
    }
}