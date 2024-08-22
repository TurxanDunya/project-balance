using System.Collections;
using UnityEngine;

public class ProjectionIcon : MonoBehaviour
{
    [SerializeField] private CubeRayCast cubeRayCast;
    [SerializeField] private float gapWithPlatform = 0.05f;

    [Header("Animation")]
    [SerializeField] private float scaleFactor = 2f;
    [SerializeField] private float duration = 1.0f;

    private Vector3 initialScale;

    private void Start()
    {
        initialScale = transform.localScale / 2;
        StartCoroutine(ScaleIcon());
    }

    private void Update()
    {
        transform.position = cubeRayCast.GetLineRendererHitPosition();
        transform.position += new Vector3(0, gapWithPlatform, 0);

        transform.rotation = cubeRayCast.GetLineRendererHitRotation() * Quaternion.Euler(90, 0, 0);
    }

    private IEnumerator ScaleIcon()
    {
        while (true)
        {
            yield return StartCoroutine(AnimateScale(initialScale, initialScale * scaleFactor, duration));
            yield return StartCoroutine(AnimateScale(initialScale * scaleFactor, initialScale, duration));
        }
    }

    private IEnumerator AnimateScale(Vector3 from, Vector3 to, float duration)
    {
        float elapsed = 0.0f;

        while (elapsed < duration)
        {
            transform.localScale = Vector3.Lerp(from, to, elapsed / duration);
            elapsed += Time.deltaTime;
            yield return null;
        }

        transform.localScale = to;
    }

}
