using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class CubeSpawnManagementScript : MonoBehaviour
{
    [SerializeField] private InputAction pressed;
    [SerializeField] private GameObject cubePrefab;
    [SerializeField] private Vector3 spawnPosition;
    [SerializeField] private float cubeSpawnDelay = 3.0f;

    CubeMovement cubeMovement;

    void Start()
    {
        SpawnCube();

        cubeMovement = GetComponent<CubeMovement>();

        pressed.Enable();
        pressed.canceled += _ => StartCoroutine(SpawnWithDelay());
    }

    public void SpawnCube()
    {
        if(cubeMovement != null && cubeMovement.enabled)
        {
            return;
        }

        GameObject newCube = Instantiate(cubePrefab, spawnPosition, Quaternion.identity);
        CubeMovement attachedMovementScript = newCube.GetComponent<CubeMovement>();
        if (attachedMovementScript == null)
        {
            attachedMovementScript = newCube.AddComponent<CubeMovement>();
        }

        cubeMovement = attachedMovementScript;
        attachedMovementScript.enabled = true;
    }

    private IEnumerator SpawnWithDelay()
    {
        yield return new WaitForSeconds(cubeSpawnDelay);
        SpawnCube();
    }

}
