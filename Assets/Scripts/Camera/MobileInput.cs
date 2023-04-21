using UnityEngine;

[RequireComponent(typeof(Joystick))]
public class MobileInput : MonoBehaviour
{
    [SerializeField] private CameraMover _cameraMover;

    private Joystick _joystick;

    private void Awake()
    {
        _cameraMover = FindObjectOfType<CameraMover>();
        _joystick = GetComponent<Joystick>();
    }

    private void Update()
    {
        _cameraMover.Move(_joystick.GetDirection());
    }
}