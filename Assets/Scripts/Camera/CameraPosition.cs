using System.Collections;
using UnityEngine;

public class CameraPosition : MonoBehaviour
{
    [Header("Params")]
    [SerializeField] private float normalizedDegreeToMoveOppsiteSide = 0.2f;

    private Camera cameraComponent;
    private GameObject platformObj;
    private CubeMovement cubeMovement;

    private GameObject positions;
    private Transform initialTransform;
    private Transform oppositeTransform;

    private bool isOnOppositeSide = false;

    private void Awake()
    {
        cameraComponent = GetComponent<Camera>();
    }

    private void Start()
    {
        platformObj = GameObject.FindGameObjectWithTag(TagConstants.MAIN_PLATFORM);
        positions = GameObject.FindGameObjectWithTag(TagConstants.PREDEFINED_POSITIONS);

        Transform[] predefinedPositions = positions.GetComponentsInChildren<Transform>();
        initialTransform = predefinedPositions[1];
        oppositeTransform = predefinedPositions[2];

        StartCoroutine(ChangePositionOpposite());
    }

    private IEnumerator ChangePositionOpposite()
    {
        while (true)
        {
            Quaternion platformRotation = platformObj.transform.rotation;

            if (platformRotation.x >= normalizedDegreeToMoveOppsiteSide && !isOnOppositeSide)
            {
                transform.position = oppositeTransform.position;
                transform.rotation = oppositeTransform.rotation;

                CheckCubeMovement();
                cubeMovement.InvertAxis();

                isOnOppositeSide = true;
                yield return new WaitForSeconds(1);
                continue;
            }

            if (platformRotation.x <= -normalizedDegreeToMoveOppsiteSide && isOnOppositeSide)
            {
                transform.position = initialTransform.position;
                transform.rotation = initialTransform.rotation;

                CheckCubeMovement();
                cubeMovement.UndoInvertAxis();

                isOnOppositeSide = false;
                yield return new WaitForSeconds(1);
                continue;
            }

            yield return new WaitForSeconds(1);
        }
    }

    /*
     * In fact, we should not do it this way, but we do!
     * Because CORE Component is in prefab. Core components should not be in prefab.
     * CUBE PREFAB IS NOT the CORE. But Movement is core mechanism.
     */
    private CubeMovement CheckCubeMovement()
    {
        if (cubeMovement == null)
        {
            cubeMovement = FindAnyObjectByType<CubeMovement>();
            return cubeMovement;
        }

        return cubeMovement;
    }

}
