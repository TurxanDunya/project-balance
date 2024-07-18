using System.Collections.Generic;

using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.SceneManagement;

public class InGameUIController : MonoBehaviour
{
    [Header("Dependant controllers")]
    [SerializeField] private PauseScreenController pauseScreenController;

    [SerializeField] private StateChanger stateChanger;

    [SerializeField] private PowerUps powerUps;
    [SerializeField] private CubeSpawnManagement cubeSpawnManagement;
    [SerializeField] private TimeLevelFinish timeLevelFinish;
    [SerializeField] private CoinManager coinManager;

    private CubeCounter cubeCounter;

    private VisualElement rootElement;

    private VisualElement powerUpsVE;
    private Button firstPowerUpButton;
    private Button secondPowerUpButton;
    private Button thirdPowerUpButton;
    private Button fourthPowerUpButton;

    private VisualElement topVE;
    private Button pauseButton;
    private Button levelsButton;

    private VisualElement leftCubesCountVE;
    private VisualElement woodVE;
    private Label woodCountLabel;
    private VisualElement metalVE;
    private Label metalCountLabel;
    private VisualElement iceVE;
    private Label iceCountLabel;

    // VE coord fields
    float leftBorder;
    float rightBorder;
    float bottomBorder;
    float upBorder;

    private void Awake()
    {
        cubeCounter = FindAnyObjectByType<CubeCounter>();
    }

    private void OnEnable()
    {
        cubeCounter.OnUpdateCubeCount += UpdateCubeCounts;
        coinManager.OnCoinCountChangeEvent += UpdatePowerUpIconStatusesByCoinCount;
    }

    private void OnDisable()
    {
        cubeCounter.OnUpdateCubeCount -= UpdateCubeCounts;
        coinManager.OnCoinCountChangeEvent -= UpdatePowerUpIconStatusesByCoinCount;
    }

    private void Start()
    {
        rootElement = GetComponent<UIDocument>()
            .rootVisualElement.Q<VisualElement>("rootVE");

        powerUpsVE = rootElement.Q<VisualElement>("power_ups");
        firstPowerUpButton = powerUpsVE.Q<Button>("first_power_up");
        secondPowerUpButton = powerUpsVE.Q<Button>("second_power_up");
        thirdPowerUpButton = powerUpsVE.Q<Button>("third_power_up");
        fourthPowerUpButton = powerUpsVE.Q<Button>("fourth_power_up");

        topVE = rootElement.Q<VisualElement>("topVE");
        pauseButton = topVE.Q<Button>("Pause");
        levelsButton = topVE.Q<Button>("Levels");
        levelsButton.text = SceneManager.GetActiveScene().name;

        leftCubesCountVE = rootElement.Q<VisualElement>("left_cubes_count_VE");
        woodVE = leftCubesCountVE.Q<VisualElement>("wood_VE");
        woodCountLabel = woodVE.Q<Label>("wood_count_lbl");
        metalVE = leftCubesCountVE.Q<VisualElement>("metal_VE");
        metalCountLabel = metalVE.Q<Label>("metal_count_lbl");
        iceVE = leftCubesCountVE.Q<VisualElement>("ice_VE");
        iceCountLabel = iceVE.Q<Label>("ice_count_lbl");

        BindEventsWithFunctions();
        UpdatePowerUpIconStatusesByCoinCount(coinManager.CoinCount);

        UpdateCubeCounts(
            cubeCounter.WoodCount,
            cubeCounter.MetalCount,
            cubeCounter.IceCount,
            cubeCounter.RockCount);

        SafeArea.ApplySafeArea(rootElement);
    }

    public bool IsOverUI(Vector2 touchPosition)
    {
        CalculateRootVECoords();

        // Because VE position coord and touch coord differs in y axis
        touchPosition.y = rootElement.layout.height - touchPosition.y;
        if (touchPosition.x >= leftBorder && touchPosition.x <= rightBorder
            && touchPosition.y <= bottomBorder && touchPosition.y >= upBorder)
        {
            return true;
        }

        return false;
    }

    private void CalculateRootVECoords()
    {
        leftBorder = powerUpsVE.layout.x;
        rightBorder = powerUpsVE.layout.x + powerUpsVE.layout.width;
        bottomBorder = powerUpsVE.layout.y + powerUpsVE.layout.height;
        upBorder = powerUpsVE.layout.y;
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
        if(coinManager.IsCoinEnough(PowerUpPriceConstants.CHANGE_CUBE))
        {
            cubeSpawnManagement.ReplaceCube();
            coinManager.SubtractCoin(PowerUpPriceConstants.CHANGE_CUBE);
        }
    }

    private void PerformSecondPowerUp()
    {
        if(coinManager.IsCoinEnough(PowerUpPriceConstants.ADD_TIME))
        {
            timeLevelFinish.IncreaseFinishTime(10);
            coinManager.SubtractCoin(PowerUpPriceConstants.ADD_TIME);
        }
    }

    private void PerformThirdPowerUp()
    {
        if(coinManager.IsCoinEnough(PowerUpPriceConstants.MAGNET))
        {
            cubeSpawnManagement.ReplaceWithMagnet();
            coinManager.SubtractCoin(PowerUpPriceConstants.MAGNET);
        }
    }

    private void PerformFourthPowerUp()
    {
        if (coinManager.IsCoinEnough(PowerUpPriceConstants.BOMB))
        {
            cubeSpawnManagement.ReplaceWithBomb();
            coinManager.SubtractCoin(PowerUpPriceConstants.BOMB);
        }
    }

    private void PauseGame()
    {
        pauseScreenController.ChangeStateToPausePage();
    }

    private void ShowLevels()
    {
        stateChanger.ChangeStateToLevelMenu();
    }

    private void UpdateCubeCounts(int woodCount, int metalCount, int iceCount, int rockCount)
    {
        woodCountLabel.text = woodCount.ToString();
        metalCountLabel.text = metalCount.ToString();
        iceCountLabel.text = iceCount.ToString();
    }

    private void UpdatePowerUpIconStatusesByCoinCount(long coinCount)
    {
        Button[] powerUpButtons = {
            firstPowerUpButton,
            secondPowerUpButton,
            thirdPowerUpButton,
            fourthPowerUpButton
        };

        int[] coinThresholds = {
            PowerUpPriceConstants.CHANGE_CUBE,
            PowerUpPriceConstants.ADD_TIME,
            PowerUpPriceConstants.MAGNET,
            PowerUpPriceConstants.BOMB
        };

        for (int i = 0; i < powerUpButtons.Length; i++)
        {
            if (coinCount < coinThresholds[i])
            {
                for (int j = i; j < powerUpButtons.Length; j++)
                {
                    powerUpButtons[j].style.opacity = 0.5f;
                    powerUpButtons[j].SetEnabled(false);
                }
                return;
            }
            else
            {
                powerUpButtons[i].style.opacity = 1.0f;
                powerUpButtons[i].SetEnabled(true);
            }
        }
    }
}
