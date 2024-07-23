using UnityEngine;

public class Cube : MonoBehaviour
{
    [SerializeField] private ParticleSystem rippleEffect;
    [SerializeField] private CubeData.CubeMaterialType cubeMaterialType;
    [SerializeField] private AudioSource fallSFXPlayer;
  
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
        if(rippleEffect != null)
        {
            ParticleSystem ripple = Instantiate(rippleEffect, transform.position, Quaternion.Euler(90, 0, 0));
            ripple.Play();
        };
        fallSFXPlayer.Play();

        tag = TagConstants.DROPPED_CUBE;
        Platform.CubeLanded -= CubeLanded;
    }

}
