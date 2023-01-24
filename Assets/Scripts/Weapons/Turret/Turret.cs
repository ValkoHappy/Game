using System.Collections.Generic;
using UnityEngine;

public class Turret : MonoBehaviour
{
    [SerializeField] private Transform _partToRotate;
    [SerializeField] private float _rotationSpeed;

    private List<Enemy> _enemies = new List<Enemy>();
    public Vector3 TargetEnemy { get; private set; }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out Enemy enemy))
        {
            if (enemy != null && enemy.IsAlive())
            {
                _enemies.Add(enemy);
            }
        }
    }

    private void Update()
    {
        if (TargetEnemy != null)
        {
            Vector3 direction = TargetEnemy - transform.position;
            Quaternion lookRotation = Quaternion.LookRotation(direction);
            _partToRotate.rotation = Quaternion.Slerp(_partToRotate.rotation, lookRotation, Time.deltaTime * _rotationSpeed);
        }
        if(_enemies.Count > 0)
        {
            float shortestDistance = Mathf.Infinity;
            Enemy nearestEnemy = null;

            foreach (var target in _enemies)
            {
                float distanceToEnemy = Vector3.Distance(transform.position, target.transform.position);

                if (distanceToEnemy < shortestDistance)
                {
                    shortestDistance = distanceToEnemy;
                    nearestEnemy = target;
                }
            }

            if (nearestEnemy != null && nearestEnemy.IsAlive())
            {
                TargetEnemy = nearestEnemy.transform.position;
            }
        }
    }
}

