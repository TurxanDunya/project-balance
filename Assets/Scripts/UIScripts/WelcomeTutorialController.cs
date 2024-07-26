using UnityEngine;
using UnityEngine.UIElements;

public class WelcomeTutorialController : MonoBehaviour
{
    [SerializeField] private GameObject selfPrefab;

    private VisualElement rootElement;
    private Button gotItButton;
    private VisualElement moveFingerImageVE;

    void Start()
    {
        rootElement = GetComponent<UIDocument>().rootVisualElement.Q<VisualElement>("RootVE");
        moveFingerImageVE = rootElement.Q<VisualElement>("MoveFingerImageVE");
        gotItButton = moveFingerImageVE.Q<Button>("GotItButton");

        gotItButton.clicked += DisableHowToMoveCubeVE;
    }

    public void ShowMoveCubeTutorial()
    {
        rootElement.style.display = DisplayStyle.Flex;
    }

    private void DisableHowToMoveCubeVE()
    {
        rootElement.Remove(moveFingerImageVE);
        Destroy(selfPrefab);
    }

}
