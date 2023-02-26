using System.Collections.Generic;
using UnityEngine;

public class Fence : MonoBehaviour
{
    //[SerializeField] private float rotationSpeed;
    //[SerializeField] private GameObject baseBoard;
    //[SerializeField] private GameObject[] boards;

    //private FenceCollision targetFence;
    //private List<FenceCollision> fences;

    //private void Start()
    //{
    //    fences = new List<FenceCollision>();
    //}

    //private void Update()
    //{
    //    if (targetFence != null)
    //    {
    //        Vector3 direction = targetFence.transform.position - baseBoard.transform.position;
    //        Quaternion lookRotation = Quaternion.LookRotation(direction);
    //        baseBoard.transform.rotation = Quaternion.Slerp(baseBoard.transform.rotation, lookRotation, Time.deltaTime * rotationSpeed);
    //    }
    //}

    //private void OnTriggerEnter(Collider other)
    //{
    //    FenceCollision fence = other.GetComponent<FenceCollision>();

    //    if (fence != null)
    //    {
    //        fences.Add(fence);
    //        SetTargetFence();
    //    }
    //}

    //private void OnTriggerExit(Collider other)
    //{
    //    FenceCollision fence = other.GetComponent<FenceCollision>();

    //    if (fence != null)
    //    {
    //        fences.Remove(fence);
    //        SetTargetFence();
    //    }
    //}

    //private void SetTargetFence()
    //{
    //    if (fences.Count > 0)
    //    {
    //        float shortestDistance = float.MaxValue;
    //        FenceCollision nearestFence = null;

    //        foreach (var fence in fences)
    //        {
    //            if (fence != null)
    //            {
    //                float distanceToFence = Vector3.Distance(baseBoard.transform.position, fence.transform.position);

    //                if (distanceToFence < shortestDistance)
    //                {
    //                    shortestDistance = distanceToFence;
    //                    nearestFence = fence;
    //                }
    //            }
    //        }

    //        if (nearestFence != null)
    //        {
    //            targetFence = nearestFence;
    //        }
    //        else
    //        {
    //            targetFence = null;
    //        }
    //    }
    //    else
    //    {
    //        targetFence = null;
    //    }
    //}
}



