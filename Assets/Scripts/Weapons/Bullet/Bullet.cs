using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private int _damage;
    [SerializeField] private int _force;
    [SerializeField] private float _speed;
    [SerializeField] private float _yOffSet;
    [SerializeField] private ParticleSystem _particle;

    private Vector3 _targetEnemy;
    private Rigidbody _rigidbody;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        if(_targetEnemy != null)
        {
            Vector3 direction = _targetEnemy - transform.position;
            float distance = _speed * Time.deltaTime;
            transform.Translate(direction * distance, Space.World);
            transform.LookAt(_targetEnemy);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void Seek(Transform transform)
    {
        _targetEnemy = transform.position;
        _targetEnemy.y = transform.position.y + _yOffSet;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.TryGetComponent(out EnemyCollision enemy))
        {
            if(enemy.ApplayDamage(_rigidbody, _damage, _force))
            {
                if(_particle!= null)
                {
                    _particle.transform.position = transform.position;
                    _particle.Play();
                }
                Instantiate(_particle);
                Destroy(gameObject);
            }
        }
        if (other.TryGetComponent(out Ground ground))
        {
            Destroy(gameObject);
        }
    }
}
