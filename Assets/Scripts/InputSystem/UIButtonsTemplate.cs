using UnityEngine;

public class UIButtonsTemplate : MonoBehaviour
{
    private PowerUpsController powerUpsController;
    private WelcomeTutorialController welcomeTutorialController;

    private void Awake()
    {
        powerUpsController = FindObjectOfType<PowerUpsController>();
        welcomeTutorialController = FindObjectOfType<WelcomeTutorialController>();
    }

    public bool IsOverlappingAnyUI(Vector2 touchPosition)
    {
        if(powerUpsController.IsOverUI(touchPosition))
        {
            return true;
        }

        if(welcomeTutorialController != null &&
            welcomeTutorialController.IsOverUI(touchPosition))
        {
            return true;
        }

        return false;
    }
}
