using UnityEngine;
using UnityEngine.UIElements;

public class WelcomeTutorialController : MonoBehaviour
{
    private static readonly ILogger logger = Debug.unityLogger;

    [SerializeField] private GameObject selfPrefab;

    private TutorialSaveSystem tutorialSaveSystem;

    private VisualElement rootElement;
    private Button gotItButton;
    private VisualElement moveFingerImageVE;

    void Start()
    {
        tutorialSaveSystem = GetComponent<TutorialSaveSystem>();

        rootElement = GetComponent<UIDocument>().rootVisualElement.Q<VisualElement>("RootVE");
        moveFingerImageVE = rootElement.Q<VisualElement>("MoveFingerImageVE");
        gotItButton = moveFingerImageVE.Q<Button>("GotItButton");

        gotItButton.clicked += DisableHowToMoveCubeVE;
    }

    public void ShowMoveCubeTutorial()
    {
        if(tutorialSaveSystem.GetIsWelcomeTutorialWatched())
        {
            logger.Log(LogType.Log, "Welcome tutorial already shown, no need to again!");
            DisableHowToMoveCubeVE();
            return;
        }

        rootElement.style.display = DisplayStyle.Flex;
        tutorialSaveSystem.SetWelcomeTutorialWatched();
    }

    private void DisableHowToMoveCubeVE()
    {
        rootElement.Remove(moveFingerImageVE);
        Destroy(selfPrefab);
    }

}
