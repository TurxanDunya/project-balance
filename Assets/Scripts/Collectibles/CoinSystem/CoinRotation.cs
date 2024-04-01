using UnityEngine;

public class CoinRotation : MonoBehaviour
{

    private void FixedUpdate()
    {
        Vector3 currentRotation = transform.rotation.eulerAngles;
        currentRotation.y += 1.0f;
        transform.rotation = Quaternion.Euler(currentRotation);
    }

}
