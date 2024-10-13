using UnityEngine;
using UnityEngine.EventSystems;


public class CubeMovement : MonoBehaviour
{
    private InputManager inputManager;
    private CubeRayCast cubeRayCastScript;
    private InvertMovement invertMovement;

    private Vector3 previousPosition;

    // cube movement variables
    Vector3 forwardDirection;
    Vector3 rightDirection;
    Vector3 moveDirection;

    private void Start()
    {
        cubeRayCastScript = GetComponent<CubeRayCast>();

        // Since the camera is static, calculating only once should be enough
        forwardDirection = Vector3.ProjectOnPlane(
           Camera.main.transform.forward, Vector3.up).normalized;
        rightDirection = Vector3.ProjectOnPlane(
            Camera.main.transform.right, Vector3.up).normalized;
    }

    private void Awake()
    {
        inputManager = gameObject.AddComponent<InputManager>();
        invertMovement = FindAnyObjectByType<InvertMovement>();
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
        KeepCubeInPlatformArea();
        cubeRayCastScript.UpdateLineRendererPosition();
    }

    public void Move(Vector2 delta)
    {
        gameObject.layer = LayerMask.NameToLayer("Outlined");
        moveDirection = forwardDirection * delta.y + rightDirection * delta.x;
        moveDirection *= Time.deltaTime * 0.03f;

        moveDirection = invertMovement != null ? invertMovement.invertVector(moveDirection) : moveDirection;
        moveDirection.y = 0;
        transform.position += moveDirection;
    }

    private void KeepCubeInPlatformArea()
    {
        if (cubeRayCastScript.IsHittingPlatform())
        {
            previousPosition = transform.position;
            return;
        }

        transform.position = previousPosition;
    }

}
