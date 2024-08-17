using System;
using System.Collections.Generic;
using Scripts.Build.Struct;
using Scripts.Health;
using UnityEngine;

namespace Scripts.Build
{
    [RequireComponent(typeof(HealthHandler))]
    public class PeacefulConstruction : MonoBehaviour
    {
        [SerializeField] private float _bounceForce;
        [SerializeField] private float _bounceRadius;

        private HealthHandler _healthContainer;
        private BuildingDetail[] _buildingDetails;
        private Building _building;
        private List<BuildingDetailSnapshot> _detailSnapshots = new List<BuildingDetailSnapshot>();

        public event Action<PeacefulConstruction> ConstructionDied;
        public event Action Died;
        public event Action Damaged;
        public event Action BuildingRestored;

        public Building Building => _building;
        public bool IsAlive => _healthContainer.Health <= 0 ? false : true;


        private void Awake()
        {
            _healthContainer = GetComponent<HealthHandler>();
            _building = GetComponentInParent<Building>();
        }

        private void Start()
        {
            _buildingDetails = GetComponentsInChildren<BuildingDetail>();
        }

        private void OnEnable()
        {
            _healthContainer.Died += OnDie;
        }

        private void OnDisable()
        {
            _healthContainer.Died -= OnDie;
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
                if (_detailSnapshots.Count > 0)
                {
                    _buildingDetails[i].transform.position = _detailSnapshots[i].Position;
                    _buildingDetails[i].transform.localRotation = _detailSnapshots[i].Rotation;
                    _buildingDetails[i].ResetBounce();
                }

                _healthContainer.Clear();
            }

            BuildingRestored?.Invoke();
            _detailSnapshots.Clear();
        }

        public void Destroy()
        {
            Destroy(_building.gameObject);
        }

        private void OnDie()
        {
            Died?.Invoke();
            ConstructionDied?.Invoke(this);
            Break();
        }

        private void Break()
        {
            foreach (var detail in _buildingDetails)
            {
                _detailSnapshots.Add(new BuildingDetailSnapshot()
                {
                    Position = detail.transform.position,
                    Rotation = detail.transform.localRotation,
                });
                detail.Bounce(_bounceForce, transform.position, _bounceRadius);
            }
        }
    }
}