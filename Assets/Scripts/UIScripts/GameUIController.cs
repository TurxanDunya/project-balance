using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class GameUIController : MonoBehaviour
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

    void Start()
    {
        angleCalculator = GetComponent<AngleCalculator>();

        ConfigureGameOverUIElements();
        ConfigureWinnerUIElements();
        ConfigureLevelStarUIElements();

        SafeArea.ApplySafeArea(rootGameOver);
        SafeArea.ApplySafeArea(rootWinner);
        SafeArea.ApplySafeArea(rootLevelStars);
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

    public bool IsGameWinUIIsVisible()
    {
        return rootWinner.style.display == DisplayStyle.Flex;
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

    public bool IsOverUI()
    {
        if(rootGameOver.style.display == DisplayStyle.Flex)
        {
            return true;
        }

        if(rootWinner.style.display == DisplayStyle.Flex)
        {
            return true;
        }

        return false;
    }

    private void ConfigureGameOverUIElements()
    {
        rootGameOver = gameOverUI.GetComponent<UIDocument>()
            .rootVisualElement.Q<VisualElement>("root_container");

        rootGameOver.Q<Button>("btn_home").clicked += () => GoHomePage();
        rootGameOver.Q<Button>("btn_replay").clicked += () => ReloadLevel();
        rootGameOver.Q<Button>("btn_levels").clicked += () => OpenLevelMenu();
    }

    private void ConfigureWinnerUIElements()
    {
        rootWinner = winnerUI.GetComponent<UIDocument>()
            .rootVisualElement.Q<VisualElement>("root_container");

        winnerVE = rootWinner.Q<VisualElement>("winner_VE");
        currentSecondLabel = rootWinner.Q<Label>("CurrentSecondLabel");

        rootWinner.Q<Button>("btn_home").clicked += () => GoHomePage();
        rootWinner.Q<Button>("btn_next").clicked += () => GoNextLevel();
        rootWinner.Q<Button>("btn_levels").clicked += () => OpenLevelMenu();
        rootWinner.Q<Button>("btn_replay").clicked += () => ReloadLevel();
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

    private void GoHomePage() {
        ReloadLevel();
        stateChanger.ChangeStateToMainUI();
    }

    private void ReloadLevel()
    {
        LevelManager.INSTANCE.levelManagment.levelList.lastPlayedLevelName =
            SceneManager.GetActiveScene().name;
        SceneManager.LoadScene(LevelNameConstants.START_LOAD_SCREEN);
    }

    private void OpenLevelMenu()
    {
        stateChanger.ChangeStateToLevelMenu();
    }

    void GoNextLevel()
    {
        string[] levelNameParts = SceneManager.GetActiveScene().name.Split(" ");
        int nextLevelNumber = int.Parse(levelNameParts[1]);
        string nextLevelName = levelNameParts[0] + " " + (++nextLevelNumber);

        LevelManager.INSTANCE.levelManagment.levelList.lastPlayedLevelName = nextLevelName;
        SceneManager.LoadScene(LevelNameConstants.START_LOAD_SCREEN);
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

}
