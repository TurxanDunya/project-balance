using UnityEngine;
using UnityEngine.UIElements;

public class AboutUsController : MonoBehaviour
{
    [SerializeField] private StateChanger stateChanger;

    private VisualElement rootElement;
    private VisualElement topVE;
    private Button homeButton;

    private void Start()
    {
        rootElement = GetComponent<UIDocument>()
            .rootVisualElement.Q<VisualElement>("rootVE");

        topVE = rootElement.Q<VisualElement>("top_ve");
        homeButton = topVE.Q<Button>("home_btn");

        homeButton.clicked += () => stateChanger.ChangeStateToMainUIWithoutLoadPage();
    }
}
