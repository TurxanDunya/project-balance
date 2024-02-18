using UnityEngine;
using UnityEngine.InputSystem;

[DefaultExecutionOrder(-1)]
public class InputManager : MonoBehaviour
{
    public delegate void EndTouchEvent();
    public event EndTouchEvent OnEndTouch;

    public delegate void PerformedTouchEvent(Vector2 position);
    public event PerformedTouchEvent OnPerformedTouch;

    private TouchControls touchControls;

    private void Awake()
    {
        touchControls = new TouchControls();
    }

    private void OnEnable()
    {
        touchControls.Enable();
    }

    private void OnDisable()
    {
        touchControls.Disable();
    }

    private void Start()
    {
        touchControls.CubeController.Touch.canceled += _ => EndTouch();

        touchControls.CubeController.DragAndMove.started += ctx => PerformTouch(ctx);
        touchControls.CubeController.DragAndMove.performed -= ctx => PerformTouch(ctx);
    }

    private void EndTouch()
    {
        if (OnEndTouch != null)
        {
            OnEndTouch();
        }
    }

    private void PerformTouch(InputAction.CallbackContext context)
    {
        bool isTouched = touchControls.CubeController.Touch.IsPressed();
        if (isTouched && OnPerformedTouch != null)
        {
            OnPerformedTouch(touchControls.CubeController.DragAndMove.ReadValue<Vector2>());
        }
    }

}
