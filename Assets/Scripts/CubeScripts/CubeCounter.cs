using System.Collections.Generic;
using UnityEngine;

public class CubeCounter : MonoBehaviour
{
    [SerializeField] List<CubeCountDataByMaterialType> cubeCountDataByMaterialTypeList;

    public delegate void UpdateCubeCountEvent(int woodCount, int metalCount, int iceCount, int rockCount);
    public event UpdateCubeCountEvent OnUpdateCubeCount;

    public delegate void CanReplaceCubeEvent(bool canReplace);
    public event CanReplaceCubeEvent OnCanReplaceCubeEvent;

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
    }

    private void Start()
    {
        OnUpdateCubeCount?.Invoke(WoodCount, MetalCount, IceCount, RockCount);
        OnCanReplaceCubeEvent?.Invoke(IsCubeExistOnDifferentTypes());
    }

    public CubeData.CubeMaterialType? GetAvailableCube()
    {
        if (totalCubeCount <= 0)
        {
            return null;
        }

        int index = Random.Range(0, cubeCountDataByMaterialTypeList.Count);
        CubeCountDataByMaterialType cubeCountByType = cubeCountDataByMaterialTypeList[index];
        if (cubeCountByType.cubeCount <= 0)
        {
            cubeCountDataByMaterialTypeList.Remove(cubeCountByType);
            return GetAvailableCube();
        }
        else
        {
            totalCubeCount--;
            DecreaseCount(cubeCountByType.cubeMaterialType);
            cubeCountDataByMaterialTypeList[index].cubeCount--;
            OnUpdateCubeCount?.Invoke(WoodCount, MetalCount, IceCount, RockCount);
            OnCanReplaceCubeEvent?.Invoke(IsCubeExistOnDifferentTypes());
            return cubeCountByType.cubeMaterialType;
        }
    }

    public void AddCube(CubeData.CubeMaterialType cubeMaterialType)
    {
        totalCubeCount++;
        IncreaseCount(cubeMaterialType);

        foreach (CubeCountDataByMaterialType element in cubeCountDataByMaterialTypeList)
        {
            if (element.cubeMaterialType == cubeMaterialType)
            {
                element.cubeCount++;
                break;
            }
        }

        OnUpdateCubeCount?.Invoke(WoodCount, MetalCount, IceCount, RockCount);
    }

    public CubeData.CubeMaterialType? ChangeAvailableCubeTypeFrom(CubeData.CubeMaterialType cubeMaterialType)
    {
        if (totalCubeCount <= 1 || !IsCubeExistOnDifferentTypes())
        {
            return null;
        }

        int index = Random.Range(0, cubeCountDataByMaterialTypeList.Count);
        CubeCountDataByMaterialType cube = cubeCountDataByMaterialTypeList[index];
        if (cube.cubeCount <= 0)
        {
            return ChangeAvailableCubeTypeFrom(cubeMaterialType);
        }

        return cube.cubeMaterialType;
    }

    public bool IsCubeExistOnDifferentTypes()
    {
        int differentCubeTypes = 0;
        if (woodCount > 0)
        {
            differentCubeTypes++;
        }

        if (metalCount > 0)
        {
            differentCubeTypes++;
        }

        if (iceCount > 0)
        {
            differentCubeTypes++;
        }

        if (rockCount > 0)
        {
            differentCubeTypes++;
        }

        return differentCubeTypes >= 2;
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
        foreach (var cube in cubeCountDataByMaterialTypeList)
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
public class CubeCountDataByMaterialType
{
    public int cubeCount;
    public CubeData.CubeMaterialType cubeMaterialType;
}
