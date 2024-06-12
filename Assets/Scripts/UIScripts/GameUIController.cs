using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class GameUIController : MonoBehaviour
{
    private AngleCalculator angleCalculator;

    [SerializeField] private StateChanger stateChanger;

    [Header("GameOver UI")]
    [SerializeField] private GameObject gameOverUI;
    private VisualElement rootGameOver;
    private VisualElement gameOverVE;
    private VisualElement gameOverButtonLineVE;

    [Header("Winner UI")]
    [SerializeField] private GameObject winnerUI;
    private VisualElement rootWinner;
    private VisualElement winnerVE;
    private Label currentSecondLabel;
    private VisualElement winnerButtonLineVE;

    [Header("Game UI")]
    [SerializeField] private GameObject gameUI;
    private VisualElement rootLevelStars;
    private ProgressBar levelStarProgressBar;

    void Start()
    {
        angleCalculator = GetComponent<AngleCalculator>();

        ConfigureGameOverUIElements();
        ConfigureWinnerUIElements();
        ConfigureLevelStarUIElements();
    }

    private void Update()
    {
        int platformAngle = angleCalculator.GetPlatformAngle();
        SetLevelStarsByPlatformAngle(platformAngle);
    }

    public void GameOverUIVisibility(bool isVisible)
    {
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
        rootWinner.visible = visibility;
        winnerVE.visible = visibility;
    }

    public void ShouldActivateLevelStarUI(bool isActive)
    {
        gameUI.SetActive(isActive);
    }

    public int GetLevelStar()
    {
        var progress = (int)levelStarProgressBar.value;
        var star = 0;
        switch (progress)
        {
            case int n when (n > 80 && n <= 90):
                star = 3;
                break;

            case int n when (n > 60 && n <= 80):
                star = 2;
                break;

            case int n when (n > 0 && n <= 60):
                star = 1;
                break;
        }

        return star;
    }

    public void ShowAndUpdateWinnerTimeOnScreen(int currentSecond)
    {
        currentSecondLabel.visible = true;
        currentSecondLabel.text = currentSecond.ToString();
    }

    public void HideWinnerTimeOnScreen()
    {
        winnerVE.visible = false;
        currentSecondLabel.visible = false;
    }

    private void ConfigureGameOverUIElements()
    {
        rootGameOver = gameOverUI.GetComponent<UIDocument>()
            .rootVisualElement.Q<VisualElement>("root_container");

        gameOverVE = rootGameOver.Q<VisualElement>("game_over");
        gameOverButtonLineVE = gameOverVE.Q<VisualElement>("button_line_ve");

        gameOverButtonLineVE.Q<Button>("btn_home").clicked += () => GoHomePage();
        gameOverButtonLineVE.Q<Button>("btn_replay").clicked += () => ReloadLevel();
        gameOverButtonLineVE.Q<Button>("btn_levels").clicked += () => OpenLevelMenu();
    }

    private void ConfigureWinnerUIElements()
    {
        rootWinner = winnerUI.GetComponent<UIDocument>()
            .rootVisualElement.Q<VisualElement>("root_container");

        winnerVE = rootWinner.Q<VisualElement>("winner_VE");
        currentSecondLabel = rootWinner.Q<Label>("CurrentSecondLabel");
        winnerButtonLineVE = winnerVE.Q<VisualElement>("button_line_ve");

        winnerButtonLineVE.Q<Button>("btn_home").clicked += () => GoHomePage();
        winnerButtonLineVE.Q<Button>("btn_next").clicked += () => GoNextLevel();
        winnerButtonLineVE.Q<Button>("btn_levels").clicked += () => OpenLevelMenu();
    }

    private void ConfigureLevelStarUIElements()
    {
        rootLevelStars = gameUI.GetComponent<UIDocument>()
            .rootVisualElement.Q<VisualElement>("root_container");

        levelStarProgressBar = rootLevelStars.Q<ProgressBar>("progress");
    }

    private void SetLevelStarsByPlatformAngle(int degree)
    {
        var progress = 90 - degree;
        levelStarProgressBar.value = progress;
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

}
