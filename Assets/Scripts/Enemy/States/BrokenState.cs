using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class BrokenState : EnemyState
{
    [SerializeField] private float fadeTime = 3.0f;

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
        Vector3 oppositeDirection = -impactDirection;

        Animator.SetTrigger(Die);
        Rigidbody.AddForce(oppositeDirection * force, ForceMode.Impulse);
        StartCoroutine(FadeOut());
        Died?.Invoke();
    }

    private IEnumerator FadeOut()
    {
        Color enemyColor = enemyMaterial.color;
        float elapsedTime = 0.0f;

        while (elapsedTime < fadeTime)
        {
            float alpha = Mathf.Lerp(1.0f, 0.0f, elapsedTime / fadeTime);
            enemyColor.a = alpha;
            enemyMaterial.color = enemyColor;
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        Destroy(gameObject);
    }
}
