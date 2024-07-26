using UnityEngine;

public class CoinRotation : MonoBehaviour
{
    private Vector3 currentRotation;

    private void Start()
    {
        transform.rotation = Quaternion.Euler(0, 0, 90);
        currentRotation = transform.rotation.eulerAngles;
    }

    private void FixedUpdate()
    {
        currentRotation.y += 1.0f;
        transform.rotation = Quaternion.Euler(currentRotation);
    }

}
