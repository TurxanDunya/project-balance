using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CubeCounter : MonoBehaviour
{
    [SerializeField] List<CubeCountData> cubes;

    private int totalCubeCount;

    private int woodCount;
    private int metalCount;
    private int iceCount;

    private void Awake() {
        defineCubeCounts();
    }

    public CubeData.CubeMaterialType? getAvailableCube()
    {
        if (totalCubeCount <= 0)
        {
            return null;
        }

        int index = Random.Range(0, cubes.Count);
        CubeCountData cube = cubes[index];
        if (cube.cubeCount <= 0)
        {
            return getAvailableCube();
        }
        else
        {
            totalCubeCount--;
            decreaseCount(cube.cubeMaterialType);
            return cube.cubeMaterialType;
        }
    }

    private void decreaseCount(CubeData.CubeMaterialType type)
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
        }
    }

    private void defineCubeCounts()
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
        }

        totalCubeCount = woodCount + metalCount + iceCount;
    }

}

[System.Serializable]
public class CubeCountData
{
    public int cubeCount;
    public CubeData.CubeMaterialType cubeMaterialType;
}
