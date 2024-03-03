using UnityEngine;
using UnityEngine.UIElements;

public class PowerUpsController : MonoBehaviour
{
    [SerializeField] private PowerUps powerUps;

    private VisualElement menuRoot;

    void Start()
    {
        menuRoot = GetComponent<UIDocument>().rootVisualElement;
        Button firstPowerUp = menuRoot.Q<Button>("first_power_up");

        firstPowerUp.clicked += () => {
            Debug.Log("Do first power up job!");
        };
    }

}
