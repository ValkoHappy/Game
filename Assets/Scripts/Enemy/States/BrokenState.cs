using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UIElements;

public class BrokenState : EnemyState
{
    [SerializeField] private float fadeTime = 3.0f;
    [SerializeField] private ParticleSystem _particleDied;

    private const string Die = "Die";
    private Material enemyMaterial;

    public event UnityAction Died;

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
        while (elapsedTime < fadeTime)
        {
            float alpha = Mathf.Lerp(1.0f, 0.0f, elapsedTime / fadeTime);
            enemyColor.a = alpha;
            enemyMaterial.color = enemyColor;
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        Instantiate(_particleDied, transform.position, transform.rotation);
        Destroy(gameObject);
    }
}
