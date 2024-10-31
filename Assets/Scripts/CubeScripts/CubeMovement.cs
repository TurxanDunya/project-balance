using System.Linq;
using UnityEngine;

public class CubeMovement : MonoBehaviour
{
    private GameObject platformObj;
    private Platform platform;

    private InputManager inputManager;
    private CubeRayCast cubeRayCast;
    private InvertMovement invertMovement;

    private Vector3 cubePosition;
    private Vector3 previousPosition;

    // cube movement variables
    Vector3 forwardDirection;
    Vector3 rightDirection;
    Vector3 moveDirection;

    private void Awake()
    {
        inputManager = gameObject.AddComponent<InputManager>();
        invertMovement = FindAnyObjectByType<InvertMovement>();
    }

    private void Start()
    {
        cubePosition = transform.position;

        platformObj = GameObject.FindGameObjectWithTag(TagConstants.MAIN_PLATFORM);
        platform = platformObj.GetComponent<Platform>();

        cubeRayCast = GetComponent<CubeRayCast>();
        cubeRayCast.UpdateLineRendererPosition();

        // Since the camera is static, calculating only once should be enough
        forwardDirection = Vector3.ProjectOnPlane(
           Camera.main.transform.forward, Vector3.up).normalized;
        rightDirection = Vector3.ProjectOnPlane(
            Camera.main.transform.right, Vector3.up).normalized;
    }

    private void OnEnable()
    {
        inputManager.OnPerformedTouch += Move;
    }

    private void OnDisable()
    {
        inputManager.OnPerformedTouch -= Move;
    }

    private void Update()
    {
        MoveCubeWithPlatform();
        cubeRayCast.UpdateLineRendererPosition();
    }

    public void Move(Vector2 delta)
    {
        gameObject.layer = LayerMask.NameToLayer("Outlined");
        moveDirection = forwardDirection * delta.y + rightDirection * delta.x;
        moveDirection *= Time.deltaTime * 0.03f;

        moveDirection = invertMovement != null 
            ? invertMovement.invertVector(moveDirection) : moveDirection;
        moveDirection.y = 0;
        transform.position += moveDirection;

        KeepCubeInPlatformArea();
    }

    private void KeepCubeInPlatformArea()
    {
        if (platform.IsPositionInPlatformArea(transform.position))
        {
            previousPosition = transform.position;
            return;
        }

        transform.position = previousPosition;
    }

    private void MoveCubeWithPlatform()
    {
        if (cubeRayCast.IsHittingPlayables())
        {
            return;
        }

        Vector3 targetPosition = platformObj.transform.position;
        targetPosition.y = cubePosition.y;

        transform.position = Vector3.MoveTowards(
            transform.position, targetPosition, Time.deltaTime / 2);
    }

}
