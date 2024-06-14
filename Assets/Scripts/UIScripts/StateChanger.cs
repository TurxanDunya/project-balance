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
    [SerializeField] private GameObject welcomeTutorialUI;
    [SerializeField] private GameObject levelMenuUI;

    public void ChangeStateToInGameUI()
    {
        Time.timeScale = 1;

        gameUI.SetActive(true);
        inGameUI.SetActive(true);
        
        gameOverUI.SetActive(true);
        winnerUI.SetActive(true);

        CheckWelcomeTutorial();

        pauseScreenUI.SetActive(false);
        homeScreenUI.SetActive(false);
        levelMenuUI.SetActive(false);
    }

    public void ChangeStateToMainUI()
    {
        SceneManager.LoadScene(LevelNameConstants.START_LOAD_SCREEN);

        gameUI.SetActive(false);
        inGameUI.SetActive(false);
        pauseScreenUI.SetActive(false);
        gameOverUI.SetActive(false);
        winnerUI.SetActive(false);
        levelMenuUI.SetActive(false);

        homeScreenUI.SetActive(true);
    }

    public void ChangeStateToPause()
    {
        Time.timeScale = 0;

        gameUI.SetActive(false);
        inGameUI.SetActive(false);
        gameOverUI.SetActive(false);
        winnerUI.SetActive(false);
        homeScreenUI.SetActive(false);
        levelMenuUI.SetActive(false);

        pauseScreenUI.SetActive(true);
    }

    public void ChangeStateToResume()
    {
        Time.timeScale = 1;

        pauseScreenUI.SetActive(false);
        gameOverUI.SetActive(false);
        winnerUI.SetActive(false);
        homeScreenUI.SetActive(false);
        levelMenuUI.SetActive(false);

        inGameUI.SetActive(true);
        gameUI.SetActive(true);
    }

    public void ChangeStateToLevelMenu()
    {
        Time.timeScale = 0;

        inGameUI.SetActive(false);
        gameOverUI.SetActive(false);
        winnerUI.SetActive(false);
        homeScreenUI.SetActive(false);
        pauseScreenUI.SetActive(false);
        gameUI.SetActive(false);

        levelMenuUI.SetActive(true);
    }

    public void HideLevelMenu()
    {
        Time.timeScale = 1;

        gameOverUI.SetActive(false);
        winnerUI.SetActive(false);
        homeScreenUI.SetActive(false);
        pauseScreenUI.SetActive(false);

        inGameUI.SetActive(true);
        gameUI.SetActive(true);

        levelMenuUI.SetActive(false);
    }

    private void CheckWelcomeTutorial()
    {
        if (welcomeTutorialUI != null)
        {
            welcomeTutorialUI.SetActive(true);
            welcomeTutorialUI.GetComponent<WelcomeTutorialController>().MakeRootElementVisible();
        }
    }

}
