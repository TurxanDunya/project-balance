using UnityEngine;
using UnityEngine.UIElements;

public class HomeScreenController : MonoBehaviour
{
    [SerializeField] private StateChanger stateChanger;

    private VisualElement rootElement;
    private VisualElement topVE;
    private VisualElement leftSideVE;
    private VisualElement rightSideVE;
    private Button settingsButton;
    private Button tapToPlayButton;
    private Button aboutUsButton;

    private bool isHomePageEnabled = true;

    private void Start()
    {
        rootElement = GetComponent<UIDocument>().rootVisualElement;

        topVE = rootElement.Q<VisualElement>("topVE");
        leftSideVE = topVE.Q<VisualElement>("left_side_ve");
        settingsButton = leftSideVE.Q<Button>("settings_btn");

        rightSideVE = topVE.Q<VisualElement>("right_side_ve");
        aboutUsButton = rightSideVE.Q<Button>("about_us_btn");

        tapToPlayButton = rootElement.Q<Button>("tapToPlayButton");

        settingsButton.clicked += () => ShowSettingsUI();
        tapToPlayButton.clicked += () => ChangeStateForInGameUI();
        aboutUsButton.clicked += () => ShowAboutUsUI();
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

    private void ShowSettingsUI()
    {
        stateChanger.ChangeStateFromMainUIToSettingsUI();
    }

    private void ShowAboutUsUI()
    {
        stateChanger.ShowAboutUsUI();
    }

}
