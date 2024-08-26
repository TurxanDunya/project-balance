using System;
using UnityEngine;

public class Platform : MonoBehaviour
{
    public static event Action CubeLanded;
    public static event Action BombLanded;

    public static void CallCubeLandedEvent()
    {
        CubeLanded?.Invoke();
    }

    private void OnCollisionEnter(Collision collision)
    {
        foreach (ContactPoint contact in collision.contacts)
        {
            Collider colliderHit = contact.thisCollider;
            if (colliderHit is MeshCollider)
            {
                if (collision.gameObject.CompareTag(TagConstants.PLAYABLE_CUBE)
                    || collision.gameObject.CompareTag(TagConstants.DROPPED_CUBE)
                    || collision.gameObject.CompareTag(TagConstants.MAGNET))
                {
                    
                    CubeLanded?.Invoke();
                    return;
                }

                if (collision.gameObject.CompareTag(TagConstants.BOMB))
                {
                    CubeLanded?.Invoke();
                    BombLanded?.Invoke();
                    return;
                }
            }
        }
    }
}
