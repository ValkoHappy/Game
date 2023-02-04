using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Building : MonoBehaviour
{
    [SerializeField] private Vector2Int _size;

    private Renderer[] _renderers;

    public Vector2Int Size => _size;

    private void Awake()
    {
        _renderers = GetComponentsInChildren<Renderer>();
    }

    public void SetTransparent(bool available)
    {
        if (available)
        {
            foreach (var renderer in _renderers)
            {
                renderer.material.color = Color.green;
            }
        }
        else
        {
            foreach (var renderer in _renderers)
            {
                renderer.material.color = Color.red;
            }
        }
    }

    public void SetNormal()
    {
        foreach (var renderer in _renderers)
        {
            renderer.material.color = Color.white;
        }
    }

    private void OnDrawGizmos()
    {
        for (int x = 0; x < _size.x; x++)
        {
            for (int y = 0; y < _size.y; y++)
            {
                Gizmos.color = new Color(0,1,0,0.3f);
                Gizmos.DrawCube(transform.position + new Vector3(x, 0, y), new Vector3(1, .1f, 1));
            }
        }
    }
}
