using System;
using UnityEngine;

public class CubeFallDetector : MonoBehaviour
{
    public static event Action playableCubeDetect;
    public static event Action magnetDetect;

    private void OnTriggerEnter(Collider cube)
    {
        if (cube.CompareTag(TagConstants.PLAYABLE_CUBE)
            || cube.CompareTag(TagConstants.DROPPED_CUBE))
        {
            playableCubeDetect?.Invoke();
            return;
        }

        if (cube.CompareTag(TagConstants.MAGNET))
        {
            magnetDetect?.Invoke();
        }
    }

}
