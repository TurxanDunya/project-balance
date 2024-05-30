using System.Collections;
using UnityEngine;

public class ProjectionIcon : MonoBehaviour
{
    [SerializeField] private CubeRayCast cubeRayCast;
    [SerializeField] private float gapWithPlatform = 0.01f;

    [Header("Animation")]
    [SerializeField] private float scaleFactor = 1.5f;
    [SerializeField] private float duration = 1.0f;

    private GameObject platform;

    public Vector3 initialScale = new(0.03f, 0.03f, 0.03f);

    private void Start()
    {
        StartCoroutine(ScaleIcon());
    }

    private void Update()
    {
        if (platform == null)
        {
            platform = GameObject.FindWithTag(Constants.MAIN_PLATFORM);
        }
        else if(platform != null && cubeRayCast != null)
        {
            transform.position = cubeRayCast.GetLineRendererHitPosition();
            transform.position += new Vector3(0, gapWithPlatform, 0);

            transform.rotation = platform.transform.rotation;
            transform.eulerAngles += new Vector3(90, 0, 0);
        }
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
