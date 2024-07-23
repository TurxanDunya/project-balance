using System;
using UnityEngine;

public class CubeFallDetector : MonoBehaviour
{
    public static event Action playableCubeDetect;

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == TagConstants.PLAYABLE_CUBE)
        {
            playableCubeDetect?.Invoke();
        }
    }

}
