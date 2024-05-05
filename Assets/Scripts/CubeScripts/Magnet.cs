using UnityEngine;

public class Magnet : MonoBehaviour
{
    public void Release()
    {
        GetComponent<Rigidbody>().useGravity = true;

        Destroy(GetComponent<CubeMovement>());
        Destroy(GetComponent<CubeRayCastScript>());
        Destroy(GetComponent<LineRenderer>());
    }

}
