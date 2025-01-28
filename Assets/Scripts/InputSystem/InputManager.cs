using UnityEngine;
using UnityEngine.InputSystem;
using CandyCoded.HapticFeedback;

[DefaultExecutionOrder(-1)]
public class InputManager : MonoBehaviour
{
    private static InputManager instance;
    public static bool isOverUI = false;

    public delegate void EndTouchEvent();
    public event EndTouchEvent OnEndTouch;

    public delegate void PerformedTouchEvent(Vector2 delta);
    public event PerformedTouchEvent OnPerformedTouch;

    private TouchControls touchControls;

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
        touchControls.CubeController.Touch.canceled -= touchContext => EndTouch();
        touchControls.CubeController.DragAndMove.started -= moveContext => PerformTouch(moveContext);

        touchControls.Disable();
    }

    private void Start()
    {
        touchControls.CubeController.Touch.canceled += touchContext => EndTouch();
        touchControls.CubeController.DragAndMove.started += moveContext => PerformTouch(moveContext);
    }

    private void EndTouch()
    {
        if (OnEndTouch != null && !isOverUI)
        {
            HapticFeedback.MediumFeedback();
            OnEndTouch();
        }
    }

    private void PerformTouch(InputAction.CallbackContext moveContext)
    {
        if (OnPerformedTouch != null && !isOverUI)
        {
            Vector2 delta = moveContext.ReadValue<Vector2>();
            OnPerformedTouch(delta);
        }
    }

}
