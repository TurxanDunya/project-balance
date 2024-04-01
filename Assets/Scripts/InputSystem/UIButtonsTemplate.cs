using UnityEngine;

public class UIButtonsTemplate : MonoBehaviour
{
    private PowerUpsController powerUpsController;

    private void Awake()
    {
        powerUpsController = FindObjectOfType<PowerUpsController>();
    }

    public bool IsOverlappingAnyUI(Vector2 touchPosition)
    {
        if(powerUpsController.IsOverUI(touchPosition))
        {
            return true;
        }

        return false;
    }
}
