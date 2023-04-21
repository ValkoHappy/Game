using UnityEngine;
using UnityEngine.Events;

public class Building : MonoBehaviour
{
    [SerializeField] private Vector2Int _size;

    private Color _color1 = new Color(0, 1, 0, 0.3f);
    private Color _color2 = new Color(1, 0, 0, 0.3f);
    private Color[] _originalColors;
    private Renderer[] _renderers;
    private bool _isBuilding;
    private PeacefulConstruction _peacefulConstruction;

    public event UnityAction DeliveryBuilding;
    public event UnityAction CreateBuilding;

    public Vector2Int TileSize => _size;
    public bool IsBuilding => _isBuilding;
    public PeacefulConstruction PeacefulConstruction => _peacefulConstruction;

    private void Awake()
    {
        _peacefulConstruction = GetComponentInChildren<PeacefulConstruction>();
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
            SetColor(_color1);
            _isBuilding = true;
        }
        else
        {
            SetColor(_color2);
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
}
