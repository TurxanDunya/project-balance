using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class CubeMovement : MonoBehaviour
{
    [SerializeField] public InputAction pressed, axis;
    [SerializeField] private float moveSpeed = 1.0f;

    private Vector2 movement;
    private Vector3 previousPosition;
    private readonly bool moveAllowed = true;

    private CubeRayCastScript cubeRayCastScript;

    // cube movement variables
    Vector3 forwardNormalizedVector;
    Vector3 rightNormalizedVector;
    Vector3 moveDirection;

    private void Start()
    {
        cubeRayCastScript = GetComponent<CubeRayCastScript>();
    }

    private void OnEnable()
    {
        pressed.Enable();
        axis.Enable();

        pressed.performed += _ => {
            Debug.Log("basildi");
            StartCoroutine(Move()); 
        };

        axis.performed += context => {
            Debug.Log("hereketde" + context.ReadValue<Vector2>());
            movement = context.ReadValue<Vector2>();
        };

        axis.canceled += _ => {
            movement = Vector2.zero;
        };
    }

    private void OnDisable()
    {
        StopAllCoroutines();
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
