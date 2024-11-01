using UnityEngine;

public class CubeMoveAxisInverter : MonoBehaviour
{
    private CubeMovement cubeMovement;

    private void Update()
    {
        if (cubeMovement == null)
        {
            cubeMovement = FindAnyObjectByType<CubeMovement>();
        }

        if (cubeMovement != null)
        {
            cubeMovement.InvertAxis();
        }
    }
}
