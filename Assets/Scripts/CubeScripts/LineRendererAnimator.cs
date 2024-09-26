using UnityEngine;

public class LineRendererAnimator : MonoBehaviour
{
    [SerializeField] private float animationDuration = 0.3f;

    private LineRenderer lineRenderer;
    private Gradient gradient;

    private GradientColorKey[] colorKeys;

    private void Start()
    {
        lineRenderer = GetComponent<LineRenderer>();
        gradient = new Gradient();

        SetGradient();
    }

    private void SetGradient()
    {
        colorKeys = new GradientColorKey[3];
        colorKeys[0] = new GradientColorKey(GetRandomColor(), 0);
        colorKeys[1] = new GradientColorKey(GetRandomColor(), 1);
        colorKeys[2] = new GradientColorKey(GetRandomColor(), 2);

        gradient.colorKeys = colorKeys;
        lineRenderer.colorGradient = gradient;
    }

    private Color GetRandomColor()
    {
        return new Color(Random.value, Random.value, Random.value);
    }

}
