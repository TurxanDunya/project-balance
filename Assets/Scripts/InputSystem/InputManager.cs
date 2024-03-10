using UnityEngine;
using UnityEngine.InputSystem;

[DefaultExecutionOrder(-1)]
public class InputManager : MonoBehaviour
{
    private static InputManager instance;
    public bool isOverUI = false;

    public delegate void EndTouchEvent();
    public event EndTouchEvent OnEndTouch;

    public delegate void PerformedTouchEvent(Vector2 position);
    public event PerformedTouchEvent OnPerformedTouch;

    private TouchControls touchControls;

    // This class should be singleton, because of integration with old input system
    public static InputManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<InputManager>();
                return instance;
            }
            else
            {
                return instance;
            }
        }
    }

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
        if (OnEndTouch != null && !isOverUI)
        {
            OnEndTouch();
        }
    }

    private void PerformTouch(InputAction.CallbackContext context)
    {
        bool isTouched = touchControls.CubeController.Touch.IsPressed();
        if (isTouched && OnPerformedTouch != null && !isOverUI)
        {
            OnPerformedTouch(touchControls.CubeController.DragAndMove.ReadValue<Vector2>());
        }
    }

}
