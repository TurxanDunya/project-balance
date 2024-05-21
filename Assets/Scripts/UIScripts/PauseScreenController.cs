using UnityEngine;
using UnityEngine.UIElements;

public class PauseScreenController : MonoBehaviour
{
    [SerializeField] private StateChanger stateChanger;

    private VisualElement rootElement;
    private VisualElement topVE;
    private VisualElement pausePopUpVE;

    private Button homeButton;

    void Start()
    {
        rootElement = GetComponent<UIDocument>()
            .rootVisualElement.Q<VisualElement>("RootVE");

        topVE = rootElement.Q<VisualElement>("topVE");
        homeButton = topVE.Q<Button>("Home");
        homeButton.clicked += () => ChangeStateToHomePage();

        pausePopUpVE = rootElement.Q<VisualElement>("PausePopUpVE");
    }

    public void ChangeStateToPausePage()
    {
        pausePopUpVE.style.display = DisplayStyle.Flex;
        homeButton.style.display = DisplayStyle.Flex;
        stateChanger.ChangeStateToPause();
    }

    private void ChangeStateToHomePage()
    {
        stateChanger.ChangeStateToMainUI();
    }

}
