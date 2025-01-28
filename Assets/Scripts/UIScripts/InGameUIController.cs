using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.SceneManagement;

public class InGameUIController : MonoBehaviour, IControllerTemplate
{
    [Header("Event Channels")]
    [SerializeField] private CameraMovementEventChannelSO cameraMovementEventChannelSO;

    private InputManager inputManager;
    [SerializeField] private StateChanger stateChanger;

    [SerializeField] private PowerUps powerUps;
    [SerializeField] private CubeSpawnManagement cubeSpawnManagement;
    [SerializeField] private TimeLevelFinish timeLevelFinish;
    [SerializeField] private CoinManager coinManager;

    private CubeCounter cubeCounter;
    private UIElementEnabler uiElementEnabler;

    private VisualElement rootElement;

    private VisualElement powerUpsVE;
    private VisualElement cancelVE;
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
    private VisualElement rockVE;
    private Label rockCountLabel;

    // middle pane icons
    private Button moveCameraAcrossBtn;
    private Button moveCameraLeftBtn;
    private Button moveCameraRightBtn;

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
        inputManager = gameObject.AddComponent<InputManager>();
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

        inputManager.OnPerformedTouch += HidePowerUpsAndShowCancel;
        inputManager.OnEndTouch += EndTouch;
    }

    private void Start()
    {
        rootElement = GetComponent<UIDocument>()
            .rootVisualElement.Q<VisualElement>("rootVE");

        powerUpsVE = rootElement.Q<VisualElement>("power_ups");
        cancelVE = rootElement.Q<VisualElement>("cancel_ve");
        firstPowerUpButton = powerUpsVE.Q<Button>("first_power_up");
        secondPowerUpButton = powerUpsVE.Q<Button>("second_power_up");
        thirdPowerUpButton = powerUpsVE.Q<Button>("third_power_up");
        fourthPowerUpButton = powerUpsVE.Q<Button>("fourth_power_up");

        topVE = rootElement.Q<VisualElement>("topVE");
        pauseButton = topVE.Q<Button>("Pause");
        levelsButton = topVE.Q<Button>("Levels");
        levelsButton.text = SceneManager.GetActiveScene().name;

        // left pane
        leftCubesCountVE = rootElement.Q<VisualElement>("left_cubes_count_VE");
        woodVE = leftCubesCountVE.Q<VisualElement>("wood_VE");
        woodCountLabel = woodVE.Q<Label>("wood_count_lbl");
        metalVE = leftCubesCountVE.Q<VisualElement>("metal_VE");
        metalCountLabel = metalVE.Q<Label>("metal_count_lbl");
        iceVE = leftCubesCountVE.Q<VisualElement>("ice_VE");
        iceCountLabel = iceVE.Q<Label>("ice_count_lbl");
        rockVE = leftCubesCountVE.Q<VisualElement>("rock_VE");
        rockCountLabel = rockVE.Q<Label>("rock_count_lbl");

        // middle pane
        moveCameraAcrossBtn = rootElement.Q<Button>("move_camera_across_btn");
        moveCameraLeftBtn = rootElement.Q<Button>("move_camera_left_btn");
        moveCameraRightBtn = rootElement.Q<Button>("move_camera_right_btn");

        // right pane
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
        HideCancelButtonAndShowPowerUps();
        UpdatePowerUpIconStatusesByCoinCount(coinManager.CoinCount);

        UpdateCubeCounts(
            cubeCounter.WoodCount,
            cubeCounter.MetalCount,
            cubeCounter.IceCount,
            cubeCounter.RockCount);

        UpdateFirstPowerUpIconStatus(cubeCounter.IsCubeExistOnDifferentTypes());

        SafeArea.ApplySafeArea(rootElement);
    }

    private void BindEventsWithFunctions()
    {
        firstPowerUpButton.RegisterCallback<PointerDownEvent>(FirstPowerUpPressed, TrickleDown.TrickleDown);
        firstPowerUpButton.RegisterCallback<PointerUpEvent>(FirstPowerUpReleased);

        secondPowerUpButton.RegisterCallback<PointerDownEvent>(SecondPowerUpPressed, TrickleDown.TrickleDown);
        secondPowerUpButton.RegisterCallback<PointerUpEvent>(SecondPowerUpReleased);

        thirdPowerUpButton.RegisterCallback<PointerDownEvent>(ThirdPowerUpPressed, TrickleDown.TrickleDown);
        thirdPowerUpButton.RegisterCallback<PointerUpEvent>(ThirdPowerUpReleased, TrickleDown.TrickleDown);

        fourthPowerUpButton.RegisterCallback<PointerDownEvent>(FourthPowerUpPressed, TrickleDown.TrickleDown);
        fourthPowerUpButton.RegisterCallback<PointerUpEvent>(FourthPowerUpReleased);

        cancelVE.RegisterCallback<PointerEnterEvent>(EnterCancelVE);
        cancelVE.RegisterCallback<PointerUpEvent>(ExecuteCancel);

        pauseButton.RegisterCallback<PointerDownEvent>(PauseGameEnter, TrickleDown.TrickleDown);
        pauseButton.RegisterCallback<PointerUpEvent>(PauseGameLeave);

        levelsButton.RegisterCallback<PointerDownEvent>(ShowLevelsEnter, TrickleDown.TrickleDown);
        levelsButton.RegisterCallback<PointerUpEvent>(ShowLevelsLeave);

        moveCameraAcrossBtn.RegisterCallback<PointerDownEvent>(MoveCameraAcrossEnter, TrickleDown.TrickleDown);
        moveCameraAcrossBtn.RegisterCallback<PointerUpEvent>(MoveCameraAcrossLeave);

        moveCameraLeftBtn.RegisterCallback<PointerDownEvent>(MoveCameraLeftEnter, TrickleDown.TrickleDown);
        moveCameraLeftBtn.RegisterCallback<PointerUpEvent>(MoveCameraLeftLeave);

        moveCameraRightBtn.RegisterCallback<PointerDownEvent>(MoveCameraRightEnter, TrickleDown.TrickleDown);
        moveCameraRightBtn.RegisterCallback<PointerUpEvent>(MoveCameraRightLeave);
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

        firstPowerUpButton.UnregisterCallback<PointerDownEvent>(FirstPowerUpPressed);
        firstPowerUpButton.UnregisterCallback<PointerUpEvent>(FirstPowerUpReleased);

        secondPowerUpButton.UnregisterCallback<PointerDownEvent>(SecondPowerUpPressed);
        secondPowerUpButton.UnregisterCallback<PointerUpEvent>(SecondPowerUpReleased);

        thirdPowerUpButton.UnregisterCallback<PointerDownEvent>(ThirdPowerUpPressed);
        thirdPowerUpButton.UnregisterCallback<PointerUpEvent>(ThirdPowerUpReleased);

        fourthPowerUpButton.UnregisterCallback<PointerDownEvent>(FourthPowerUpPressed);
        fourthPowerUpButton.UnregisterCallback<PointerUpEvent>(FourthPowerUpReleased);

        cancelVE.UnregisterCallback<PointerEnterEvent>(EnterCancelVE);
        cancelVE.UnregisterCallback<PointerUpEvent>(ExecuteCancel);

        inputManager.OnPerformedTouch -= HidePowerUpsAndShowCancel;
        inputManager.OnEndTouch -= EndTouch;

        pauseButton.UnregisterCallback<PointerDownEvent>(PauseGameEnter);
        pauseButton.UnregisterCallback<PointerUpEvent>(PauseGameLeave);

        levelsButton.UnregisterCallback<PointerDownEvent>(ShowLevelsEnter);
        levelsButton.UnregisterCallback<PointerUpEvent>(ShowLevelsLeave);

        moveCameraAcrossBtn.UnregisterCallback<PointerDownEvent>(MoveCameraAcrossEnter);
        moveCameraAcrossBtn.UnregisterCallback<PointerUpEvent>(MoveCameraAcrossLeave);

        moveCameraLeftBtn.UnregisterCallback<PointerDownEvent>(MoveCameraLeftEnter);
        moveCameraLeftBtn.UnregisterCallback<PointerUpEvent>(MoveCameraLeftLeave);

        moveCameraRightBtn.UnregisterCallback<PointerDownEvent>(MoveCameraRightEnter);
        moveCameraRightBtn.UnregisterCallback<PointerUpEvent>(MoveCameraRightLeave);
    }

    private void EnterCancelVE(PointerEnterEvent ev)
    {
        InputManager.isOverUI = true;
    }

    private void ExecuteCancel(PointerUpEvent ev)
    {
        InputManager.isOverUI = false;
        cubeSpawnManagement.ResetCurrentMoveableObjectPosition();
        HideCancelButtonAndShowPowerUps();
    }

    private void EndTouch()
    {
        InputManager.isOverUI = false;
        HideCancelButtonAndShowPowerUps();
    }

    private void HideCancelButtonAndShowPowerUps()
    {
        ShouldShowCancel(false);
        ShouldShowPowerups(true);
    }

    private void HidePowerUpsAndShowCancel(Vector2 delta)
    {
        ShouldShowCancel(true);
        ShouldShowPowerups(false);
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

    private void MoveCameraAcrossEnter(PointerDownEvent ev)
    {
        InputManager.isOverUI = true;
        cameraMovementEventChannelSO.RaiseEvent(CameraPosition.Direction.ACROSS);
    }

    private void MoveCameraAcrossLeave(PointerUpEvent ev)
    {
        InputManager.isOverUI = false;
    }

    private void MoveCameraLeftEnter(PointerDownEvent ev)
    {
        InputManager.isOverUI = true;
        cameraMovementEventChannelSO.RaiseEvent(CameraPosition.Direction.LEFT);
    }

    private void MoveCameraLeftLeave(PointerUpEvent ev)
    {
        InputManager.isOverUI = false;
    }

    private void MoveCameraRightEnter(PointerDownEvent ev)
    {
        InputManager.isOverUI = true;
        cameraMovementEventChannelSO.RaiseEvent(CameraPosition.Direction.RIGHT);
    }

    private void MoveCameraRightLeave(PointerUpEvent ev)
    {
        InputManager.isOverUI = false;
    }

    private void FirstPowerUpPressed(PointerDownEvent ev)
    {
        InputManager.isOverUI = true;
    }

    private void FirstPowerUpReleased(PointerUpEvent ev)
    {
        InputManager.isOverUI = false;

        if (coinManager.IsCoinEnough(PowerUpPriceConstants.CHANGE_CUBE))
        {
            bool isSuccess = cubeSpawnManagement.ReplaceCube();
            if (isSuccess)
            {
                coinManager.SubtractCoin(PowerUpPriceConstants.CHANGE_CUBE);
            }
        }
    }

    private void SecondPowerUpPressed(PointerDownEvent ev)
    {
        InputManager.isOverUI = true;

        if(coinManager.IsCoinEnough(PowerUpPriceConstants.ADD_TIME))
        {
            timeLevelFinish.IncreaseFinishTime(10);
            coinManager.SubtractCoin(PowerUpPriceConstants.ADD_TIME);
        }
    }

    private void SecondPowerUpReleased(PointerUpEvent ev)
    {
        InputManager.isOverUI = false;
    }

    private void ThirdPowerUpPressed(PointerDownEvent ev)
    {
        InputManager.isOverUI = true;
    }

    private void ThirdPowerUpReleased(PointerUpEvent ev)
    {
        InputManager.isOverUI = false;

        if (!coinManager.IsCoinEnough(PowerUpPriceConstants.MAGNET)
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

    private void FourthPowerUpPressed(PointerDownEvent ev)
    {
        InputManager.isOverUI = true;
    }

    private void FourthPowerUpReleased(PointerUpEvent ev)
    {
        InputManager.isOverUI = false;

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
        ShouldShowRockCounter(uiElementEnabler.isRockEnabled);

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

    private void ShouldShowRockCounter(bool isVisible)
    {
        if (isVisible)
        {
            rockVE.style.display = DisplayStyle.Flex;
        }
        else
        {
            rockVE.style.display = DisplayStyle.None;
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

    private void ShouldShowCancel(bool isVisible)
    {
        if (isVisible)
        {
            cancelVE.style.display = DisplayStyle.Flex;
        }
        else
        {
            cancelVE.style.display = DisplayStyle.None;
        }
    }

    private void ShouldShowPowerups(bool isVisible)
    {
        if (isVisible)
        {
            powerUpsVE.style.display = DisplayStyle.Flex;
        }
        else
        {
            powerUpsVE.style.display = DisplayStyle.None;
        }
    }

    private void PauseGameEnter(PointerDownEvent PointerDownEvent)
    {
        InputManager.isOverUI = true;
    }

    private void PauseGameLeave(PointerUpEvent PointerUpEvent)
    {
        InputManager.isOverUI = false;
        stateChanger.ChangeStateToPause();
    }

    private void ShowLevelsEnter(PointerDownEvent PointerDownEvent)
    {
        InputManager.isOverUI = true;
    }

    private void ShowLevelsLeave(PointerUpEvent PointerUpEvent)
    {
        stateChanger.ChangeStateToLevelMenu();
    }

    private void UpdateCubeCounts(int woodCount, int metalCount, int iceCount, int rockCount)
    {
        woodCountLabel.text = woodCount.ToString();
        metalCountLabel.text = metalCount.ToString();
        iceCountLabel.text = iceCount.ToString();
        rockCountLabel.text = rockCount.ToString();
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
                }
                return;
            }
            else
            {
                powerUpButtons[i].style.opacity = 1.0f;
            }
        }

        UpdateFirstPowerUpIconStatus(cubeCounter.IsCubeExistOnDifferentTypes());
    }

    private void UpdateFirstPowerUpIconStatus(bool shouldEnabled)
    {
        if (shouldEnabled)
        {
            firstPowerUpButton.style.opacity = 1.0f;
        }
        else
        {
            firstPowerUpButton.style.opacity = 0.5f;
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

}
