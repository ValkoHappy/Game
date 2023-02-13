using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fence : MonoBehaviour
{
    //[SerializeField] private float _distanceToCheck = 2f; // The distance at which to check for existing fences
    //[SerializeField] private float _thresholdAngle = 30f; // The maximum angle difference between two fences for them to be considered connected

    //private void Update()
    //{
    //    // Check for existing fences and adjust this fence's rotation to match
    //    Vector3 forward = transform.forward;
    //    Collider[] hits = Physics.OverlapSphere(transform.position, _distanceToCheck);
    //    foreach (Collider hit in hits)
    //    {
    //        Fence otherFence = hit.GetComponent<Fence>();
    //        if (otherFence == null || otherFence == this) continue;

    //        Vector3 otherForward = otherFence.transform.forward;
    //        float angle = Vector3.Angle(forward, otherForward);
    //        if (angle < _thresholdAngle)
    //        {
    //            transform.rotation = Quaternion.LookRotation(otherForward);
    //            break;
    //        }
    //    }

    //    // If no existing fences were found, remove all but one column of the fence
    //    if (transform.childCount > 1)
    //    {
    //        Transform column = transform.GetChild(0);
    //        for (int i = transform.childCount - 1; i > 0; i--)
    //        {
    //            Destroy(transform.GetChild(i).gameObject);
    //        }
    //        column.localPosition = Vector3.zero;
    //    }
    //}
}
