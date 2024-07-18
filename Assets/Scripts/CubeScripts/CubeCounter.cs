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

    public int WoodCount { get => woodCount; set => woodCount = value; }
    public int MetalCount { get => metalCount; set => metalCount = value; }
    public int IceCount { get => iceCount; set => iceCount = value; }
    public int RockCount { get => rockCount; set => rockCount = value; }

    private void Awake() {
        DefineCubeCounts();
        OnUpdateCubeCount?.Invoke(WoodCount, MetalCount, IceCount, RockCount);
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
            OnUpdateCubeCount?.Invoke(WoodCount, MetalCount, IceCount, RockCount);
            return cube.cubeMaterialType;
        }
    }

    public void AddCube(CubeData.CubeMaterialType cubeMaterialType)
    {
        totalCubeCount++;
        IncreaseCount(cubeMaterialType);
        OnUpdateCubeCount?.Invoke(WoodCount, MetalCount, IceCount, RockCount);
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

    

    private void DecreaseCount(CubeData.CubeMaterialType type)
    {
        switch(type)
        {
            case CubeData.CubeMaterialType.WOOD:
                WoodCount--;
                break;
            case CubeData.CubeMaterialType.METAL:
                MetalCount--;
                break;
            case CubeData.CubeMaterialType.ICE:
                IceCount--;
                break;
            case CubeData.CubeMaterialType.ROCK:
                RockCount--;
                break;
        }
    }

    private void IncreaseCount(CubeData.CubeMaterialType type)
    {
        switch (type)
        {
            case CubeData.CubeMaterialType.WOOD:
                WoodCount++;
                break;
            case CubeData.CubeMaterialType.METAL:
                MetalCount++;
                break;
            case CubeData.CubeMaterialType.ICE:
                IceCount++;
                break;
            case CubeData.CubeMaterialType.ROCK:
                RockCount++;
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
                WoodCount += cubeCount;
            }

            if (cubeMaterialType == CubeData.CubeMaterialType.METAL)
            {
                MetalCount += cubeCount;
            }

            if (cubeMaterialType == CubeData.CubeMaterialType.ICE)
            {
                IceCount += cubeCount;
            }

            if (cubeMaterialType == CubeData.CubeMaterialType.ROCK)
            {
                RockCount += cubeCount;
            }
        }

        totalCubeCount = WoodCount + MetalCount + IceCount + RockCount;
    }

}

[System.Serializable]
public class CubeCountData
{
    public int cubeCount;
    public CubeData.CubeMaterialType cubeMaterialType;
}
