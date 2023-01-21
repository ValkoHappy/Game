using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(RangeCollider))]
public class Turret : MonoBehaviour
{
    [SerializeField] private Transform _partToRotate;
    [SerializeField] public float _range;
    [SerializeField] private float _rotationSpeed;

    private RangeCollider _collider;
    public Transform TargetEnemy { get; private set; }

    private void Awake()
    {
        _collider = GetComponent<RangeCollider>();
    }

    private void Start()
    {
        _collider.GetRange(_range);
    }

    //public Transform FindTarget()
    //{
    //    TargetEnemy = null;
    //    Enemy[] enemies = FindObjectsOfType<Enemy>();
    //    float shortestDistance = Mathf.Infinity;
    //    Enemy nearestEnemy = null;

    //    foreach (var enemy in enemies)
    //    {
    //        float distanceToEnemy = Vector3.Distance(transform.position, enemy.transform.position);

    //        if(distanceToEnemy < shortestDistance)
    //        {
    //            shortestDistance = distanceToEnemy;
    //            nearestEnemy = enemy;
    //        }
    //    }

    //    if(nearestEnemy != null && shortestDistance <= _range)
    //    {
    //        return TargetEnemy = nearestEnemy.transform;
    //    }
    //    else
    //    {
    //        return TargetEnemy = null;
    //    }
    //}

    private void Update()
    {
        if (TargetEnemy != null)
        {
            Vector3 direction = TargetEnemy.position - transform.position;
            Quaternion lookRotation = Quaternion.LookRotation(direction);
            Vector3 rotation = Quaternion.Lerp(_partToRotate.rotation, lookRotation, _rotationSpeed * Time.deltaTime).eulerAngles;
            _partToRotate.rotation = Quaternion.Euler(0, rotation.y, 0);
        }         
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.TryGetComponent(out Enemy enemy))
        {
            if(enemy != null)
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

                if (nearestEnemy != null)
                {
                    TargetEnemy = nearestEnemy.transform;
                }
                else
                {
                    TargetEnemy = null;
                }
            }
        }
    }

    //private void OnDrawGizmosSelected()
    //{
    //    Gizmos.color = Color.red;
    //    Gizmos.DrawWireSphere(transform.position, _range);
    //}
}

