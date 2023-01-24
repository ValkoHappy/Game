using System.Collections.Generic;
using UnityEngine;

public class Turret : MonoBehaviour
{
    [SerializeField] private Transform _partToRotate;
    [SerializeField] private float _rotationSpeed;
    public Transform TargetEnemy { get; private set; }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out Enemy enemy))
        {
            if (enemy != null && enemy.IsAlive())
            {
                List<Enemy> enemies = new List<Enemy>();
                enemies.Add(enemy);
                float shortestDistance = Mathf.Infinity;
                Enemy nearestEnemy = null;

                foreach (var target in enemies)
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
                    TargetEnemy = nearestEnemy.transform;
                }
            }
        }
    }

    private void Update()
    {
        if (TargetEnemy != null)
        {
            Vector3 direction = TargetEnemy.position - transform.position;
            Quaternion lookRotation = Quaternion.LookRotation(direction);
            _partToRotate.rotation = Quaternion.Slerp(_partToRotate.rotation, lookRotation, Time.deltaTime * _rotationSpeed);
        }
    }
}

