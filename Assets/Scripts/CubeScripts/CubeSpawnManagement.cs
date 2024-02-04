using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class CubeSpawnManagementScript : MonoBehaviour
{
    [SerializeField] private InputAction pressed;
    [SerializeField] private List<GameObject> cubePrefabs;
    [SerializeField] private Transform spawnPosition;

    [SerializeField] CubeCounter CubeCounter;

    [Header("Cubes")]
    [SerializeField] private GameObject woodPrefab;
    [SerializeField] private GameObject metalPrefab;
    [SerializeField] private GameObject icePrefab;

    GameObject currentMoveableCube;

    void Start()
    {
        pressed.Enable();
        SpawnCube();

        pressed.canceled += _ => { ReleaseObject(); };
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
        CubeData.CubeMaterialType? cubeMaterialType = CubeCounter.getAvailableCube();
        if (cubeMaterialType == null)
        {
            return; // TODO: Win pop up will be shown
        }

        GameObject cubePrefab = null;
        switch (cubeMaterialType)
        {
            case CubeData.CubeMaterialType.WOOD:
                cubePrefab = woodPrefab;
                break;
            case CubeData.CubeMaterialType.METAL:
                cubePrefab = metalPrefab;
                break;
            case CubeData.CubeMaterialType.ICE:
                cubePrefab = icePrefab;
                break;
            default:
                break;
        }

        if (cubePrefab != null) {
            currentMoveableCube = Instantiate(cubePrefab, spawnPosition.position, Quaternion.identity);
        }
        
    }

    private void ReleaseObject()
    {
        if (currentMoveableCube != null) {
            currentMoveableCube.GetComponent<Cube>().CubeReleased();
            currentMoveableCube = null;
        }
       
    }

}
