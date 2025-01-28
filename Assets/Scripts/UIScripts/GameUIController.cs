using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

enum RedirectAction { HOME, REPLAY, NEXTLEVEL };

public class GameUIController : MonoBehaviour, IControllerTemplate, AdsEventCallback
{
    private AngleCalculator angleCalculator;

    [SerializeField] private StateChanger stateChanger;

    [Header("Level star params")]
    [SerializeField] private float degreeMultiplier = 1;
    [SerializeField] private short progressForOneStar = 0;
    [SerializeField] private short progressForTwoStar = 35;
    [SerializeField] private short progressForThreeStar = 80;

    [Header("GameOver UI")]
    [SerializeField] private GameObject gameOverUI;
    private VisualElement rootGameOver;

    [Header("Winner UI")]
    [SerializeField] private GameObject winnerUI;
    private VisualElement rootWinner;
    private VisualElement winnerVE;
    private Label currentSecondLabel;

    [Header("Game UI")]
    [SerializeField] private GameObject gameUI;
    private VisualElement rootLevelStars;
    private ProgressBar levelStarProgressBar;
    private VisualElement starsVe;
    private VisualElement starsVe1;
    private VisualElement starsVe2;
    private VisualElement starsVe3;

    [Header("AdMob params")]
    [SerializeField] private int chanceToShowAd = 3;
    AdmobInterstitialAd interstitialAd;

    private RedirectAction redirectAction;

    void Start()
    {
        interstitialAd = new AdmobInterstitialAd();
        interstitialAd.SetAdsCallback(this);
        angleCalculator = GetComponent<AngleCalculator>();

        ConfigureGameOverUIElements();
        ConfigureWinnerUIElements();
        ConfigureLevelStarUIElements();

        SafeArea.ApplySafeArea(rootGameOver);
        SafeArea.ApplySafeArea(rootWinner);
        SafeArea.ApplySafeArea(rootLevelStars);

        if (!interstitialAd.CanShowInterstitialAd()) {
            interstitialAd.LoadAd();
        }
    }

    private void OnDisable()
    {
        rootGameOver.Q<Button>("btn_home").UnregisterCallback<PointerDownEvent>(GoHomePagePressed);
        rootGameOver.Q<Button>("btn_home").UnregisterCallback<PointerUpEvent>(GoHomePageReleased);

        rootGameOver.Q<Button>("btn_replay").UnregisterCallback<PointerDownEvent>(ReloadLevelPressed);
        rootGameOver.Q<Button>("btn_replay").UnregisterCallback<PointerUpEvent>(ReloadLevelReleased);

        rootGameOver.Q<Button>("btn_levels").UnregisterCallback<PointerDownEvent>(OpenLevelMenuPressed);
        rootGameOver.Q<Button>("btn_levels").UnregisterCallback<PointerUpEvent>(OpenLevelMenuReleased);

        rootWinner.Q<Button>("btn_home").UnregisterCallback<PointerDownEvent>(GoHomePagePressed);
        rootWinner.Q<Button>("btn_home").UnregisterCallback<PointerUpEvent>(GoHomePageReleased);

        rootWinner.Q<Button>("btn_next").UnregisterCallback<PointerDownEvent>(GoNextLevelPressed);
        rootWinner.Q<Button>("btn_next").UnregisterCallback<PointerUpEvent>(GoNextLevelReleased);

        rootWinner.Q<Button>("btn_levels").UnregisterCallback<PointerDownEvent>(OpenLevelMenuPressed);
        rootWinner.Q<Button>("btn_levels").UnregisterCallback<PointerUpEvent>(OpenLevelMenuReleased);

        rootWinner.Q<Button>("btn_replay").UnregisterCallback<PointerDownEvent>(ReloadLevelPressed);
        rootWinner.Q<Button>("btn_replay").UnregisterCallback<PointerUpEvent>(ReloadLevelReleased);
    }

    private void Update()
    {
        SetLevelStarsByPlatformAngle(angleCalculator.GetPlatformAngle());
    }

    public void GameOverUIVisibility(bool isVisible)
    {
        if(winnerVE.style.display == DisplayStyle.Flex)
        {
            return;
        }

        if(isVisible)
        {
            rootGameOver.style.display = DisplayStyle.Flex;
        }
        else
        {
            rootGameOver.style.display = DisplayStyle.None;
        }
    }

    public void WinnerUIVisibility(bool visibility)
    {
        if (rootGameOver.style.display == DisplayStyle.Flex)
        {
            return;
        }

        if(visibility)
        {
            rootWinner.style.display = DisplayStyle.Flex;
            winnerVE.style.display = DisplayStyle.Flex;
        }
        else
        {
            rootWinner.style.display = DisplayStyle.None;
            winnerVE.style.display = DisplayStyle.None;
        }
    }

    public void OnStandartAdsClose()
    {
        switch (redirectAction)
        {
            case RedirectAction.HOME:
                RedirectHomePage();
                break;
            case RedirectAction.REPLAY:
                RedirectHomePage();
                break;
            case RedirectAction.NEXTLEVEL:
                RedirectNextLevel();
                break;
        }

        RedirectHomePage();
    }

    public void OnRewardedAdsClose(double reward)
    {
    }

    public bool IsGameWinUIVisible()
    {
        return rootWinner.style.display == DisplayStyle.Flex
            && currentSecondLabel.style.display == DisplayStyle.Flex;
    }

    public void ShouldActivateLevelStarUI(bool isActive)
    {
        gameUI.SetActive(isActive);
    }

    public int GetLevelStar()
    {
        var star = 0;
        switch (progress)
        {
            case float progress when progress >= progressForThreeStar:
                star = 3;
                break;

            case float progress when progress > progressForTwoStar:
                star = 2;
                break;

            case float progress when progress > progressForOneStar:
                star = 1;
                break;
        }

        return star;
    }

    public void ShowAndUpdateWinnerTimeOnScreen(int currentSecond)
    {
        rootWinner.style.display = DisplayStyle.Flex;
        currentSecondLabel.style.display = DisplayStyle.Flex;
        currentSecondLabel.text = currentSecond.ToString();
    }

    public void HideWinnerTimeOnScreen()
    {
        rootWinner.style.display = DisplayStyle.None;
        winnerVE.style.display = DisplayStyle.None;
        currentSecondLabel.style.display = DisplayStyle.None;
    }

    private void ConfigureGameOverUIElements()
    {
        rootGameOver = gameOverUI.GetComponent<UIDocument>()
            .rootVisualElement.Q<VisualElement>("root_container");

        rootGameOver.Q<Button>("btn_home").RegisterCallback<PointerDownEvent>(GoHomePagePressed);
        rootGameOver.Q<Button>("btn_home").RegisterCallback<PointerUpEvent>(GoHomePageReleased);

        rootGameOver.Q<Button>("btn_replay").RegisterCallback<PointerDownEvent>(ReloadLevelPressed);
        rootGameOver.Q<Button>("btn_replay").RegisterCallback<PointerUpEvent>(ReloadLevelReleased);

        rootGameOver.Q<Button>("btn_levels").RegisterCallback<PointerDownEvent>(OpenLevelMenuPressed);
        rootGameOver.Q<Button>("btn_levels").RegisterCallback<PointerUpEvent>(OpenLevelMenuReleased);
    }

    private void ConfigureWinnerUIElements()
    {
        rootWinner = winnerUI.GetComponent<UIDocument>()
            .rootVisualElement.Q<VisualElement>("root_container");

        winnerVE = rootWinner.Q<VisualElement>("winner_VE");
        currentSecondLabel = rootWinner.Q<Label>("CurrentSecondLabel");

        rootWinner.Q<Button>("btn_home").RegisterCallback<PointerDownEvent>(GoHomePagePressed);
        rootWinner.Q<Button>("btn_home").RegisterCallback<PointerUpEvent>(GoHomePageReleased);

        rootWinner.Q<Button>("btn_next").RegisterCallback<PointerDownEvent>(GoNextLevelPressed);
        rootWinner.Q<Button>("btn_next").RegisterCallback<PointerUpEvent>(GoNextLevelReleased);

        rootWinner.Q<Button>("btn_levels").RegisterCallback<PointerDownEvent>(OpenLevelMenuPressed);
        rootWinner.Q<Button>("btn_levels").RegisterCallback<PointerUpEvent>(OpenLevelMenuReleased);

        rootWinner.Q<Button>("btn_replay").RegisterCallback<PointerDownEvent>(ReloadLevelPressed);
        rootWinner.Q<Button>("btn_replay").RegisterCallback<PointerUpEvent>(ReloadLevelReleased);
    }

    private void SetActionButtonsStatus(bool status)
    {
        rootGameOver.Q<Button>("btn_home").SetEnabled(status);
        rootGameOver.Q<Button>("btn_replay").SetEnabled(status);
        rootGameOver.Q<Button>("btn_levels").SetEnabled(status);

        rootWinner.Q<Button>("btn_home").SetEnabled(status);
        rootWinner.Q<Button>("btn_next").SetEnabled(status);
        rootWinner.Q<Button>("btn_levels").SetEnabled(status);
        rootWinner.Q<Button>("btn_replay").SetEnabled(status);
    }

    private void ConfigureLevelStarUIElements()
    {
        rootLevelStars = gameUI.GetComponent<UIDocument>()
            .rootVisualElement.Q<VisualElement>("root_container");

        levelStarProgressBar = rootLevelStars.Q<ProgressBar>("progress");
        starsVe = rootLevelStars.Q<VisualElement>("starsVE");

        starsVe1 = starsVe.Q<VisualElement>("starVE_1");
        starsVe2 = starsVe.Q<VisualElement>("starVE_2");
        starsVe3 = starsVe.Q<VisualElement>("starVE_3");
    }

    float progress;
    private void SetLevelStarsByPlatformAngle(float degree)
    {
        progress = 90 - degree * degreeMultiplier;
        levelStarProgressBar.value = progress;

        GetStarStatusByProgress(progress);
    }

    private void GoHomePagePressed(PointerDownEvent ev)
    {
        InputManager.isOverUI = true;
    }

    private void GoHomePageReleased(PointerUpEvent ev)
    {
        redirectAction = RedirectAction.HOME;

        if (ShouldShowAd())
        {
            if (interstitialAd.CanShowInterstitialAd())
            {
                SetActionButtonsStatus(false);
                interstitialAd.ShowAd();
            }
            else
            {
                RedirectHomePage();
            }
        }
        else
        {
            RedirectHomePage();
        }

        InputManager.isOverUI = false;
    }

    private void RedirectHomePage()
    {
        stateChanger.ChangeStateToHome();
    }

    private void ReloadLevelPressed(PointerDownEvent ev)
    {
        InputManager.isOverUI = true;
    }

    private void ReloadLevelReleased(PointerUpEvent ev)
    {
        redirectAction = RedirectAction.REPLAY;
        if (ShouldShowAd())
        {
            if (interstitialAd.CanShowInterstitialAd())
            {
                SetActionButtonsStatus(false);
                interstitialAd.ShowAd();
            }
            else
            {
                RedirectHomePage();
            }
        }
        else
        {
            RedirectHomePage();
        }

        InputManager.isOverUI = false;
    }

    private void OpenLevelMenuPressed(PointerDownEvent ev)
    {
        InputManager.isOverUI = true;
    }

    private void OpenLevelMenuReleased(PointerUpEvent ev)
    {
        stateChanger.ChangeStateToLevelMenu();
        InputManager.isOverUI = false;
    }

    private void GoNextLevelPressed(PointerDownEvent ev)
    {
        InputManager.isOverUI = true;
    }

    private void GoNextLevelReleased(PointerUpEvent ev)
    {
        redirectAction = RedirectAction.NEXTLEVEL;
        if (ShouldShowAd())
        {
            if (interstitialAd.CanShowInterstitialAd())
            {
                SetActionButtonsStatus(false);
                interstitialAd.ShowAd();
            }
            else
            {
                RedirectNextLevel();
            }
        }
        else
        {
            RedirectNextLevel();
        }

        InputManager.isOverUI = false;
    }

    private void RedirectNextLevel() {
        LevelManagment levelManagment = LevelManager.INSTANCE.levelManagment;
        Level nextLevel = levelManagment.FindNextLevel();
        levelManagment.currentLevel = nextLevel;
        levelManagment.levelList.lastPlayedLevelName = nextLevel.name;

        LevelManager.INSTANCE.levelManagment.levelList.lastPlayedLevelName = nextLevel.name;
        SceneManager.LoadScene(LevelNameConstants.START_LOAD_SCREEN);
    }


    private bool ShouldShowAd()
    {
        bool shouldShowAd = Random.Range(1, chanceToShowAd + 1) == 1;
        return shouldShowAd;
    }

    private void GetStarStatusByProgress(float progress)
    {
        if(progress >= progressForThreeStar)
        {
            starsVe1.style.opacity = 1.0f;
            starsVe2.style.opacity = 1.0f;
            starsVe3.style.opacity = 1.0f;
        }
        else if (progress >= progressForTwoStar)
        {
            starsVe1.style.opacity = 1.0f;
            starsVe2.style.opacity = 1.0f;
            starsVe3.style.opacity = 0.5f;
        }
        else if (progress >= progressForOneStar)
        {
            starsVe1.style.opacity = 1.0f;
            starsVe2.style.opacity = 0.5f;
            starsVe3.style.opacity = 0.5f;
        }
        else
        {
            starsVe1.style.opacity = 0.5f;
            starsVe2.style.opacity = 0.5f;
            starsVe3.style.opacity = 0.5f;
        }
    }

    public void SetGameOverDisplayFlex()
    {
        rootGameOver.style.display = DisplayStyle.Flex;
    }

    public void SetGameOverDisplayNone()
    {
        rootGameOver.style.display = DisplayStyle.None;
    }

    public void SetLevelStarDisplayFlex()
    {
        rootLevelStars.style.display = DisplayStyle.Flex;
    }

    public void SetLevelStarDisplayNone()
    {
        rootLevelStars.style.display = DisplayStyle.None;
    }

    public void SetWinnerDisplayFlex()
    {
        rootWinner.style.display = DisplayStyle.Flex;
    }

    public void SetWinnerDisplayNone()
    {
        rootWinner.style.display = DisplayStyle.None;
    }

    public void SetDisplayNone()
    {
        // Should be implemented when we refactor class
    }

    public void SetDisplayFlex()
    {
        // Should be implemented when we refactor class
    }

    private void OnDestroy()
    {
        interstitialAd.Destroy();
    }

}
