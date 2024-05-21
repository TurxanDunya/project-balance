using UnityEngine;

public class StateChanger : MonoBehaviour
{
    [SerializeField] private GameObject gameUI;
    [SerializeField] private GameObject inGameUI;
    [SerializeField] private GameObject homeScreenUI;
    [SerializeField] private GameObject pauseScreenUI;
    [SerializeField] private GameObject gameOverUI;
    [SerializeField] private GameObject winnerUI;
    [SerializeField] private GameObject levelStar;
    [SerializeField] private GameObject welcomeTutorialUI;

    public void ChangeStateToInGameUI()
    {
        gameUI.SetActive(true);
        inGameUI.SetActive(true);
        pauseScreenUI.SetActive(true);
        gameOverUI.SetActive(true);
        winnerUI.SetActive(true);
        levelStar.SetActive(true);

        CheckWelcomeTutorial();

        homeScreenUI.SetActive(false);
    }

    public void ChangeStateToMainUI()
    {
        gameUI.SetActive(false);
        inGameUI.SetActive(false);
        pauseScreenUI.SetActive(false);
        gameOverUI.SetActive(false);
        winnerUI.SetActive(false);
        levelStar.SetActive(false);

        homeScreenUI.SetActive(true);
    }

    public void ChangeStateToPause()
    {
        gameUI.SetActive(false);
        inGameUI.SetActive(false);
        gameOverUI.SetActive(false);
        winnerUI.SetActive(false);
        levelStar.SetActive(false);
        homeScreenUI.SetActive(false);

        pauseScreenUI.SetActive(true);
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
