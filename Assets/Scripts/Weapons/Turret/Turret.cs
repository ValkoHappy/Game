using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(ShootTurret), typeof(SphereCollider))]
public class Turret : MonoBehaviour
{
    [SerializeField] private Transform _partToRotate;
    [SerializeField] private float _rotationSpeed;

    private ShootTurret _shootTurret;
    private List<EnemyCollision> _enemies;
    private PeacefulConstruction _construction;
    private SphereCollider _sphereCollider;
    private BuildingsGrid _buildingsGrid;
    private Spawner _spawner;
    private Coroutine _removeCoroutine;

    public PeacefulConstruction Construction => _construction;
    public EnemyCollision TargetEnemy { get; private set; }

    private void Awake()
    {
        _buildingsGrid = FindObjectOfType<BuildingsGrid>();
        _shootTurret = GetComponent<ShootTurret>();
        _construction = GetComponentInChildren<PeacefulConstruction>();
        _sphereCollider = GetComponent<SphereCollider>();
    }

    private void OnEnable()
    {
        _buildingsGrid.DeliveredBuilding += TurnOnCollider;
        _buildingsGrid.CreatedBuilding += TurnOffCollider;
        _construction.Died += RemoveAllEnemies;
    }

    private void OnDisable()
    {
        _buildingsGrid.DeliveredBuilding -= TurnOnCollider;
        _buildingsGrid.CreatedBuilding -= TurnOffCollider;
        _construction.Died -= RemoveAllEnemies;
    }

    private void Start()
    {
        _enemies = new List<EnemyCollision>();
        _sphereCollider.enabled = false;
        //StartRemove();
    }

    private void Update()
    {
        if (TargetEnemy != null && TargetEnemy.IsAlive() && _construction.IsAlive())
        {
            Vector3 direction = TargetEnemy.transform.position - transform.position;
            Quaternion lookRotation = Quaternion.LookRotation(direction);
            _partToRotate.rotation = Quaternion.Slerp(_partToRotate.rotation, lookRotation, Time.deltaTime * _rotationSpeed);
        }

        if (TargetEnemy == null && _construction.IsAlive() && _partToRotate.rotation != Quaternion.Euler(new Vector3(0, 0, 0)))
        {
            _partToRotate.rotation = Quaternion.Slerp(_partToRotate.rotation, Quaternion.LookRotation(new Vector3(0, 0, 0)), Time.deltaTime * _rotationSpeed);
        }

        if (_enemies.Count > 0)
        {
            float shortestDistance = Mathf.Infinity;
            EnemyCollision nearestEnemy = null;

            foreach (var target in _enemies)
            {
                if (target.IsAlive() && target != null)
                {
                    float distanceToEnemy = Vector3.Distance(transform.position, target.transform.position);

                    if (distanceToEnemy < shortestDistance)
                    {
                        shortestDistance = distanceToEnemy;
                        nearestEnemy = target;
                    }
                }

            }

            if (nearestEnemy != null && nearestEnemy.IsAlive())
            {
                TargetEnemy = nearestEnemy;
                if(TargetEnemy.IsAlive())
                    _shootTurret.RestartShoot();
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out EnemyCollision enemy))
        {
            if (enemy != null && enemy.IsAlive())
            {
                _enemies.Add(enemy);
            }
        }
    }

    //public void StartRemove()
    //{
    //    if (_removeCoroutine != null)
    //    {
    //        StopCoroutine(_removeCoroutine);
    //    }
    //    _removeCoroutine = StartCoroutine(RemoveBuilding());

    //}

    //private IEnumerator RemoveBuilding()
    //{
    //    var waitForSeconds = new WaitForSeconds(5f);
    //    if (_enemies.Count > 0)
    //    {
    //        foreach (var enemy in _enemies)
    //        {
    //            if (enemy != null && enemy.IsAlive() == false)
    //            {
    //                _enemies.Remove(enemy);
    //                _shootTurret.StopShoot();
    //            }
    //        }
    //    }
    //    yield return waitForSeconds;
    //    StartRemove();
    //}

    private void RemoveAllEnemies()
    {
        _enemies.Clear();
    }

    private void TurnOnCollider()
    {
        if (_sphereCollider != null) 
            _sphereCollider.enabled = true;
    }

    private void TurnOffCollider()
    {
        if(_sphereCollider != null )
            _sphereCollider.enabled = false;
    }
}

