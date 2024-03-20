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

        touchControls.CubeController.DragAndMove.started += moveContext => PerformTouch(moveContext);
    }

    private void EndTouch()
    {
        if (OnEndTouch != null && !isOverUI)
        {
            OnEndTouch();
        }
    }

    private void PerformTouch(InputAction.CallbackContext moveContext)
    {
        if (OnPerformedTouch != null && !isOverUI)
        {
            OnPerformedTouch(moveContext.ReadValue<Vector2>());
        }
    }

}
