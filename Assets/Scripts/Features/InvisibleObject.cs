using System.Collections;
using UnityEngine;

public class InvisibleObject : MonoBehaviour
{
    private static int WORKED_FOR_CUBE_COUNT = 0;

    [SerializeField] private float fadeDuration = 1f;
    [SerializeField] private int workIntervalForCube = 3;

    private void OnEnable()
    {
        Platform.CubeLanded += MakeRandomDroppedCubeInvisible;
    }

    private void OnDisable()
    {
        Platform.CubeLanded -= MakeRandomDroppedCubeInvisible;

        StopAllCoroutines();
    }

    public void MakeRandomDroppedCubeInvisible()
    {
        GameObject droppedCube =
            GameObject.FindGameObjectWithTag(TagConstants.DROPPED_CUBE);

        WORKED_FOR_CUBE_COUNT++;
        if (WORKED_FOR_CUBE_COUNT % workIntervalForCube == 0)
        {
            MakeObjectInvisible(droppedCube);
        }
    }

    public void MakeObjectInvisible(GameObject gameObject)
    {
        StartCoroutine(FadeOut(gameObject));
    }

    private IEnumerator FadeOut(GameObject gameObject)
    {
        Renderer renderer = gameObject.GetComponent<Renderer>();
        if (renderer == null)
        {
            yield break;
        }

        Material material = renderer.material;
        Color originalColor = material.color;

        float elapsedTime = 0f;

        while (elapsedTime < fadeDuration)
        {
            elapsedTime += Time.deltaTime;
            float alpha = Mathf.Lerp(1f, 0f, elapsedTime / fadeDuration);
            material.color = new Color(originalColor.r, originalColor.g, originalColor.b, alpha);
            yield return null;
        }

        renderer.enabled = false;
    }

}
