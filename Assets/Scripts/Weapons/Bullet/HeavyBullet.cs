using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeavyBullet : Bullet
{
    [SerializeField] private float explosionRadius = 5f;
    [SerializeField] private float explosionForce = 700f;

    private HashSet<IDamageable> _processedEnemies = new HashSet<IDamageable>();

    private void OnCollisionEnter(Collision collision)
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, explosionRadius);
        foreach (Collider nearbyObject in colliders)
        {
            IDamageable damageableObject = nearbyObject.GetComponent<IDamageable>();
            if (damageableObject != null && !_processedEnemies.Contains(damageableObject))
            {
                Rigidbody rb = nearbyObject.GetComponent<Rigidbody>();
                if (rb != null)
                {
                    rb.AddExplosionForce(explosionForce, transform.position, explosionRadius);
                }

                damageableObject.TakeDamage(Damage);
                _processedEnemies.Add(damageableObject);
            }
        }
        Destroy(gameObject);
    }

}
