using System.Collections;
using System.Collections.Generic;
using Scripts.Build;
using Scripts.Enemy;
using UnityEngine;

namespace Scripts.Weapons.Turret
{
    [RequireComponent(typeof(AttackTurret))]
    [RequireComponent(typeof(SphereCollider))]
    public class Turret : MonoBehaviour
    {
        [SerializeField] private Transform _partToRotate;
        [SerializeField] private float _rotationSpeed;

        private AttackTurret _shootTurret;
        private List<EnemyCollision> _enemies;
        private PeacefulConstruction _construction;
        private SphereCollider _sphereCollider;
        private EnemyHandler _enemyHandler;
        private Coroutine _sortCoroutine;
        private WaitForSeconds _sortWait = new WaitForSeconds(0.5f);

        public PeacefulConstruction Construction => _construction;
        public EnemyCollision TargetEnemy { get; private set; }

        private void Awake()
        {
            _shootTurret = GetComponent<AttackTurret>();
            _construction = GetComponentInChildren<PeacefulConstruction>();
            _sphereCollider = GetComponent<SphereCollider>();
            _enemyHandler = FindObjectOfType<EnemyHandler>();
        }

        private void OnEnable()
        {
            _enemyHandler.EnemiesIncluded += OnEnableCollider;
            _enemyHandler.EnemiesRemoved += OnTurnOffCollider;
            _construction.Died += OnRemoveAllEnemies;
        }

        private void OnDisable()
        {
            _enemyHandler.EnemiesIncluded -= OnEnableCollider;
            _enemyHandler.EnemiesRemoved -= OnTurnOffCollider;
            _construction.Died -= OnRemoveAllEnemies;
        }

        private void Start()
        {
            _enemies = new List<EnemyCollision>();
            _sphereCollider.enabled = false;
            StartSortEnemies();
        }

        private void Update()
        {
            if (_enemyHandler.IsAttackBegun && TargetEnemy != null && TargetEnemy.IsAlive && _construction.IsAlive)
            {
                Vector3 direction = TargetEnemy.transform.position - transform.position;
                Quaternion lookRotation = Quaternion.LookRotation(direction);

                _partToRotate.rotation = Quaternion.Slerp(
                    _partToRotate.rotation,
                    lookRotation,
                    Time.deltaTime * _rotationSpeed);
            }

            if (TargetEnemy == null && _construction.IsAlive
                && _partToRotate.rotation != Quaternion.Euler(new Vector3(0, 0, 0)))
            {
                _partToRotate.rotation = Quaternion.Slerp(
                    _partToRotate.rotation,
                    Quaternion.LookRotation(new Vector3(0, 0, 0)),
                    Time.deltaTime * _rotationSpeed);
            }
        }

        private void StartSortEnemies()
        {
            if (_sortCoroutine != null)
            {
                StopCoroutine(_sortCoroutine);
                _sortCoroutine = null;
            }

            _sortCoroutine = StartCoroutine(SortEnemies());
        }

        private IEnumerator SortEnemies()
        {
            if (_enemies.Count > 0)
            {
                float shortestDistance = Mathf.Infinity;
                EnemyCollision nearestEnemy = null;

                foreach (var target in _enemies)
                {
                    if (target.IsAlive && target != null)
                    {
                        float distanceToEnemy = Vector3.Distance(transform.position, target.transform.position);

                        if (distanceToEnemy < shortestDistance)
                        {
                            shortestDistance = distanceToEnemy;
                            nearestEnemy = target;
                        }
                    }
                }

                if (nearestEnemy != null && nearestEnemy.IsAlive)
                {
                    TargetEnemy = nearestEnemy;

                    if (TargetEnemy.IsAlive)
                        _shootTurret.Restart();
                }
            }

            yield return _sortWait;

            StartSortEnemies();
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent(out EnemyCollision enemy))
            {
                if (enemy != null && enemy.IsAlive)
                    _enemies.Add(enemy);
            }
        }

        private void OnRemoveAllEnemies()
        {
            _enemies.Clear();
        }

        private void OnEnableCollider()
        {
            if (_sphereCollider != null)
                _sphereCollider.enabled = true;
        }

        private void OnTurnOffCollider()
        {
            if (_sphereCollider != null)
                _sphereCollider.enabled = false;
        }
    }
}