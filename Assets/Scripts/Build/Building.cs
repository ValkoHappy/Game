using UnityEngine;
using UnityEngine.Events;

public class Building : MonoBehaviour
{
    [SerializeField] private Vector2Int _size;

    private Renderer[] _renderers;
    private Color[] _originalColors;
    private bool _isBuilding;
    public PeacefulConstruction PeacefulConstruction { get; private set; }

    public event UnityAction DeliveryBuilding;
    public event UnityAction CreateBuilding;

    public Vector2Int TileSize => _size;
    public bool IsBuilding => _isBuilding;

    private void Awake()
    {
        PeacefulConstruction = GetComponentInChildren<PeacefulConstruction>();
        _renderers = GetComponentsInChildren<Renderer>();
        _originalColors = new Color[_renderers.Length];
        for (int i = 0; i < _renderers.Length; i++)
        {
            _originalColors[i] = _renderers[i].material.color;
        }
    }

    public void SetTransparent(bool available)
    {
        if (available)
        {
            SetColor(new Color(0, 1, 0, 0.3f));
            _isBuilding = true;
        }
        else
        {
            SetColor(new Color(1, 0, 0, 0.3f));
            _isBuilding = false;
        }
    }

    public void Create()
    {
        CreateBuilding?.Invoke();
    }

    public void SetNormal()
    {
        for (int i = 0; i < _renderers.Length; i++)
        {
            _renderers[i].material.color = _originalColors[i];
        }
        DeliveryBuilding?.Invoke();
    }

    private void SetColor(Color color)
    {
        foreach (var renderer in _renderers)
        {
            renderer.material.color = color;
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
