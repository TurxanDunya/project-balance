using UnityEngine;
using UnityEngine.UIElements;

public class PowerUpsController : MonoBehaviour
{
    [SerializeField] private PowerUps powerUps;
    [SerializeField] private CubeSpawnManagement cubeSpawnManagement;
    [SerializeField] private TimeLevelFinish timeLevelFinish;

    private VisualElement rootElement;
    private VisualElement rootVisualElement;
    private Button firstPowerUpButton;
    private Button secondPowerUpButton;
    private Button thirdPowerUpButton;

    // VE coord fields
    float leftBorder;
    float rightBorder;
    float bottomBorder;
    float upBorder;

    void Start()
    {
        rootElement = GetComponent<UIDocument>().rootVisualElement;
        rootVisualElement = rootElement.Q<VisualElement>("power_ups");

        firstPowerUpButton = rootVisualElement.Q<Button>("first_power_up");
        secondPowerUpButton = rootVisualElement.Q<Button>("second_power_up");
        thirdPowerUpButton = rootVisualElement.Q<Button>("third_power_up");

        BindEventsWithFunctions();
    }

    public bool IsOverUI(Vector2 touchPosition)
    {
        CalculateRootVECoords();

        if (touchPosition.x >= leftBorder && touchPosition.x <= rightBorder
            && touchPosition.y >= bottomBorder && touchPosition.y <= upBorder)
        {
            return true;
        }

        return false;
    }

    private void CalculateRootVECoords()
    {
        leftBorder = rootVisualElement.layout.x - rootVisualElement.layout.width / 2;
        rightBorder = rootVisualElement.layout.x + rootVisualElement.layout.width / 2;
        bottomBorder = rootVisualElement.layout.y;
        upBorder = rootVisualElement.layout.y + rootVisualElement.layout.height;
    }

    private void BindEventsWithFunctions()
    {
        firstPowerUpButton.clicked += () => PerformFirstPowerUp();
        secondPowerUpButton.clicked += () => PerformSecondPowerUp();
        thirdPowerUpButton.clicked += () => PerformThirdPowerUp();
    }

    private void PerformFirstPowerUp()
    {
        bool isCubeChanged = cubeSpawnManagement.ReplaceCubeIfPossible();

        if(!isCubeChanged)
        {
            // TODO: Will ignore power-up and will purchase some coin
        }
    }

    private void PerformSecondPowerUp()
    {
        timeLevelFinish.IncreaseFinishTime(5);
    }

    private void PerformThirdPowerUp()
    {
        cubeSpawnManagement.ReplaceWithMagnet();
    }

}
