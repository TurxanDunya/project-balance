using System.Collections;
using UnityEngine;

public class Cube : MonoBehaviour
{
    [SerializeField] private ParticleSystem rippleEffect;
    [SerializeField] private CubeData.CubeMaterialType cubeMaterialType;

    private void Start()
    {
        GetComponent<Rigidbody>().isKinematic = true;
    }

    private void OnDisable()
    {
        DestroyAllInstantiated();
    }

    public void Release()
    {
        GetComponent<Rigidbody>().isKinematic = false;
        GetComponent<Rigidbody>().useGravity = true;

        RandomFallSpeed randonFallSpeed = GetComponent<RandomFallSpeed>();
        if (randonFallSpeed != null && gameObject.activeInHierarchy)
        {
            randonFallSpeed.RestoreDefaultSpeed();
        }

        Destroy(GetComponent<CubeMovement>());
        Destroy(GetComponent<CubeRayCast>());
        Destroy(GetComponent<LineRenderer>());

        Destroy(GameObject.Find("ProjectionIcon"));
    }

    public CubeData.CubeMaterialType GetCubeMaterialType()
    {
        return cubeMaterialType;
    }

    private void OnCollisionEnter(Collision collision)
    {
        Collider collider = collision.collider;
        if (collider.CompareTag(TagConstants.MAIN_PLATFORM)
            || collider.CompareTag(TagConstants.MAIN_PLATFORM_COLLIDER))
        {
            PlayParticleEffect();
        }

        tag = TagConstants.DROPPED_CUBE;
    }

    ParticleSystem ripple = null;
    private void PlayParticleEffect()
    {
        if (rippleEffect != null)
        {
            ripple = Instantiate(
                rippleEffect, transform.position, Quaternion.Euler(90, 0, 0));
            ripple.Play();
            rippleEffect = null;
            StartCoroutine(DestroyRippleEffect());
        };
    }

    private IEnumerator DestroyRippleEffect()
    {
        while(ripple.isPlaying)
        {
            yield return new WaitForSeconds(0.5f);
        }

        Destroy(ripple);
    }

    private void DestroyAllInstantiated()
    {
        GameObject[] playableCubeObjects =
            GameObject.FindGameObjectsWithTag(TagConstants.PLAYABLE_CUBE);
        foreach (GameObject playableCubeObject in playableCubeObjects)
        {
            Destroy(playableCubeObject);
        }

        GameObject[] droppedCubeObjects =
            GameObject.FindGameObjectsWithTag(TagConstants.DROPPED_CUBE);
        foreach (GameObject droppedCubeObject in droppedCubeObjects)
        {
            Destroy(droppedCubeObject);
        }
    }

}
