using UnityEngine;

public class Cube : MonoBehaviour
{
    [SerializeField] private ParticleSystem rippleEffect;
    [SerializeField] private CubeData.CubeMaterialType cubeMaterialType;

    private SoundSystem soundSystem;

    private void Start()
    {
        soundSystem = GetComponent<SoundSystem>();
    }

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
        Platform.CubeLanded -= CubeLanded;
    }

    private void OnCollisionEnter(Collision collision)
    {
        Collider collider = collision.collider;
        if (collider.CompareTag(TagConstants.DROPPED_CUBE)
            || collider.CompareTag(TagConstants.PLAYABLE_CUBE))
        {
            Platform.CallCubeLandedEvent();
            return;
        }

        if (collider.CompareTag(TagConstants.MAIN_PLATFORM))
        {
            PlayParticleEffect();
        }

        soundSystem.MakeSound(collision);
        tag = TagConstants.DROPPED_CUBE;
    }

    private void PlayParticleEffect()
    {
        if (rippleEffect != null)
        {
            ParticleSystem ripple = Instantiate(
                rippleEffect, transform.position, Quaternion.Euler(90, 0, 0));
            ripple.Play();
        };
    }

}
