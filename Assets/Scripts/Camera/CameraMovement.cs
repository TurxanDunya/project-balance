using System.Collections;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    [Header("Params")]
    [SerializeField] private float moveSpeed = 4.0f;
    [SerializeField] private float fovStartValue = 70.0f;
    [SerializeField] private float fovFinishValue = 55.0f;

    private Camera cameraComponent;

    private void Awake()
    {
        cameraComponent = GetComponent<Camera>();
    }

    private void Start()
    {
        cameraComponent.fieldOfView = fovStartValue;
        StartCoroutine(SmoothFOVTransition());
    }

    private IEnumerator SmoothFOVTransition()
    {
        while (cameraComponent.fieldOfView > fovFinishValue)
        {
            cameraComponent.fieldOfView -= moveSpeed * Time.deltaTime;
            yield return null;
        }

        cameraComponent.fieldOfView = fovFinishValue;
    }

}
