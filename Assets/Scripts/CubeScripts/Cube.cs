using UnityEngine;

public class Cube : MonoBehaviour
{
    [SerializeField]
    private ParticleSystem rippleEffect;

    private void OnEnable()
    {
        Platform.CubeLanded += CubeLanded;
    }

    private void OnDisable()
    {
        Platform.CubeLanded -= CubeLanded;
    }


    private void CubeLanded() {
        rippleEffect.Play();
        Platform.CubeLanded -= CubeLanded;
    }


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
