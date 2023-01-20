using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private float _damage;
    [SerializeField] private float _speed;
    [SerializeField] private float _yOffSet;

    private Transform _targetEnemy;

    private void Update()
    {
        if(_targetEnemy != null)
        {
            Vector3 direction = _targetEnemy.position - transform.position;
            float distance = _speed * Time.deltaTime;

            transform.Translate(new Vector3(direction.x, direction.y + _yOffSet, direction.z).normalized * distance, Space.World);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void Seek(Transform target)
    {
        _targetEnemy = target;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.TryGetComponent(out Enemy enemy))
        {
            enemy.ApplyDamage(_damage);
            Destroy(gameObject);
        }
    }
}
