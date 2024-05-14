using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.SceneManagement;

public class PowerUpsController : MonoBehaviour
{
    [Header("Dependant controllers")]
    [SerializeField] private PausePopUpController pausePopUpController;

    [SerializeField] private PowerUps powerUps;
    [SerializeField] private CubeSpawnManagement cubeSpawnManagement;
    [SerializeField] private TimeLevelFinish timeLevelFinish;

    private VisualElement rootElement;
    private VisualElement powerUpsVE;
    private Button firstPowerUpButton;
    private Button secondPowerUpButton;
    private Button thirdPowerUpButton;
    private Button fourthPowerUpButton;

    private VisualElement topVE;
    private Button pauseButton;
    private Button levelsButton;

    // VE coord fields
    float leftBorder;
    float rightBorder;
    float bottomBorder;
    float upBorder;

    void Start()
    {
        rootElement = GetComponent<UIDocument>().rootVisualElement;

        powerUpsVE = rootElement.Q<VisualElement>("power_ups");
        firstPowerUpButton = powerUpsVE.Q<Button>("first_power_up");
        secondPowerUpButton = powerUpsVE.Q<Button>("second_power_up");
        thirdPowerUpButton = powerUpsVE.Q<Button>("third_power_up");
        fourthPowerUpButton = powerUpsVE.Q<Button>("fourth_power_up");

        topVE = rootElement.Q<VisualElement>("top-VE");
        pauseButton = topVE.Q<Button>("Pause");
        levelsButton = topVE.Q<Button>("Levels");

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
        leftBorder = powerUpsVE.layout.x - powerUpsVE.layout.width / 2;
        rightBorder = powerUpsVE.layout.x + powerUpsVE.layout.width / 2;
        bottomBorder = powerUpsVE.layout.y;
        upBorder = powerUpsVE.layout.y + powerUpsVE.layout.height;
    }

    private void BindEventsWithFunctions()
    {
        firstPowerUpButton.clicked += () => PerformFirstPowerUp();
        secondPowerUpButton.clicked += () => PerformSecondPowerUp();
        thirdPowerUpButton.clicked += () => PerformThirdPowerUp();
        fourthPowerUpButton.clicked += () => PerformFourthPowerUp();

        pauseButton.clicked += () => PauseGame();
        levelsButton.clicked += () => ShowLevels();
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

    private void PerformFourthPowerUp()
    {
        cubeSpawnManagement.ReplaceWithBomb();
    }

    private void PauseGame()
    {
        Time.timeScale = 0;
        pausePopUpController.Show();
    }

    private void ShowLevels()
    {
        SceneManager.LoadScene(LevelNameConstants.LEVEL_SCENE_NAME);
    }

}
