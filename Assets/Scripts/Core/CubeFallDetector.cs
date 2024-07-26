using System;
using UnityEngine;

public class CubeFallDetector : MonoBehaviour
{
    public static event Action playableCubeDetect;

    private void OnTriggerEnter(Collider cube)
    {
        if (cube.CompareTag(TagConstants.PLAYABLE_CUBE)
            || cube.CompareTag(TagConstants.DROPPED_CUBE))
        {
            playableCubeDetect?.Invoke();
        }
    }

}
