using System.Collections;
using UnityEngine;

public class FallingShapesController : MonoBehaviour
{
    [SerializeField] private GameObject[] FallingObjects;
    [SerializeField] private GameObject platformObj;
    [SerializeField] private int fallingSecond = 3;
    [SerializeField] private float gapWithPlatformY = 0.2f;

    private Vector3 platformScale;
    private Vector3 fallPosition;

    void Start()
    {
        StartCoroutine(FallShape());
    }

    private IEnumerator FallShape()
    {
        while (true)
        {
            yield return new WaitForSeconds(fallingSecond);
            DefineCoinSpawnPosition();
            GameObject fallingShape = FallingObjects[Random.Range(0, FallingObjects.Length)];
            Instantiate(fallingShape, fallPosition, Quaternion.identity);
        }
    }

    private void DefineCoinSpawnPosition()
    {
        platformScale = platformObj.transform.localScale;

        var fallXDistanceFromCenter = Random.Range(-platformScale.x, platformScale.x);
        var fallZDistanceFromCenter = Random.Range(-platformScale.z, platformScale.z);

        Vector3 platformUpDirection = platformObj.transform.up;
        Vector3 platformSurfacePosition =
            platformObj.transform.position + platformUpDirection * (platformScale.y / 2);

        fallPosition = platformObj.transform.TransformPoint(
            new(fallXDistanceFromCenter, 0, fallZDistanceFromCenter));

        fallPosition = new Vector3(
            fallPosition.x, platformSurfacePosition.y + gapWithPlatformY, fallPosition.z);
    }
}
