using UnityEngine;

[RequireComponent(typeof(SphereCollider))]
public class RangeCollider : MonoBehaviour
{
    private SphereCollider _collider;

    private void Awake()
    {
        _collider = GetComponent<SphereCollider>();
    }

    public void SetRange(float radius)
    {
        _collider.radius = radius;
    }
}
