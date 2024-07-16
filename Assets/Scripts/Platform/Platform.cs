using System;
using UnityEngine;

public class Platform : MonoBehaviour
{
    public static event Action CubeLanded;
    public static event Action BombLanded;

    private void OnCollisionEnter(Collision collision)
    {
        foreach (ContactPoint contact in collision.contacts)
        {
            Collider colliderHit = contact.thisCollider;
            if (colliderHit is MeshCollider)
            {
                if (collision.gameObject.CompareTag(Constants.PLAYABLE_CUBE)
                    || collision.gameObject.CompareTag(Constants.MAGNET))
                {
                    CubeLanded?.Invoke();
                    return;
                }

                if (collision.gameObject.CompareTag(Constants.BOMB))
                {
                    CubeLanded?.Invoke();
                    BombLanded?.Invoke();
                }
            }
        }
    }
}
