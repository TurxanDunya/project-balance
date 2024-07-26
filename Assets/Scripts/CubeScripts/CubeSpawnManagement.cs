using System;
using UnityEngine;
using Random = UnityEngine.Random;

public class CubeSpawnManagement : MonoBehaviour
{
    private static readonly ILogger logger = Debug.unityLogger;

    private InputManager inputManager;

    public static event Action winGame;

    [SerializeField] private Transform spawnPosition;
    [SerializeField] private CubeCounter cubeCounter;

    [Header("Cubes")]
    [SerializeField] private GameObject[] woodPrefab;
    [SerializeField] private GameObject[] metalPrefab;
    [SerializeField] private GameObject[] icePrefab;
    [SerializeField] private GameObject[] rockPrefab;

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
        if (currentMoveableObject)
        {
            logger.Log(LogType.Warning, "There is already moveable cube, so need to create new one!");
            return;
        }

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

    public void ReplaceCube()
    {
        CubeData.CubeMaterialType currentCubeMaterialType =
            currentMoveableObject.GetComponent<Cube>().GetCubeMaterialType();
        CubeData.CubeMaterialType? newCubeMaterialType =
            cubeCounter.ChangeAvailableCubeTypeFrom(currentCubeMaterialType);

        Destroy(currentMoveableObject);
        GameObject cubePrefab = GetCubePrefabFromPool(newCubeMaterialType);
        if (cubePrefab != null)
        {
            currentMoveableObject = Instantiate(cubePrefab, spawnPosition.position, Quaternion.identity);
        }
    }

    public void ReplaceWithMagnet()
    {
        Destroy(currentMoveableObject);

        cubeCounter.AddCube(currentMoveableObject.GetComponent<Cube>().GetCubeMaterialType());

        GameObject magnet = GetCubePrefabFromPool(CubeData.CubeMaterialType.MAGNET);
        currentMoveableObject = Instantiate(magnet, spawnPosition.position, Quaternion.identity);
    }

    public void ReplaceWithBomb()
    {
        Destroy(currentMoveableObject);

        cubeCounter.AddCube(currentMoveableObject.GetComponent<Cube>().GetCubeMaterialType());

        GameObject bomb = GetCubePrefabFromPool(CubeData.CubeMaterialType.BOMB);
        currentMoveableObject = Instantiate(bomb, spawnPosition.position, Quaternion.identity);
    }

    private GameObject GetCubePrefabFromPool(CubeData.CubeMaterialType? cubeMaterialType)
    {
        switch (cubeMaterialType)
        {
            case CubeData.CubeMaterialType.WOOD:
                int randomCubeWood = Random.Range(0, woodPrefab.Length);
                return woodPrefab[randomCubeWood];
            case CubeData.CubeMaterialType.METAL:
                int randomCubeMetal = Random.Range(0, metalPrefab.Length);
                return metalPrefab[randomCubeMetal];
            case CubeData.CubeMaterialType.ICE:
                int randomCubeIce = Random.Range(0, icePrefab.Length);
                return icePrefab[randomCubeIce];
            case CubeData.CubeMaterialType.ROCK:
                int randomCubeRock = Random.Range(0, rockPrefab.Length);
                return rockPrefab[randomCubeRock];
            case CubeData.CubeMaterialType.MAGNET:
                return magnetPrefab;
            case CubeData.CubeMaterialType.BOMB:
                return bombPrefab;
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
