using UnityEngine;

public class Magnet : MonoBehaviour
{
    [SerializeField] private float attractDistance;
    [SerializeField] private float attractForce;
    [SerializeField] private ParticleSystem[] magnetEffect;

    private void FixedUpdate()
    {
        GameObject[] attractibleObjects = GameObject.FindGameObjectsWithTag(Constants.PLAYABLE_CUBE);

        foreach (GameObject attractibleObject in attractibleObjects)
        {
            Rigidbody rb = attractibleObject.GetComponent<Rigidbody>();

            Vector3 magnetPosition = transform.position;
            Vector3 otherObjectPosition = attractibleObject.transform.position;
            float distanceBetween = Vector3.Distance(magnetPosition, otherObjectPosition);

            if (distanceBetween <= attractDistance)
            {
                Vector3 direction = (magnetPosition - otherObjectPosition).normalized;
                rb.AddForce(attractForce * Time.deltaTime * direction, ForceMode.Acceleration);
            }
        }
    }

    public void Release()
    {
        GetComponent<Rigidbody>().useGravity = true;

        Destroy(GetComponent<CubeMovement>());
        Destroy(GetComponent<CubeRayCast>());
        Destroy(GetComponent<LineRenderer>());

        Destroy(GameObject.Find("ProjectionIcon"));
    }

    private void OnCollisionEnter(Collision collision)
    {
        foreach (var effect in magnetEffect) {
            effect.Play();
        }
    }

}
