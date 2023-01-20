using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turret : MonoBehaviour
{
    [SerializeField] private Transform _partToRotate;
    [SerializeField] public float _range;
    [SerializeField] private float _rotationSpeed;

    public Transform TargetEnemy { get; private set; }

    public Transform FindTarget()
    {
        TargetEnemy = null;
        Enemy[] enemies = FindObjectsOfType<Enemy>();
        float shortestDistance = Mathf.Infinity;
        Enemy nearestEnemy = null;

        foreach (var enemy in enemies)
        {
            float distanceToEnemy = Vector3.Distance(transform.position, enemy.transform.position);

            if(distanceToEnemy < shortestDistance)
            {
                shortestDistance = distanceToEnemy;
                nearestEnemy = enemy;
            }
        }

        if(nearestEnemy != null && shortestDistance <= _range)
        {
            return TargetEnemy = nearestEnemy.transform;
        }
        else
        {
            return TargetEnemy = null;
        }
    }

    private void Update()
    {
        if (FindTarget())
        {
            Vector3 direction = TargetEnemy.position - transform.position;
            Quaternion lookRotation = Quaternion.LookRotation(direction);
            Vector3 rotation = Quaternion.Lerp(_partToRotate.rotation, lookRotation, _rotationSpeed * Time.deltaTime).eulerAngles;
            _partToRotate.rotation = Quaternion.Euler(0, rotation.y, 0);
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, _range);
    }
}

