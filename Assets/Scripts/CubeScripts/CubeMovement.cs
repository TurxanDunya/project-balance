using UnityEngine;

public class CubeMovement : MonoBehaviour
{
    private InputManager inputManager;
    private CubeRayCastScript cubeRayCastScript;

    private Vector3 previousPosition;

    // cube movement variables
    Vector3 forwardDirection;
    Vector3 rightDirection;
    Vector3 moveDirection;

    private void Start()
    {
        cubeRayCastScript = GetComponent<CubeRayCastScript>();
    }

    private void Awake()
    {
        inputManager = gameObject.AddComponent<InputManager>();
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
        cubeRayCastScript.UpdateLineRendererStatus();
    }

    public void Move(Vector2 deltaPosition)
    {
        forwardDirection = Vector3.ProjectOnPlane(Camera.main.transform.forward, Vector3.up).normalized;
        rightDirection = Vector3.ProjectOnPlane(Camera.main.transform.right, Vector3.up).normalized;

        moveDirection = forwardDirection * deltaPosition.y + rightDirection * deltaPosition.x;
        moveDirection *= Time.deltaTime * 0.03f;

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
