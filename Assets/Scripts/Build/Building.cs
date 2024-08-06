using System;
using UnityEngine;

public class Building : MonoBehaviour
{
    [SerializeField] private Vector2Int _size;

    private PeacefulConstruction _peacefulConstruction;
    private BuildingCharacteristics _buildingCharacteristics;

    private Renderer[] _renderers;

    private Color _positiveColor = new Color(0, 1, 0, 0.3f);
    private Color _negativeColor = new Color(1, 0, 0, 0.3f);
    private Color[] _originalColors;

    public event Action Delivered;
    public event Action Created;

    public Vector2Int TileSize => _size;
    public PeacefulConstruction PeacefulConstruction => _peacefulConstruction;
    public BuildingCharacteristics BuildingCharacteristics => _buildingCharacteristics;

    private void Awake()
    {
        _buildingCharacteristics = GetComponentInChildren<BuildingCharacteristics>();
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
            SetColor(_positiveColor);
        else
            SetColor(_negativeColor);
    }

    public void Create()
    {
        Created?.Invoke();
    }

    public void SetNormal()
    {
        for (int i = 0; i < _renderers.Length; i++)
        {
            _renderers[i].material.color = _originalColors[i];
        }

        Delivered?.Invoke();
    }

    private void SetColor(Color color)
    {
        foreach (var renderer in _renderers)
        {
            renderer.material.color = color;
        }
    }
}
