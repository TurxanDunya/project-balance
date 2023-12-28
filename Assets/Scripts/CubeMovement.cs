using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class CubeMovement : MonoBehaviour
{
    [SerializeField] private InputAction pressed, axis;
    [SerializeField] private float moveSpeed = 1.0f;
    [SerializeField] private float cubeSpawnDelay = 3.0f;
    [SerializeField] private CubeSpawner cubeSpawner;
    [SerializeField] private GameObject plaformMesh;

    private Vector2 movement;
    private Vector3 previousPosition;
    private readonly bool moveAllowed = true;

    private Rigidbody rb;
    private LineRenderer lineRenderer;
    private CubeRayCastScript cubeRayCastScript;

    // cube movement variables
    Vector3 forwardNormalizedVector;
    Vector3 rightNormalizedVector;
    Vector3 moveDirection;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        lineRenderer = GetComponent<LineRenderer>();
        cubeRayCastScript = GetComponent<CubeRayCastScript>();
    }

    private void Awake()
    {
        pressed.Enable();
        axis.Enable();

        pressed.performed += _ => {
            StartCoroutine(Move()); 
        };

        pressed.canceled += _ => { 
            ReleaseObject();
            StartCoroutine(SpawnCubeAfterDelay(cubeSpawnDelay));
        };

        axis.performed += context => { movement = context.ReadValue<Vector2>(); };
        axis.canceled += _ => { movement = Vector2.zero; };
    }

    private void Update()
    {
        KeepMoveableCubeInPlatformArea();
    }

    public IEnumerator Move()
    {
        while (moveAllowed)
        {
            forwardNormalizedVector = transform.forward.normalized;
            rightNormalizedVector = transform.right.normalized;
           
            moveDirection = forwardNormalizedVector * movement.y + rightNormalizedVector * movement.x;
            moveDirection *= moveSpeed * Time.deltaTime;

            transform.position += moveDirection;
            yield return null;
        }
    }

    private void ReleaseObject()
    {
        rb.useGravity = true;

        pressed.Disable();
        axis.Disable();

        Destroy(lineRenderer);
    }

    // Coroutine to spawn the cube after a delay
    private IEnumerator SpawnCubeAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        cubeSpawner.SpawnCube();
    }

    private void KeepMoveableCubeInPlatformArea()
    {
        if (cubeRayCastScript.IsHittingPlatform())
        {
            previousPosition = transform.position;
            return;
        }

        transform.position = previousPosition;
    }

}
