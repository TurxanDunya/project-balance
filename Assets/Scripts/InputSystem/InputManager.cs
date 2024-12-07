using UnityEngine;
using UnityEngine.InputSystem;
using CandyCoded.HapticFeedback;

[DefaultExecutionOrder(-1)]
public class InputManager : MonoBehaviour
{
    private static InputManager instance;
    private bool isOverUI = false;
    public static bool isCancelButtonEnabled;

    public delegate void EndTouchEvent();
    public event EndTouchEvent OnEndTouch;

    public delegate void PerformedTouchEvent(Vector2 delta);
    public event PerformedTouchEvent OnPerformedTouch;

    private TouchControls touchControls;

    private UIButtonsTemplate uIButtonsTemplate;

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

        uIButtonsTemplate = FindObjectOfType<UIButtonsTemplate>();
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
        touchControls.CubeController.Touch.started += touchContext => StartTouch(touchContext);
        touchControls.CubeController.Touch.canceled += _ => EndTouch();

        touchControls.CubeController.DragAndMove.started += moveContext => PerformTouch(moveContext);
    }

    // In start touch we define if overlapping UI, and if so all other inputs will be blocked
    private void StartTouch(InputAction.CallbackContext touchContext)
    {
        Vector2 touchPosition = touchContext.ReadValue<Vector2>();

        if (!uIButtonsTemplate)
        {
            isOverUI = true;
            return;
        }

        if (uIButtonsTemplate.IsOverlappingAnyUI(touchPosition))
        {
            isOverUI = true;
        }
        else
        {
           HapticFeedback.HeavyFeedback();
           isOverUI = false;
        }
}

    private void EndTouch()
    {
        if (OnEndTouch != null && !isOverUI && !isCancelButtonEnabled)
        {
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
