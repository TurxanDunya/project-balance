using System.Collections;
using UnityEngine;

public class FallingShapesController : MonoBehaviour
{
    [SerializeField] private GameObject[] FallingObjects;
    [SerializeField] private GameObject platformObj;
    [SerializeField] private int fallingSecond = 3;
    [SerializeField] private float gapFromPlatformEdge = 0.1f;

    private Vector3 platformScale;
    private Vector3 fallPosition;

    void Start()
    {
        StartCoroutine(FallShape());
    }

    private void DefineCoinSpawnPosition()
    {
        platformScale = platformObj.transform.localScale;

        var fallXDistanceFromCenter = Random.Range(
            -platformScale.x + gapFromPlatformEdge, platformScale.x  - gapFromPlatformEdge);
        var fallZDistanceFromCenter = Random.Range(
            -platformScale.z  + gapFromPlatformEdge, platformScale.z  - gapFromPlatformEdge);

        Vector3 platformUpDirection = platformObj.transform.up;
        Vector3 platformSurfacePosition =
            platformObj.transform.position + platformUpDirection * (platformScale.y / 2);

        fallPosition = platformObj.transform.TransformPoint(
            new(fallXDistanceFromCenter, 0, fallZDistanceFromCenter));

        fallPosition = new Vector3(
            fallPosition.x, platformSurfacePosition.y + gapFromPlatformEdge, fallPosition.z);
    }

    private IEnumerator FallShape()
    {
        while (true) {
            yield return new WaitForSeconds(fallingSecond);
            DefineCoinSpawnPosition();
            GameObject fallingShape = FallingObjects[Random.Range(0, FallingObjects.Length)];
            Instantiate(fallingShape, fallPosition, Quaternion.identity);
        }
    }
}
