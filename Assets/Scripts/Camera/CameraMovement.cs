using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    [Header("Params")]
    [SerializeField] private float moveSpeed = 4.0f;
    [SerializeField] private float fovStartValue = 70.0f;
    [SerializeField] private float fovFinishValue = 55.0f;

    private Camera camera;

    private void Awake()
    {
        camera = GetComponent<Camera>();
    }

    private void Start()
    {
        camera.fieldOfView = fovStartValue;
    }

    private void FixedUpdate()
    {
        if (camera.fieldOfView <= fovFinishValue)
        {
            return;
        }

        camera.fieldOfView -= moveSpeed * Time.fixedDeltaTime;
    }

}
