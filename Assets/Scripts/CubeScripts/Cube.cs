using UnityEngine;

public class Cube : MonoBehaviour
{
    [SerializeField] private ParticleSystem rippleEffect;
    [SerializeField] private CubeData.CubeMaterialType cubeMaterialType;

    public void Release()
    {
        GetComponent<Rigidbody>().useGravity = true;

        Destroy(GetComponent<CubeMovement>());
        Destroy(GetComponent<CubeRayCast>());
        Destroy(GetComponent<LineRenderer>());

        Destroy(GameObject.Find("ProjectionIcon"));
    }

    public CubeData.CubeMaterialType GetCubeMaterialType()
    {
        return cubeMaterialType;
    }

    private void OnEnable()
    {
        Platform.CubeLanded += CubeLanded;
    }

    private void OnDisable()
    {
        Platform.CubeLanded -= CubeLanded;
    }

    private void CubeLanded() {
        rippleEffect.Play();
        Platform.CubeLanded -= CubeLanded;
    }
    
}
