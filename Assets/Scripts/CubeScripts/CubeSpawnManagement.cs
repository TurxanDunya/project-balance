using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class CubeSpawnManagement : MonoBehaviour
{
    private InputManager inputManager;

    public static event Action winGame;

    [SerializeField] private List<GameObject> cubePrefabs;
    [SerializeField] private Transform spawnPosition;

    [SerializeField] private CubeCounter cubeCounter;

    [Header("Cubes")]
    [SerializeField] private GameObject[] woodPrefab;
    [SerializeField] private GameObject[] metalPrefab;
    [SerializeField] private GameObject[] icePrefab;

    GameObject currentMoveableCube;

    private void Awake()
    {
        inputManager = gameObject.AddComponent<InputManager>();
    }

    void Start()
    {
        SpawnCube();
    }

    private void OnEnable()
    {
        Platform.CubeLanded += SpawnCube;
        inputManager.OnEndTouch += ReleaseObject;
    }

    private void OnDisable()
    {
        Platform.CubeLanded -= SpawnCube;
        inputManager.OnEndTouch -= ReleaseObject;
    }

    public void SpawnCube()
    {
        CubeData.CubeMaterialType? cubeMaterialType = cubeCounter.getAvailableCube();
        if (cubeMaterialType == null)
        {
            Debug.Log("CubeSpawnManagement WIN");
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
