using UnityEngine;

public class UIButtonsTemplate : MonoBehaviour
{
    [SerializeField] private InGameUIController inGameUIController;
    [SerializeField] private WelcomeTutorialController welcomeTutorialController;
    [SerializeField] private HomeScreenController homeScreenController;

    public bool IsOverlappingAnyUI(Vector2 touchPosition)
    {
        if (inGameUIController != null &&
            inGameUIController.isActiveAndEnabled &&
            inGameUIController.IsOverUI(touchPosition))
        {
            return true;
        }

        if(welcomeTutorialController != null &&
            welcomeTutorialController.isActiveAndEnabled)
        {
            return true;
        }

        if(homeScreenController != null &&
            homeScreenController.isActiveAndEnabled)
        {
            return true;
        }

        return false;
    }
}
