using System;
using UnityEngine;
using Random = UnityEngine.Random;

public class CubeSpawnManagement : MonoBehaviour
{
    private InputManager inputManager;

    public static event Action winGame;

    [SerializeField] private Transform spawnPosition;
    [SerializeField] private CubeCounter cubeCounter;

    [Header("Cubes")]
    [SerializeField] private GameObject[] woodPrefab;
    [SerializeField] private GameObject[] metalPrefab;
    [SerializeField] private GameObject[] icePrefab;

    [Header("Magnets")]
    [SerializeField] private GameObject magnetPrefab;

    [Header("Bombs")]
    [SerializeField] private GameObject bombPrefab;

    private GameObject currentMoveableObject;

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
        CubeData.CubeMaterialType? cubeMaterialType = cubeCounter.GetAvailableCube();
        if (cubeMaterialType == null)
        {
            winGame?.Invoke();
            return;
        }

        GameObject cubePrefab = GetCubePrefabFromPool(cubeMaterialType);
        
        if (cubePrefab != null) {
            currentMoveableObject = Instantiate(cubePrefab, spawnPosition.position, Quaternion.identity);
        }
    }

    public bool ReplaceCubeIfPossible()
    {
        if(currentMoveableObject == null)
        {
            return false;
        }

        CubeData.CubeMaterialType currentCubeMaterialType =
            currentMoveableObject.GetComponent<Cube>().GetCubeMaterialType();
        CubeData.CubeMaterialType? newCubeMaterialType =
            cubeCounter.ChangeAvailableCubeTypeFrom(currentCubeMaterialType);

        if(newCubeMaterialType == null || cubeCounter.GetCubeCount() <= 1)
        {
            return false;
        }

        Destroy(currentMoveableObject);
        GameObject cubePrefab = GetCubePrefabFromPool(newCubeMaterialType);
        if (cubePrefab != null)
        {
            currentMoveableObject = Instantiate(cubePrefab, spawnPosition.position, Quaternion.identity);
        }

        return true;
    }

    public void ReplaceWithMagnet()
    {
        Destroy(currentMoveableObject);
        currentMoveableObject = Instantiate(magnetPrefab, spawnPosition.position, Quaternion.identity);
    }

    public void ReplaceWithBomb()
    {
        Destroy(currentMoveableObject);
        currentMoveableObject = Instantiate(bombPrefab, spawnPosition.position, Quaternion.identity);
    }

    private GameObject GetCubePrefabFromPool(CubeData.CubeMaterialType? cubeMaterialType)
    {
        switch (cubeMaterialType)
        {
            case CubeData.CubeMaterialType.WOOD:
                int randomCubeWood = Random.Range(0, woodPrefab.Length - 1);
                return woodPrefab[randomCubeWood];
            case CubeData.CubeMaterialType.METAL:
                int randomCubeMetal = Random.Range(0, metalPrefab.Length - 1);
                return metalPrefab[randomCubeMetal];
            case CubeData.CubeMaterialType.ICE:
                int randomCubeIce = Random.Range(0, icePrefab.Length - 1);
                return icePrefab[randomCubeIce];
            default:
                throw new Exception("No such type cube material defined!");
        }
    }

    private void ReleaseObject()
    {
        if (currentMoveableObject == null)
        {
            return;
        }

        Cube cube = currentMoveableObject.GetComponent<Cube>();
        if (cube != null)
        {
            cube.Release();
        }

        Magnet magnet = currentMoveableObject.GetComponent<Magnet>();
        if (magnet != null)
        {
            magnet.Release();
        }

        Bomb bomb = currentMoveableObject.GetComponent<Bomb>();
        if (bomb != null)
        {
            bomb.Release();
        }

        currentMoveableObject = null;
    }

}
