using System;
using UnityEngine;

public class CubeFallDetector : MonoBehaviour
{
    /*
    public delegate void PlayableCubeDetect();
    public static event PlayableCubeDetect playableCubeDetect;
    */
    public static event Action playableCubeDetect;

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == Constants.PLAYABLE_CUBE)
        {
            playableCubeDetect?.Invoke();
        }
    }

}
