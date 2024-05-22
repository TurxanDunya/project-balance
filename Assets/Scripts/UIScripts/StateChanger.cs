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

    public void ChangeStateToInGameUI()
    {
        Time.timeScale = 1;

        gameUI.SetActive(true);
        inGameUI.SetActive(true);
        pauseScreenUI.SetActive(true);
        gameOverUI.SetActive(true);
        winnerUI.SetActive(true);

        CheckWelcomeTutorial();

        homeScreenUI.SetActive(false);
    }

    public void ChangeStateToMainUI()
    {
        SceneManager.LoadScene(LevelNameConstants.START_LOAD_SCREEN);

        gameUI.SetActive(false);
        inGameUI.SetActive(false);
        pauseScreenUI.SetActive(false);
        gameOverUI.SetActive(false);
        winnerUI.SetActive(false);

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

        pauseScreenUI.SetActive(true);
    }

    public void ChangeStateToResume()
    {
        Time.timeScale = 1;

        pauseScreenUI.SetActive(false);
        gameOverUI.SetActive(false);
        winnerUI.SetActive(false);
        homeScreenUI.SetActive(false);

        inGameUI.SetActive(true);
        gameUI.SetActive(true);
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
