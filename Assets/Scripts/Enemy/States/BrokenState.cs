using System;
using System.Collections;
using UnityEngine;

namespace Scripts.Enemy.States
{
    public class BrokenState : EnemyState
    {
        private const string Die = "Die";

        [SerializeField] private float _fadeTime = 3.0f;
        [SerializeField] private ParticleSystem _particleDied;

        private Material enemyMaterial;

        public event Action Died;

        private void Awake()
        {
            enemyMaterial = GetComponentInChildren<Renderer>().material;
        }

        public void ApplyDamage(Rigidbody attachedBody, float force)
        {
            Vector3 impactDirection = (attachedBody.position - transform.position).normalized;
            Animator.SetTrigger(Die);
            Rigidbody.AddForce(impactDirection * force, ForceMode.Impulse);
            StartCoroutine(FadeOut());
            Died?.Invoke();
        }

        private IEnumerator FadeOut()
        {
            Color enemyColor = enemyMaterial.color;
            float elapsedTime = 0.0f;
            Vector3 position = transform.position;

            while (elapsedTime < _fadeTime)
            {
                float alpha = Mathf.Lerp(1.0f, 0.0f, elapsedTime / _fadeTime);
                enemyColor.a = alpha;
                enemyMaterial.color = enemyColor;
                elapsedTime += Time.deltaTime;
                yield return null;
            }

            Instantiate(_particleDied, transform.position, transform.rotation);
            Destroy(gameObject);
        }
    }
}
