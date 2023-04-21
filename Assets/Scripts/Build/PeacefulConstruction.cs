using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(HealthContainer))]
public class PeacefulConstruction : MonoBehaviour
{
    [SerializeField] private float _bounceForce;
    [SerializeField] private float _bounceRadius;

    public event UnityAction<PeacefulConstruction> Die;
    public event UnityAction Died;
    public event UnityAction Damaged;
    private HealthContainer _healthContainer;
    private bool _isAlive;
    private BuildingDetail[] _buildingDetails;
    private Building _building;
    private List<BuildingDetailSnapshot> _detailSnapshots = new List<BuildingDetailSnapshot>();

    public event UnityAction BuildingRestored;

    public HealthContainer HealthContainer => _healthContainer;
    public Building Building => _building;

    private void Awake()
    {
        _healthContainer = GetComponent<HealthContainer>();
        _building = GetComponentInParent<Building>();
    }

    private void Start()
    {
        _buildingDetails = GetComponentsInChildren<BuildingDetail>();
    }

    private void OnEnable()
    {
        _healthContainer.Died += OnDied;
    }

    private void OnDisable()
    {
        _healthContainer.Died -= OnDied;
    }

    public bool IsAlive()
    {
        if (_healthContainer.Health <= 0)
        {
            return _isAlive = false;
        }
        else
        {
            return _isAlive = true;
        }
    }

    public void ApplyDamage(float damage)
    {
        _healthContainer.TakeDamage((int)damage);
        Damaged?.Invoke();
    }

    public void ResetDetails()
    {
        for (int i = 0; i < _buildingDetails.Length; i++)
        {
            if(_detailSnapshots.Count > 0)
            {
                _buildingDetails[i].transform.position = _detailSnapshots[i].Position;
                _buildingDetails[i].transform.localRotation = _detailSnapshots[i].Rotation;
                _buildingDetails[i].ResetBounce();
            }
            _healthContainer.ResetHealth();
            _isAlive = true;
        }
        BuildingRestored?.Invoke();
        _detailSnapshots.Clear();
    }

    public void Destroy()
    {
        Destroy(_building.gameObject);
    }

    private void OnDied()
    {
        Died?.Invoke();
        Die?.Invoke(this);
        Break();
    }

    private struct BuildingDetailSnapshot
    {
        public Vector3 Position;
        public Quaternion Rotation;
    }

    private void Break()
    {
        foreach (var detail in _buildingDetails)
        {
            _detailSnapshots.Add(new BuildingDetailSnapshot()
            {
                Position = detail.transform.position,
                Rotation = detail.transform.localRotation
            });
            detail.Bounce(_bounceForce, transform.position, _bounceRadius);
        }
    }
}
