using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private int _damage;
    [SerializeField] private int _force;
    [SerializeField] private float _speed;
    [SerializeField] private float _yOffSet;
    [SerializeField] private float _radius;
    [SerializeField] private ParticleSystem _particle;

    private Rigidbody _rigidbody;
    
    public int Damage => _damage;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        Vector3 direction = transform.forward;
        _rigidbody.velocity = direction.normalized * _speed;
        Destroy(gameObject, 3);
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out EnemyCollision enemy))
        {
            ApplyDamageToEnemy(enemy);
        }
    }

    private void ApplyDamageToEnemy(EnemyCollision enemy)
    {
        if (enemy.ApplayDamage(_rigidbody, _damage, _force))
        {
            if (_particle != null)
            {
                _particle.transform.position = transform.position;
                _particle.Play();
            }
            Instantiate(_particle);
            Destroy(gameObject);
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.TryGetComponent(out EnemyCollision enemy))
        {
            if(enemy != null)
            {
                Collider[] colliders = Physics.OverlapSphere(transform.position, _radius);
                foreach (Collider collider in colliders)
                {
                    if (collider.TryGetComponent(out EnemyCollision enemyCollision))
                    {
                        ApplyDamageToEnemy(enemyCollision);
                    }
                }
            }
        }
    }
}

