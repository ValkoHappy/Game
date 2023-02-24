using UnityEngine;
using System.Collections;
using RTS_Cam;

[RequireComponent(typeof(RTS_Camera))]
public class TargetSelector : MonoBehaviour
{
    private RTS_Camera _cam;
    private Camera _camera;
    public string TargetTag;

    private void Start()
    {
        _cam = GetComponent<RTS_Camera>();
        _camera = GetComponent<Camera>();
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = _camera.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit))
            {
                if (hit.transform.CompareTag(TargetTag))
                    _cam.SetTarget(hit.transform, true);
                else
                    _cam.ResetTarget();
            }
        }
    }
}
