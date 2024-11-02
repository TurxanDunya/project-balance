using UnityEngine;

public class CubeMovement : MonoBehaviour
{
    [SerializeField] private bool invertAxis = false;

    private GameObject platformObj;
    private Platform platform;

    private InputManager inputManager;
    private CubeRayCast cubeRayCast;

    private Vector3 cubePosition;
    private Vector3 previousPosition;

    // cube movement variables
    private Vector3 forwardDirection;
    private Vector3 rightDirection;
    private Vector3 moveDirection;

    private void Awake()
    {
        inputManager = gameObject.AddComponent<InputManager>();
    }

    private void Start()
    {
        cubePosition = transform.position;

        if (invertAxis)
        {
            InvertAxis();
        }

        platformObj = GameObject.FindGameObjectWithTag(TagConstants.MAIN_PLATFORM);
        platform = platformObj.GetComponent<Platform>();

        cubeRayCast = GetComponent<CubeRayCast>();
        cubeRayCast.UpdateLineRendererPosition();

        UpdateCameraDirection();
    }

    private void UpdateCameraDirection()
    {
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

    public void InvertAxis()
    {
        invertAxis = true;
    }

    public void UndoInvertAxis()
    {
        invertAxis = false;
    }

    public void Move(Vector2 delta)
    {
        UpdateCameraDirection();

        gameObject.layer = LayerMask.NameToLayer("Outlined");
        moveDirection = forwardDirection * delta.y + rightDirection * delta.x;
        moveDirection *= Time.deltaTime * 0.03f;

        if (invertAxis)
        {
            moveDirection = -moveDirection;
        }

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
