using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using Random = UnityEngine.Random;

public class CubeSpawnManagementScript : MonoBehaviour
{
    public static event Action winGame;

    [SerializeField] private InputAction pressed;
    [SerializeField] private List<GameObject> cubePrefabs;
    [SerializeField] private Transform spawnPosition;

    [SerializeField] CubeCounter CubeCounter;

    [Header("Cubes")]
    [SerializeField] private GameObject[] woodPrefab;
    [SerializeField] private GameObject[] metalPrefab;
    [SerializeField] private GameObject[] icePrefab;

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
            winGame?.Invoke();
        }

        GameObject cubePrefab = null;
        switch (cubeMaterialType)
        {
            case CubeData.CubeMaterialType.WOOD:
                int randomCubeWood = Random.Range(0, woodPrefab.Length - 1);
                cubePrefab = woodPrefab[randomCubeWood];
                break;
            case CubeData.CubeMaterialType.METAL:
                int randomCubeMetal = Random.Range(0, metalPrefab.Length - 1);
                cubePrefab = metalPrefab[randomCubeMetal];
                break;
            case CubeData.CubeMaterialType.ICE:
                int randomCubeIce = Random.Range(0, icePrefab.Length - 1);
                cubePrefab = icePrefab[randomCubeIce];
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
