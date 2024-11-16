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
        }else if(collider.CompareTag(TagConstants.MAGNET)
            || collider.CompareTag(TagConstants.BOMB)){
            CubeSpawnManagement cubeSpawnManagement = FindAnyObjectByType<CubeSpawnManagement>();
            cubeSpawnManagement.SpawnCube();
            Destroy(collider.gameObject);
            return;
        }

        Destroy(collider.gameObject);
    }

}
