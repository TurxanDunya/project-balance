using UnityEngine;
using UnityEngine.UIElements;

public class PowerUpsController : MonoBehaviour
{
    [SerializeField] private PowerUps powerUps;
    [SerializeField] private CubeSpawnManagement cubeSpawnManagement;

    private VisualElement rootElement;
    private VisualElement rootVisualElement;
    private Button firstPowerUpButton;

    void Start()
    {
        rootElement = GetComponent<UIDocument>().rootVisualElement;
        rootVisualElement = rootElement.Q<VisualElement>("power_ups");
        firstPowerUpButton = rootVisualElement.Q<Button>("first_power_up");

        firstPowerUpButton.clicked += () => PerformFirstPowerUp();
    }

    public bool IsOverUI(Vector2 touchPosition)
    {
        float leftBorder = rootVisualElement.layout.x - rootVisualElement.layout.width / 2;
        float rightBorder = rootVisualElement.layout.x + rootVisualElement.layout.width / 2;
        float bottomBorder = rootVisualElement.layout.y - rootVisualElement.layout.height / 2;
        float upBorder = rootVisualElement.layout.y + rootVisualElement.layout.height / 2;
        
        if (touchPosition.x >= leftBorder && touchPosition.x <= rightBorder
            && touchPosition.x >= bottomBorder && touchPosition.y <= upBorder)
        {
            return true;
        }

        return false;
    }

    private void PerformFirstPowerUp()
    {
        bool isCubeChanged = cubeSpawnManagement.ReplaceCubeIfPossible();

        if(!isCubeChanged)
        {
            // TODO: Will ignore power-up
        }
    }

}
