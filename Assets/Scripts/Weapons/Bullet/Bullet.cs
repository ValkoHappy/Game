using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private float _damage;
    [SerializeField] private float _speed;
    [SerializeField] private float _yOffSet;

    private Vector3 _targetEnemy;

    private void Update()
    {
        if(_targetEnemy != null)
        {

            Vector3 direction = _targetEnemy - transform.position;
            float distance = _speed * Time.deltaTime;
            //transform.LookAt(_targetEnemy);
            transform.Translate(new Vector3(direction.x, direction.y + _yOffSet, direction.z) * distance, Space.World); ;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void Seek(Transform transform)
    {
        _targetEnemy = transform.position;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.TryGetComponent(out EnemyCollision enemy))
        {
            enemy.ApplyDamage(_damage);
            Destroy(gameObject);
        }
        if (other.TryGetComponent(out Ground ground))
        {
            Destroy(gameObject);
        }
    }
}
