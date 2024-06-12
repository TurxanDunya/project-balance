using System.Collections.Generic;
using UnityEngine;

public class CubeCounter : MonoBehaviour
{
    [SerializeField] List<CubeCountData> cubes;

    public delegate void UpdateCubeCountEvent(int woodCount, int metalCount, int iceCount, int rockCount);
    public event UpdateCubeCountEvent OnUpdateCubeCount;

    private int totalCubeCount;

    private int woodCount;
    private int metalCount;
    private int iceCount;
    private int rockCount;

    private void Awake() {
        DefineCubeCounts();
        OnUpdateCubeCount?.Invoke(woodCount, metalCount, iceCount, rockCount);
    }

    public CubeData.CubeMaterialType? GetAvailableCube()
    {
        if (totalCubeCount <= 0)
        {
            return null;
        }

        int index = Random.Range(0, cubes.Count);
        CubeCountData cube = cubes[index];
        if (cube.cubeCount <= 0)
        {
            return GetAvailableCube();
        }
        else
        {
            totalCubeCount--;
            DecreaseCount(cube.cubeMaterialType);
            OnUpdateCubeCount?.Invoke(woodCount, metalCount, iceCount, rockCount);
            return cube.cubeMaterialType;
        }
    }

    public CubeData.CubeMaterialType? ChangeAvailableCubeTypeFrom(CubeData.CubeMaterialType cubeMaterialType)
    {
        if (totalCubeCount <= 0)
        {
            return null;
        }

        int index = Random.Range(0, cubes.Count);
        CubeCountData cube = cubes[index];
        if (cube.cubeMaterialType == cubeMaterialType)
        {
            ChangeAvailableCubeTypeFrom(cubeMaterialType);
        }

        return cube.cubeMaterialType;
    }

    public int GetCubeCount()
    {
        return cubes.Count;
    }

    private void DecreaseCount(CubeData.CubeMaterialType type)
    {
        switch(type)
        {
            case CubeData.CubeMaterialType.WOOD:
                woodCount--;
                break;
            case CubeData.CubeMaterialType.METAL:
                metalCount--;
                break;
            case CubeData.CubeMaterialType.ICE:
                iceCount--;
                break;
            case CubeData.CubeMaterialType.ROCK:
                rockCount--;
                break;
        }
    }

    private void DefineCubeCounts()
    {
        foreach (var cube in cubes)
        {
            CubeData.CubeMaterialType cubeMaterialType = cube.cubeMaterialType;
            int cubeCount = cube.cubeCount;
            if (cubeMaterialType == CubeData.CubeMaterialType.WOOD)
            {
                woodCount += cubeCount;
            }

            if (cubeMaterialType == CubeData.CubeMaterialType.METAL)
            {
                metalCount += cubeCount;
            }

            if (cubeMaterialType == CubeData.CubeMaterialType.ICE)
            {
                iceCount += cubeCount;
            }
            if (cubeMaterialType == CubeData.CubeMaterialType.ROCK)
            {
                rockCount += cubeCount;
            }
        }

        totalCubeCount = woodCount + metalCount + iceCount + rockCount;
        Debug.Log(totalCubeCount);
    }

}

[System.Serializable]
public class CubeCountData
{
    public int cubeCount;
    public CubeData.CubeMaterialType cubeMaterialType;
}
