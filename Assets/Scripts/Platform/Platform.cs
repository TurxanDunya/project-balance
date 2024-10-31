using System;
using UnityEngine;
using Random = UnityEngine.Random;

public class Platform : MonoBehaviour
{
    public static event Action CubeLanded;

    // DefineSpawnablePosition variables
    private Vector3 spawnPosition;
    private float xDistanceFromCenter;
    private float zDistanceFromCenter;

    public static void CallCubeLandedEvent()
    {
        CubeLanded?.Invoke();
    }

    private void OnCollisionEnter(Collision collision)
    {
        Collider collider = collision.collider;
        if (collider is MeshCollider || collider is BoxCollider)
        {
            if (collision.gameObject.CompareTag(TagConstants.PLAYABLE_CUBE)
                || collision.gameObject.CompareTag(TagConstants.DROPPED_CUBE)
                || collision.gameObject.CompareTag(TagConstants.MAGNET))
            {
                CubeLanded?.Invoke();
            }
        }
    }

    /*
     * TODO: some validations should be add for this method
     * Because if gapFromPlatformEdge and gapFromCenter gets invalid parameters
     * it will be work as unexpected
     */
    public Vector3 DefineSpawnablePosition(
        float gapWithPlatformY, float gapFromPlatformEdge, float gapFromCenter)
    {
        ResetPositions();

        Vector3 platformScale = transform.localScale;

        xDistanceFromCenter = Random.Range(
            -platformScale.x / 2 + gapFromPlatformEdge - gapFromCenter,
            platformScale.x / 2 - gapFromPlatformEdge + gapFromCenter);
        zDistanceFromCenter = Random.Range(
            -platformScale.z / 2 + gapFromPlatformEdge - gapFromCenter,
            platformScale.z / 2 - gapFromPlatformEdge + gapFromCenter);

        Vector3 platformUpDirection = transform.up;
        Vector3 platformSurfacePosition =
            transform.position + platformUpDirection * (platformScale.y / 2);

        spawnPosition = transform.TransformPoint(new(xDistanceFromCenter, 0, zDistanceFromCenter));

        spawnPosition = new Vector3(
            spawnPosition.x, platformSurfacePosition.y + gapWithPlatformY, spawnPosition.z);

        return spawnPosition;
    }

    private void ResetPositions()
    {
        xDistanceFromCenter = 0;
        zDistanceFromCenter = 0;
    }

    public bool IsPositionInPlatformArea(Vector3 position)
    {
        Vector3 platformScale = transform.localScale;
        Vector3 platformCenter = transform.position;

        return position.x > platformCenter.x - platformScale.x / 2 &&
               position.x < platformCenter.x + platformScale.x / 2 &&
               position.z > platformCenter.z - platformScale.z / 2 &&
               position.z < platformCenter.z + platformScale.z / 2;
    }

}
