using UnityEngine;

[CreateAssetMenu(fileName = "CubeData", menuName = "ScriptableObjects/CubeData")]
public class CubeData : ScriptableObject
{
    public GameObject mesh;
    public float density;
    public CubeMaterialType cubeMaterialType;
    public CubeShape cubeShape;

    public enum CubeShape
    {
        CUBE,
        T_SHAPE,
        ROMB
    }

    public enum CubeMaterialType
    {
        WOOD,
        METAL,
        ICE
    };

}
