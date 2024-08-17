using System;
using Scripts.Health;
using Scripts.Interfaces;
using UnityEngine;

namespace Scripts.Enemy
{
    [RequireComponent(typeof(HealthHandler))]
    [RequireComponent(typeof(BoxCollider))]
    public class EnemyCollision : MonoBehaviour, IDamageable
    {
        private Enemy _enemy;
        private HealthHandler _healthContainer;

        public event Action<EnemyCollision> Died;

        public bool IsAlive => _healthContainer.Health <= 0 ? false : true;

        private void Awake()
        {
            _healthContainer = GetComponent<HealthHandler>();
            _enemy = GetComponentInParent<Enemy>();
        }

        private void OnEnable()
        {
            _healthContainer.Died += OnDied;
        }

        private void OnDisable()
        {
            _healthContainer.Died -= OnDied;
        }

        public bool ApplayDamage(Rigidbody rigidbody, int damage, int force)
        {
            if (_enemy.CurrentState != _enemy.BrokenState)
            {
                _enemy.ApplayDamage(rigidbody, damage, force);
                return true;
            }

            return false;
        }

        protected void OnDied()
        {
            enabled = false;
            Died?.Invoke(this);
        }
    }
}