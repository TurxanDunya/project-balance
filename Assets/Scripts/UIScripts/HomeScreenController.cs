using UnityEngine;
using UnityEngine.UIElements;

public class HomeScreenController : MonoBehaviour
{
    [SerializeField] private StateChanger stateChanger;

    private VisualElement rootElement;
    private VisualElement topVE;
    private Button settingsButton;
    private Button tapToPlayButton;

    private bool isHomePageEnabled = true;

    private void Start()
    {
        rootElement = GetComponent<UIDocument>().rootVisualElement;

        topVE = rootElement.Q<VisualElement>("topVE");
        settingsButton = topVE.Q<Button>("Settings");

        tapToPlayButton = rootElement.Q<Button>("tapToPlayButton");

        // Will show settings pop up
        //settingsButton.clicked += () => ;
        tapToPlayButton.clicked += () => ChangeStateForInGameUI();
    }

    public bool IsOverUI(Vector2 touchPosition)
    {
        return isHomePageEnabled;
    }

    private void ChangeStateForInGameUI()
    {
        stateChanger.ChangeStateToInGameUI();
        isHomePageEnabled = false;
    }

}
