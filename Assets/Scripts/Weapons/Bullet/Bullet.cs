using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private int _damage;
    [SerializeField] private int _force;
    [SerializeField] private float _speed;
    [SerializeField] private float _yOffSet;
    [SerializeField] private float _radius;
    [SerializeField] private ParticleSystem _particle;

    private Vector3 _targetEnemy;
    private Rigidbody _rigidbody;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        if (_targetEnemy != null)
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
            // Get all the colliders within the sphere
            Collider[] colliders = Physics.OverlapSphere(transform.position, _radius);
            foreach (Collider collider in colliders)
            {
                // Check if the collider has an EnemyCollision component
                if (collider.TryGetComponent(out EnemyCollision enemyCollision))
                {
                    ApplyDamageToEnemy(enemyCollision);
                }
            }
        }
    }
}

