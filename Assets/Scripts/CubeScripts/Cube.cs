using UnityEngine;

public class Cube : MonoBehaviour
{
    public void CubeReleased() {
        GetComponent<Rigidbody>().useGravity = true;

        CubeMovement cubeMovement = GetComponent<CubeMovement>();
        cubeMovement.pressed.Disable();
        cubeMovement.axis.Disable();

        Destroy(GetComponent<CubeMovement>());
        Destroy(GetComponent<CubeRayCastScript>());
        Destroy(GetComponent<LineRenderer>());
    }
}
