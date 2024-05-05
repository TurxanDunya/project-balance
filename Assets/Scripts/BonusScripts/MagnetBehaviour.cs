using UnityEngine;

public class MagnetBehaviour : MonoBehaviour
{
    [SerializeField] private float attractDistance;
    [SerializeField] private float attractForce;

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

}
