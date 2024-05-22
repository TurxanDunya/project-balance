using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class GameUIController : MonoBehaviour
{
    [Header("GameOver UI")]
    [SerializeField] private GameObject gameOverUI;
    private VisualElement rootGameOver;

    [Header("Winner UI")]
    [SerializeField] private GameObject winnerUI;
    private VisualElement rootWinner;

    [Header("LevelStars UI")]
    [SerializeField] private GameObject levelStarsUI;
    private VisualElement rootLevelStars;
    private ProgressBar levelStarProgressBar;

    private AngleCalculator angleCalculator;

    void Start()
    {
        angleCalculator = GetComponent<AngleCalculator>();

        rootGameOver = gameOverUI.GetComponent<UIDocument>()
            .rootVisualElement.Q<VisualElement>("root_container");
        rootWinner = winnerUI.GetComponent<UIDocument>()
            .rootVisualElement.Q<VisualElement>("root_container");
        rootLevelStars = levelStarsUI.GetComponent<UIDocument>()
            .rootVisualElement.Q<VisualElement>("root_container");

        rootGameOver.Q<Button>("btn_home").clicked += () => GameOverHomeClick();
        rootWinner.Q<Button>("btn_next").clicked += () => GoNextLevel();
        levelStarProgressBar = rootLevelStars.Q<ProgressBar>("progress");
    }

    private void Update()
    {
        int platformAngle = angleCalculator.GetPlatformAngle();
        SetLevelStarsByPlatformAngle(platformAngle);
    }

    private void SetLevelStarsByPlatformAngle(int degree)
    {
        var progress = 90 - degree;
        levelStarProgressBar.value = progress;
    }

    void GameOverHomeClick() {
        NavigateScene(Constants.START_SCENE_INDEX);
    }

    void GoNextLevel()
    {
        var level = LevelManager.INSTANCE.levelManagment.currentLevel;
        if(level != null) NavigateSceneByName(level.name);
    }

    public void GameOverUIVisibility(bool visibility)
    {
        rootGameOver.visible = visibility;
    }

    public void WinnerUIVisibility(bool visibility)
    {
        rootWinner.visible = visibility;
    }

    public void ShouldActivateLevelStarUI(bool isActive)
    {
        levelStarsUI.SetActive(isActive);
    }

    public int GetLevelStar() {
        var progress = (int)levelStarProgressBar.value;
        var star = 0;
        switch (progress){
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

    void NavigateScene(int scene)
    {
        SceneManager.LoadScene(scene);
    }

    void NavigateSceneByName(string scene)
    {
        SceneManager.LoadScene(scene);
    }

}
