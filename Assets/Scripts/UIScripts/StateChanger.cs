using System;
using UnityEngine;
using UnityEngine.SceneManagement;

[DefaultExecutionOrder(1)]
public class StateChanger : MonoBehaviour
{
    [SerializeField] private GameUIController starUI;
    [SerializeField] private InGameUIController inGameUI;
    [SerializeField] private HomeScreenController homeScreenUI;
    [SerializeField] private PauseScreenController pauseScreenUI;
    [SerializeField] private GameUIController gameOverUI;
    [SerializeField] private GameUIController winnerUI;
    [SerializeField] private LevelsSceneController levelMenuUI;
    [SerializeField] private SettingsUIController settingsUI;
    [SerializeField] private AboutUsController aboutUsUI;
    [SerializeField] private CoinController coinUI;

    private void Start()
    {
        starUI.SetLevelStarDisplayNone();
        inGameUI.SetDisplayNone();
        pauseScreenUI.SetDisplayNone();
        gameOverUI.SetGameOverDisplayNone();
        winnerUI.SetWinnerDisplayNone();
        levelMenuUI.SetDisplayNone();
        settingsUI.SetDisplayNone();
        aboutUsUI.SetDisplayNone();

        coinUI.SetDisplayFlex();
        homeScreenUI.SetDisplayFlex();
    }

    public static event Action CheckTutorialsStatus;

    public void ChangeStateToInGameUI()
    {
        Time.timeScale = 1;

        starUI.SetLevelStarDisplayFlex();
        inGameUI.SetDisplayFlex();
        
        try
        {
            CheckTutorialsStatus?.Invoke();
        }
        catch (Exception)
        {
            // Just ignore error
        }

        gameOverUI.SetGameOverDisplayNone();
        winnerUI.SetWinnerDisplayNone();
        pauseScreenUI.SetDisplayNone();
        homeScreenUI.SetDisplayNone();
        levelMenuUI.SetDisplayNone();
        settingsUI.SetDisplayNone();
        aboutUsUI.SetDisplayNone();
    }

    public void ChangeStateToHome()
    {
        LevelManager.INSTANCE.levelManagment.levelList.lastPlayedLevelName =
            SceneManager.GetActiveScene().name;
        SceneManager.LoadScene(LevelNameConstants.START_LOAD_SCREEN);
    }

    public void ChangeStateToPause()
    {
        Time.timeScale = 0;

        starUI.SetLevelStarDisplayNone();
        gameOverUI.SetGameOverDisplayNone();
        winnerUI.SetWinnerDisplayNone();
        homeScreenUI.SetDisplayNone();
        levelMenuUI.SetDisplayNone();
        settingsUI.SetDisplayNone();
        aboutUsUI.SetDisplayNone();
        inGameUI.SetDisplayNone();

        pauseScreenUI.SetDisplayFlex();
    }

    public void ChangeStateToResume()
    {
        Time.timeScale = 1;

        homeScreenUI.SetDisplayNone();
        levelMenuUI.SetDisplayNone();
        settingsUI.SetDisplayNone();
        aboutUsUI.SetDisplayNone();
        pauseScreenUI.SetDisplayNone();
        gameOverUI.SetGameOverDisplayNone();
        winnerUI.SetGameOverDisplayNone();

        starUI.SetLevelStarDisplayFlex();
        inGameUI.SetDisplayFlex();
    }

    public void ChangeStateToLevelMenu()
    {
        Time.timeScale = 0;

        inGameUI.SetDisplayNone();
        gameOverUI.SetGameOverDisplayNone();
        winnerUI.SetWinnerDisplayNone();
        homeScreenUI.SetDisplayNone();
        pauseScreenUI.SetDisplayNone();
        starUI.SetLevelStarDisplayNone();
        settingsUI.SetDisplayNone();
        aboutUsUI.SetDisplayNone();

        levelMenuUI.SetDisplayFlex();
    }

    public void HideLevelMenu()
    {
        Time.timeScale = 1;

        gameOverUI.SetGameOverDisplayNone();
        winnerUI.SetWinnerDisplayNone();
        homeScreenUI.SetDisplayNone();
        pauseScreenUI.SetDisplayNone();
        settingsUI.SetDisplayNone();
        aboutUsUI.SetDisplayNone();
        starUI.SetLevelStarDisplayNone();
        levelMenuUI.SetDisplayNone();

        inGameUI.SetDisplayFlex();
    }

    public void ChangeStateFromMainUIToSettingsUI()
    {
        starUI.SetLevelStarDisplayNone();
        inGameUI.SetDisplayNone();
        pauseScreenUI.SetDisplayNone();
        gameOverUI.SetGameOverDisplayNone();
        winnerUI.SetGameOverDisplayNone();
        levelMenuUI.SetDisplayNone();
        homeScreenUI.SetDisplayNone();
        aboutUsUI.SetDisplayNone();

        settingsUI.SetDisplayFlex();
    }

    public void ChangeStateToMainUIWithoutLoadPage()
    {
        starUI.SetLevelStarDisplayNone();
        inGameUI.SetDisplayNone();
        pauseScreenUI.SetDisplayNone();
        gameOverUI.SetGameOverDisplayNone();
        winnerUI.SetWinnerDisplayNone();
        levelMenuUI.SetDisplayNone();
        settingsUI.SetDisplayNone();
        aboutUsUI.SetDisplayNone();

        homeScreenUI.SetDisplayFlex();
    }

    public void ShowAboutUsUI()
    {
        starUI.SetLevelStarDisplayNone();
        inGameUI.SetDisplayNone();
        pauseScreenUI.SetDisplayNone();
        gameOverUI.SetGameOverDisplayNone();
        winnerUI.SetWinnerDisplayNone();
        levelMenuUI.SetDisplayNone();
        settingsUI.SetDisplayNone();
        homeScreenUI.SetDisplayNone();

        aboutUsUI.SetDisplayFlex();
    }

}
