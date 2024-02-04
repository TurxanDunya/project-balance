using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class CubeSpawnManagementScript : MonoBehaviour
{
    [SerializeField] private InputAction pressed;
    [SerializeField] private GameObject cubePrefab;
    [SerializeField] private Transform spawnPosition;
    [SerializeField] private float cubeSpawnDelay = 3.0f;

    GameObject currentMoveableCube;

    void Start()
    {
        pressed.Enable();
        SpawnCube();

        pressed.canceled += _ =>
        {
            ReleaseObject();
        };
    }

    private void OnEnable()
    {
        Platform.CubeLanded += SpawnCube;
    }

    private void OnDisable()
    {
        Platform.CubeLanded -= SpawnCube;
    }

    public void SpawnCube()
    {
        currentMoveableCube = Instantiate(cubePrefab, spawnPosition.position, Quaternion.identity);
    }

    private void ReleaseObject()
    {
        currentMoveableCube.GetComponent<Cube>().CubeReleased();
        currentMoveableCube = null;
    }

}
