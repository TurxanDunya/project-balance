using UnityEngine;

public class DynamicSpawnDestroyAnimation : MonoBehaviour
{
    [Header("Spawn Animation")]
    [SerializeField] private Vector3 startSpawnScale = new Vector3(0.01f, 0.01f, 0.01f);
    [SerializeField] private Vector3 startSpawnRotation = new Vector3(0, 220, 0);
    [SerializeField] private float spawnAnimationDuration = 0.2f;
    private Vector3 endSpawnScale;
    private Vector3 endSpawnRotation;

    [Header("Destroy Animation")]
    [SerializeField] private Vector3 endDestroyScale = new Vector3(0.01f, 0.01f, 0.01f);
    [SerializeField] private Vector3 startDestoryRotation = new Vector3(0, -220, 0);
    [SerializeField] private float destroyAnimationDuration = 0.2f;
  
    void Start()
    {
        endSpawnScale = transform.localScale;
        endSpawnRotation = transform.eulerAngles;
        PlaySpawnAnimation();
    }

    private System.Collections.IEnumerator ScaleAndRotate(Vector3 targetScale, Vector3 targetRotation, float duration, bool destroy = false)
    {
        Vector3 initialScale = transform.localScale;
        Vector3 initialRotation = transform.eulerAngles;
        float time = 0;

        while (time < duration)
        {
            transform.localScale = Vector3.Lerp(initialScale, targetScale, time / duration);
            transform.eulerAngles = Vector3.Lerp(initialRotation, targetRotation, time / duration);

            time += Time.deltaTime;
            yield return null;
        }

        transform.localScale = targetScale;
        transform.eulerAngles = targetRotation;

        if (destroy) Destroy(gameObject);
    }

    public void PlaySpawnAnimation()
    {
        transform.localScale = startSpawnScale;
        StartCoroutine(ScaleAndRotate(endSpawnScale, endSpawnRotation,spawnAnimationDuration));
    }

    public void PlayDestroyAnimation()
    {
        StartCoroutine(ScaleAndRotate(endDestroyScale, startDestoryRotation, destroyAnimationDuration, true));
    }
}
