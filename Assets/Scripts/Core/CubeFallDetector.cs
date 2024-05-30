using System;
using UnityEngine;

public class CubeFallDetector : MonoBehaviour
{
    public static event Action playableCubeDetect;

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == Constants.PLAYABLE_CUBE)
        {
            playableCubeDetect?.Invoke();
        }
    }

}
