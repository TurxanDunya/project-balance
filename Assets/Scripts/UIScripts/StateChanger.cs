using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StateChanger : MonoBehaviour
{
    [SerializeField] private GameObject gameUI;
    [SerializeField] private GameObject inGameUI;
    [SerializeField] private GameObject homeScreenUI;
    [SerializeField] private GameObject pauseScreenUI;
    [SerializeField] private GameObject gameOverUI;
    [SerializeField] private GameObject winnerUI;
    [SerializeField] private GameObject levelMenuUI;
    [SerializeField] private GameObject settingsUI;
    [SerializeField] private GameObject aboutUsUI;

    public static event Action CheckTutorialsStatus;

    public void ChangeStateToInGameUI()
    {
        Time.timeScale = 1;

        gameUI.SetActive(true);
        inGameUI.SetActive(true);
        
        gameOverUI.SetActive(true);
        winnerUI.SetActive(true);

        try
        {
            CheckTutorialsStatus?.Invoke();
        }
        catch (Exception)
        {
            // Just ignore error
        }

        pauseScreenUI.SetActive(false);
        homeScreenUI.SetActive(false);
        levelMenuUI.SetActive(false);
        settingsUI.SetActive(false);
        aboutUsUI.SetActive(false);
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

        gameUI.SetActive(false);
        gameOverUI.SetActive(false);
        winnerUI.SetActive(false);
        homeScreenUI.SetActive(false);
        levelMenuUI.SetActive(false);
        settingsUI.SetActive(false);
        aboutUsUI.SetActive(false);

        inGameUI.GetComponent<InGameUIController>().SetDisplayNone();
        pauseScreenUI.SetActive(true);
    }

    public void ChangeStateToResume()
    {
        Time.timeScale = 1;

        homeScreenUI.SetActive(false);
        levelMenuUI.SetActive(false);
        settingsUI.SetActive(false);
        aboutUsUI.SetActive(false);

        gameUI.SetActive(true);
        gameOverUI.SetActive(true);
        winnerUI.SetActive(true);

        pauseScreenUI.SetActive(false);
        inGameUI.GetComponent<InGameUIController>().SetDisplayFlex();
    }

    public void ChangeStateToLevelMenu()
    {
        Time.timeScale = 0;

        inGameUI.GetComponent<InGameUIController>().SetDisplayNone();
        gameOverUI.SetActive(false);
        winnerUI.SetActive(false);
        homeScreenUI.SetActive(false);
        pauseScreenUI.SetActive(false);
        gameUI.SetActive(false);
        settingsUI.SetActive(false);
        aboutUsUI.SetActive(false);

        levelMenuUI.SetActive(true);
    }

    public void HideLevelMenu()
    {
        Time.timeScale = 1;

        gameOverUI.SetActive(false);
        winnerUI.SetActive(false);
        homeScreenUI.SetActive(false);
        pauseScreenUI.SetActive(false);
        settingsUI.SetActive(false);
        aboutUsUI.SetActive(false);
        gameUI.SetActive(false);

        inGameUI.GetComponent<InGameUIController>().SetDisplayFlex();

        levelMenuUI.SetActive(false);
    }

    public void ChangeStateFromMainUIToSettingsUI()
    {
        gameUI.SetActive(false);
        inGameUI.SetActive(false);
        pauseScreenUI.SetActive(false);
        gameOverUI.SetActive(false);
        winnerUI.SetActive(false);
        levelMenuUI.SetActive(false);
        homeScreenUI.SetActive(false);
        aboutUsUI.SetActive(false);

        settingsUI.SetActive(true);
        settingsUI.GetComponent<SettingsUIController>().MakeBindings();
    }

    public void ChangeStateToMainUIWithoutLoadPage()
    {
        gameUI.SetActive(false);
        inGameUI.SetActive(false);
        pauseScreenUI.SetActive(false);
        gameOverUI.SetActive(false);
        winnerUI.SetActive(false);
        levelMenuUI.SetActive(false);
        settingsUI.SetActive(false);
        aboutUsUI.SetActive(false);

        homeScreenUI.SetActive(true);
        homeScreenUI.GetComponent<HomeScreenController>().MakeBindings();
    }

    public void ShowAboutUsUI()
    {
        gameUI.SetActive(false);
        inGameUI.SetActive(false);
        pauseScreenUI.SetActive(false);
        gameOverUI.SetActive(false);
        winnerUI.SetActive(false);
        levelMenuUI.SetActive(false);
        settingsUI.SetActive(false);
        homeScreenUI.SetActive(false);

        aboutUsUI.SetActive(true);
        aboutUsUI.GetComponent<AboutUsController>().MakeBindings();
    }

}
