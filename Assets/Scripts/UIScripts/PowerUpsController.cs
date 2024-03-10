using UnityEngine;
using UnityEngine.UIElements;

public class PowerUpsController : MonoBehaviour
{
    [SerializeField] private PowerUps powerUps;

    private VisualElement rootVisualElement;
    private Button firstPowerUpButton;

    private InputManager inputManager;

    private void Awake()
    {
        inputManager = InputManager.Instance;
    }

    void Start()
    {
        rootVisualElement = GetComponent<UIDocument>().rootVisualElement;
        firstPowerUpButton = rootVisualElement.Q<Button>("first_power_up");

        firstPowerUpButton.clicked += () => PerformFirstPowerUp();

        rootVisualElement.RegisterCallback<MouseEnterEvent>((evt) =>
        {
            if (evt.target == rootVisualElement)
            {
                inputManager.isOverUI = true;
            }
        });

        rootVisualElement.RegisterCallback<MouseLeaveEvent>((evt) =>
        {
            if (evt.target == rootVisualElement)
            {
                inputManager.isOverUI = false;
            }
        });
    }

    private void PerformFirstPowerUp()
    {
        Debug.Log("Do first power up job!");
    }

}
