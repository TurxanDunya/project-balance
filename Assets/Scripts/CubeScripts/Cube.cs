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
        if(rippleEffect != null)
        {
            ParticleSystem ripple = Instantiate(rippleEffect, transform.position, Quaternion.Euler(90, 0, 0));
            ripple.Play();
        };

        if(CompareTag(TagConstants.PLAYABLE_CUBE))
        {
            tag = TagConstants.DROPPED_CUBE;
        }

        Platform.CubeLanded -= CubeLanded;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.CompareTag(TagConstants.DROPPED_CUBE)
            || collision.collider.CompareTag(TagConstants.PLAYABLE_CUBE))
        {
            Platform.CallCubeLandedEvent();
        }
    }

}
