using UnityEngine;

public class UIButtonsTemplate : MonoBehaviour
{
    [SerializeField] private InGameUIController inGameUIController;
    [SerializeField] private GameUIController gameUIController;
    [SerializeField] private WelcomeTutorialController welcomeTutorialController;
    [SerializeField] private HomeScreenController homeScreenController;
    [SerializeField] private LevelsSceneController levelsSceneController;
    [SerializeField] private PauseScreenController pauseScreenController;

    public bool IsOverlappingAnyUI(Vector2 touchPosition)
    {
        if (inGameUIController &&
            inGameUIController.isActiveAndEnabled &&
            inGameUIController.IsOverUI(touchPosition))
        {
            return true;
        }

        if (welcomeTutorialController &&
            welcomeTutorialController.isActiveAndEnabled)
        {
            return true;
        }

        if (homeScreenController &&
            homeScreenController.isActiveAndEnabled)
        {
            return true;
        }

        if (gameUIController &&
            gameUIController.isActiveAndEnabled &&
            gameUIController.IsOverUI())
        {
            return true;
        }

        if (levelsSceneController &&
            levelsSceneController.isActiveAndEnabled)
        {
            return true;
        }

        if (pauseScreenController &&
            pauseScreenController.isActiveAndEnabled)
        {
            return true;
        }

        return false;
    }
}
