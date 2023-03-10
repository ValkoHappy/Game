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

    public PeacefulConstruction Construction => _construction;
    public EnemyCollision TargetEnemy { get; private set; }

    private void Awake()
    {
        _shootTurret = GetComponent<ShootTurret>();
        _construction = GetComponentInChildren<PeacefulConstruction>();
    }

    private void Start()
    {
        _enemies = new List<EnemyCollision>();
    }

    private void Update()
    {
        if (TargetEnemy != null && TargetEnemy.IsAlive() && _construction.IsAlive())
        {
            Vector3 direction = TargetEnemy.transform.position - transform.position;
            Quaternion lookRotation = Quaternion.LookRotation(direction);
            _partToRotate.rotation = Quaternion.Slerp(_partToRotate.rotation, lookRotation, Time.deltaTime * _rotationSpeed);
        }
        //else if (TargetEnemy != null && TargetEnemy.IsAlive() == false && _construction.IsAlive())
        //{
        //        _partToRotate.rotation = Quaternion.Slerp(_partToRotate.rotation, Quaternion.LookRotation(new Vector3(0,0,0)), Time.deltaTime * _rotationSpeed);
        //}

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
                _shootTurret.RestartShoot();
            }
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.TryGetComponent(out EnemyCollision enemy))
        {
            if (enemy.IsAlive() == false)
            {
                _enemies.Remove(enemy);
                _shootTurret.StopShoot();
            }
        }
    }
}

