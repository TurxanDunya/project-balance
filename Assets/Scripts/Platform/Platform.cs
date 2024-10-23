using System;
using UnityEngine;

public class Platform : MonoBehaviour
{
    public static event Action PlayableCubeLanded;
    public static event Action CubeLanded;

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

            if (collision.gameObject.CompareTag(TagConstants.PLAYABLE_CUBE))
            {
                PlayableCubeLanded?.Invoke();
            }
        }
    }
}
