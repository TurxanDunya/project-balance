using UnityEngine;

public class UIButtonsTemplate : MonoBehaviour
{
    [SerializeField] private InGameUIController inGameUIController;
    [SerializeField] private GameUIController gameUIController;
    [SerializeField] private TutorialController tutorialController;
    [SerializeField] private HomeScreenController homeScreenController;
    [SerializeField] private LevelsSceneController levelsSceneController;
    [SerializeField] private PauseScreenController pauseScreenController;
    [SerializeField] private AboutUsController aboutUsController;
    [SerializeField] private SettingsUIController settingsUIController;

    public bool IsOverlappingAnyUI(Vector2 touchPosition)
    {
        if (inGameUIController && inGameUIController.IsOverUI(touchPosition))
        {
            return true;
        }

        if (tutorialController && tutorialController.IsOverUI())
        {
            return true;
        }

        if (gameUIController && gameUIController.IsOverUI())
        {
            return true;
        }

        if (homeScreenController && homeScreenController.IsOverUI())
        {
            return true;
        }

        if (levelsSceneController && levelsSceneController.IsOverUI())
        {
            return true;
        }

        if (pauseScreenController && pauseScreenController.IsOverUI())
        {
            return true;
        }

        if (aboutUsController && aboutUsController.IsOverUI())
        {
            return true;
        }

        if (settingsUIController && settingsUIController.IsOverUI())
        {
            return true;
        }

        return false;
    }
}
