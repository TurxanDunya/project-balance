using System;
using UnityEngine;

public class CubeFallDetector : MonoBehaviour
{
    public static event Action playableCubeDetect;

    private void OnTriggerEnter(Collider collider)
    {
        if (collider.CompareTag(TagConstants.PLAYABLE_CUBE)
            || collider.CompareTag(TagConstants.DROPPED_CUBE))
        {
            playableCubeDetect?.Invoke();
            Destroy(collider.gameObject);
            return;
        }

        Destroy(collider.gameObject);
    }

}
