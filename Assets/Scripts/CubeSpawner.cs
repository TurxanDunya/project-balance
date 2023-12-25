using UnityEngine;

[CreateAssetMenu(fileName = "CubeSpawner", menuName = "ScriptableObjects/CubeSpawner", order = 1)]
public class CubeSpawner : ScriptableObject
{
    [SerializeField] private GameObject cubePrefab;
    [SerializeField] private Vector3 spawnPosition;

    public void SpawnCube()
    {
        GameObject newCube = Instantiate(cubePrefab, spawnPosition, Quaternion.identity);

        CubeMovement attachedMovementScript = newCube.GetComponent<CubeMovement>();
        if (attachedMovementScript == null)
        {
            attachedMovementScript = newCube.AddComponent<CubeMovement>();
        }

        attachedMovementScript.enabled = true;
    }
}
