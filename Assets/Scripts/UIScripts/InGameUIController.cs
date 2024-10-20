using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.SceneManagement;
using Unity.VisualScripting;

public class InGameUIController : MonoBehaviour, IControllerTemplate
{
    [Header("Dependant controllers")]
    [SerializeField] private PauseScreenController pauseScreenController;

    [SerializeField] private StateChanger stateChanger;

    [SerializeField] private PowerUps powerUps;
    [SerializeField] private CubeSpawnManagement cubeSpawnManagement;
    [SerializeField] private TimeLevelFinish timeLevelFinish;
    [SerializeField] private CoinManager coinManager;

    private CubeCounter cubeCounter;
    private UIElementEnabler uiElementEnabler;

    private VisualElement rootElement;

    private VisualElement powerUpsVE;
    private Button firstPowerUpButton;
    private Button secondPowerUpButton;
    private Button thirdPowerUpButton;
    private Button fourthPowerUpButton;

    private VisualElement topVE;
    private Button pauseButton;
    private Button levelsButton;

    // left pane icons
    private VisualElement leftCubesCountVE;
    private VisualElement woodVE;
    private Label woodCountLabel;
    private VisualElement metalVE;
    private Label metalCountLabel;
    private VisualElement iceVE;
    private Label iceCountLabel;

    // right pane icons
    private VisualElement rightVE;
    private VisualElement ghostCubeVE;
    private VisualElement lightOnOffVE;
    private VisualElement timerVE;
    private Label currentTimeLbl;
    private VisualElement invertModeVE;
    private VisualElement fallingShapeVE;
    private VisualElement windModeVE;
    private VisualElement cubeLateFallVE;
   
    private void Awake()
    {
        cubeCounter = FindAnyObjectByType<CubeCounter>();
        uiElementEnabler = FindAnyObjectByType<UIElementEnabler>();
    }

    private void OnEnable()
    {
        cubeCounter.OnUpdateCubeCount += UpdateCubeCounts;
        cubeCounter.OnCanReplaceCubeEvent += UpdateFirstPowerUpIconStatus;
        coinManager.OnCoinCountChangeEvent += UpdatePowerUpIconStatusesByCoinCount;

        if (timeLevelFinish != null)
        {
            timeLevelFinish.OnUpdateSecondEvent += UpdateTimerModeIconLabel;
        }
    }

    private void OnDisable()
    {
        cubeCounter.OnUpdateCubeCount -= UpdateCubeCounts;
        cubeCounter.OnCanReplaceCubeEvent -= UpdateFirstPowerUpIconStatus;
        coinManager.OnCoinCountChangeEvent -= UpdatePowerUpIconStatusesByCoinCount;

        if (timeLevelFinish != null)
        {
            timeLevelFinish.OnUpdateSecondEvent -= UpdateTimerModeIconLabel;
        }
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

        rightVE = rootElement.Q<VisualElement>("right_ve");
        ghostCubeVE = rightVE.Q<VisualElement>("ghost_cube_ve");
        lightOnOffVE = rightVE.Q<VisualElement>("light_on_off_ve");
        timerVE = rightVE.Q<VisualElement>("timer_ve");
        currentTimeLbl = timerVE.Q<Label>("current_time_lbl");
        invertModeVE = rightVE.Q<VisualElement>("invert_mode_ve");
        fallingShapeVE = rightVE.Q<VisualElement>("falling_shape_ve");
        windModeVE = rightVE.Q<VisualElement>("wind_mode_ve");
        cubeLateFallVE = rightVE.Q<VisualElement>("cube_late_fall_ve");

        DefineUIElementsVisibility();
        BindEventsWithFunctions();
        UpdatePowerUpIconStatusesByCoinCount(coinManager.CoinCount);

        UpdateCubeCounts(
            cubeCounter.WoodCount,
            cubeCounter.MetalCount,
            cubeCounter.IceCount,
            cubeCounter.RockCount);

        UpdateFirstPowerUpIconStatus(cubeCounter.IsCubeExistOnDifferentTypes());

        SafeArea.ApplySafeArea(rootElement);
    }

    public bool IsOverUI(Vector2 touchPosition)
    {
        CalculatePowerUpsVECoords();
        if (IsOnButton(touchPosition))
        {
            return true;
        }

        CalculateTopVECoords();
        if (IsOnButton(touchPosition))
        {
            return true;
        }

        return false;
    }

    private void UpdateTimerModeIconLabel(int second)
    {
        if (currentTimeLbl == null)
        {
            return;
        }

        currentTimeLbl.text = second.ToString();

        if (second <= 10 && second % 2 == 0)
        {
            currentTimeLbl.style.color = new Color(255, 0, 0);
        }
        else
        {
            currentTimeLbl.style.color = new Color(255, 255, 255);
        }
    }

    float leftBorder;
    float rightBorder;
    float bottomBorder;
    float upBorder;
    private void CalculatePowerUpsVECoords()
    {
        leftBorder = powerUpsVE.layout.x;
        rightBorder = powerUpsVE.layout.x + powerUpsVE.layout.width;
        bottomBorder = powerUpsVE.layout.y + powerUpsVE.layout.height;
        upBorder = powerUpsVE.layout.y;
    }

    private void CalculateTopVECoords()
    {
        leftBorder = topVE.layout.x;
        rightBorder = topVE.layout.x + topVE.layout.width;
        bottomBorder = topVE.layout.y + 530.0f;
        upBorder = topVE.layout.y;
    }

    private bool IsOnButton(Vector2 touchPosition)
    {
        // Because VE position coord and touch coord differs in y axis
        touchPosition.y = rootElement.layout.height - touchPosition.y;
        if (touchPosition.x >= leftBorder && touchPosition.x <= rightBorder
            && touchPosition.y <= bottomBorder && touchPosition.y >= upBorder)
        {
            return true;
        }

        return false;
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
            bool isSuccess = cubeSpawnManagement.ReplaceCube();
            if (isSuccess)
            {
                coinManager.SubtractCoin(PowerUpPriceConstants.CHANGE_CUBE);
            }
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
        if(!coinManager.IsCoinEnough(PowerUpPriceConstants.MAGNET)
            || thirdPowerUpButton.style.opacity != 1.0f)
        {
            return;
        }

        bool isSuccess = cubeSpawnManagement.ReplaceWithMagnet();
        if (isSuccess)
        {
            DisableThirdPowerUp();
            coinManager.SubtractCoin(PowerUpPriceConstants.MAGNET);
        }
    }

    private void PerformFourthPowerUp()
    {
        if (!coinManager.IsCoinEnough(PowerUpPriceConstants.BOMB))
        {
            return;
        }

        bool isSuccess = cubeSpawnManagement.ReplaceWithBomb();
        if (isSuccess)
        {
            coinManager.SubtractCoin(PowerUpPriceConstants.BOMB);
        }
    }

    private void DefineUIElementsVisibility()
    {
        ShouldShowFirstPowerUp(uiElementEnabler.isCubeChangerEnabled);
        ShouldShowSecondPowerUp(uiElementEnabler.isTimerEnabled);
        ShouldShowThirdPowerUp(uiElementEnabler.isMagnetEnabled);
        ShouldShowFourthPowerUp(uiElementEnabler.isBombEnabled);

        ShouldShowWoodCounter(uiElementEnabler.isWoodCubeEnabled);
        ShouldShowMetalCounter(uiElementEnabler.isMetalCubeEnabled);
        ShouldShowIceCounter(uiElementEnabler.isIceCubeEnabled);

        ShouldShowGhostMode(uiElementEnabler.isGhostModeEnabled);
        ShouldShowLightOnOffMode(uiElementEnabler.isLightBlinkModeEnabled);
        ShouldShowTimerMode(uiElementEnabler.isTimeModeEnabled);
        ShouldShowInvertMode(uiElementEnabler.isInvertModeEnabled);
        ShouldShowFallingShapeMode(uiElementEnabler.isFallingShapesModeEnabled);
        ShouldShowWindMode(uiElementEnabler.isWindModeEnabled);
        ShouldShowCubeLateFall(uiElementEnabler.isCubeLateFallEnabled);
    }

    private void ShouldShowFirstPowerUp(bool isVisible)
    {
        if (isVisible)
        {
            firstPowerUpButton.style.display = DisplayStyle.Flex;
        }
        else
        {
            firstPowerUpButton.style.display = DisplayStyle.None;
        }
    }

    private void ShouldShowSecondPowerUp(bool isVisible)
    {
        if (isVisible)
        {
            secondPowerUpButton.style.display = DisplayStyle.Flex;
        }
        else
        {
            secondPowerUpButton.style.display = DisplayStyle.None;
        }
    }

    private void ShouldShowThirdPowerUp(bool isVisible)
    {
        if (isVisible)
        {
            thirdPowerUpButton.style.display = DisplayStyle.Flex;
        }
        else
        {
            thirdPowerUpButton.style.display = DisplayStyle.None;
        }
    }

    private void ShouldShowFourthPowerUp(bool isVisible)
    {
        if (isVisible)
        {
            fourthPowerUpButton.style.display = DisplayStyle.Flex;
        }
        else
        {
            fourthPowerUpButton.style.display = DisplayStyle.None;
        }
    }

    private void ShouldShowWoodCounter(bool isVisible)
    {
        if (isVisible)
        {
            woodVE.style.display = DisplayStyle.Flex;
        }
        else
        {
            woodVE.style.display = DisplayStyle.None;
        }
    }

    private void ShouldShowMetalCounter(bool isVisible)
    {
        if (isVisible)
        {
            metalVE.style.display = DisplayStyle.Flex;
        }
        else
        {
            metalVE.style.display = DisplayStyle.None;
        }
    }

    private void ShouldShowIceCounter(bool isVisible)
    {
        if (isVisible)
        {
            iceVE.style.display = DisplayStyle.Flex;
        }
        else
        {
            iceVE.style.display = DisplayStyle.None;
        }
    }

    private void ShouldShowGhostMode(bool isVisible)
    {
        if (isVisible)
        {
            ghostCubeVE.style.display = DisplayStyle.Flex;
        }
        else
        {
            ghostCubeVE.style.display = DisplayStyle.None;
        }
    }

    private void ShouldShowLightOnOffMode(bool isVisible)
    {
        if (isVisible)
        {
            lightOnOffVE.style.display = DisplayStyle.Flex;
        }
        else
        {
            lightOnOffVE.style.display = DisplayStyle.None;
        }
    }

    private void ShouldShowTimerMode(bool isVisible)
    {
        if (isVisible)
        {
            timerVE.style.display = DisplayStyle.Flex;
        }
        else
        {
            timerVE.style.display = DisplayStyle.None;
        }
    }

    private void ShouldShowInvertMode(bool isVisible)
    {
        if (isVisible)
        {
            invertModeVE.style.display = DisplayStyle.Flex;
        }
        else
        {
            invertModeVE.style.display = DisplayStyle.None;
        }
    }

    private void ShouldShowFallingShapeMode(bool isVisible)
    {
        if (isVisible)
        {
            fallingShapeVE.style.display = DisplayStyle.Flex;
        }
        else
        {
            fallingShapeVE.style.display = DisplayStyle.None;
        }
    }

    private void ShouldShowWindMode(bool isVisible)
    {
        if (isVisible)
        {
            windModeVE.style.display = DisplayStyle.Flex;
        }
        else
        {
            windModeVE.style.display = DisplayStyle.None;
        }
    }

    private void ShouldShowCubeLateFall(bool isVisible)
    {
        if (isVisible)
        {
            cubeLateFallVE.style.display = DisplayStyle.Flex;
        }
        else
        {
            cubeLateFallVE.style.display = DisplayStyle.None;
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
            if (powerUpButtons[i].style.opacity == 0.2f)
            {
                continue;
            }
            
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

        UpdateFirstPowerUpIconStatus(cubeCounter.IsCubeExistOnDifferentTypes());
    }

    private void UpdateFirstPowerUpIconStatus(bool shouldEnabled)
    {
        if (shouldEnabled)
        {
            firstPowerUpButton.style.opacity = 1.0f;
            firstPowerUpButton.SetEnabled(true);
        }
        else
        {
            firstPowerUpButton.style.opacity = 0.5f;
            firstPowerUpButton.SetEnabled(false);
        }
    }

    private void DisableThirdPowerUp()
    {
        thirdPowerUpButton.style.opacity = 0.2f;
        thirdPowerUpButton.SetEnabled(false);
    }

    public void SetDisplayFlex()
    {
        rootElement.style.display = DisplayStyle.Flex;
    }

    public void SetDisplayNone()
    {
        rootElement.style.display = DisplayStyle.None;
    }

    public bool IsOverUI()
    {
        // Already implemented another way, use another one
        return true;
    }

}
