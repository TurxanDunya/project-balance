using System.Collections;
using UnityEngine;
using UnityEngine.UIElements;

public class TutorialController : MonoBehaviour
{
    private static readonly ILogger logger = Debug.unityLogger;

    [SerializeField] private GameObject[] tutorials;

    private TutorialSaveSystem tutorialSaveSystem;

    private VisualElement rootElement;
    private Button continueBtn;

    private void OnEnable()
    {
        StateChanger.CheckTutorialsStatus += CheckStatuses;
    }

    private void OnDisable()
    {
        StateChanger.CheckTutorialsStatus += CheckStatuses;
    }

    void Start()
    {
        tutorialSaveSystem = GetComponent<TutorialSaveSystem>();
    }

    private void CheckStatuses()
    {
        if (tutorials == null || tutorials.Length == 0)
        {
            logger.Log(LogType.Log, "There is no any tutorial to show!");
            return;
        }

        StartCoroutine(CheckStatusesCoroutine());
    }

    private IEnumerator CheckStatusesCoroutine()
    {
        foreach (GameObject tutorial in tutorials)
        {
            rootElement = tutorial.GetComponent<UIDocument>().rootVisualElement.Q<VisualElement>("RootVE");
            continueBtn = rootElement.Q<Button>("continue_btn");

            CheckTutorialWatchedOrShow(tutorial);

            bool isBtnClicked = false;
            continueBtn.clicked += () =>
            {
                isBtnClicked = true;
                DismissUIPanel(tutorial);
            };

            yield return new WaitUntil(() => isBtnClicked);
            yield return new WaitForSeconds(3);
        }
    }

    private void CheckTutorialWatchedOrShow(GameObject tutorial)
    {
        if (tutorial.name == "WelcomeTutorialUI")
        {
            if (tutorialSaveSystem.GetIsWelcomeTutorialWatched())
            {
                logger.Log(LogType.Log, "WelcomeTutorialUI already shown, no need to again!");
                DismissUIPanel(tutorial);
            }
            else
            {
                rootElement.style.display = DisplayStyle.Flex;
                tutorialSaveSystem.SetWelcomeTutorialWatched();
            }
        }

        if (tutorial.name == "MeetWoodAndChanger")
        {
            if (tutorialSaveSystem.GetMeetWoodAndChangerTutorialWatched())
            {
                logger.Log(LogType.Log, "MeetWoodAndChanger already shown, no need to again!");
                DismissUIPanel(tutorial);
            }
            else
            {
                rootElement.style.display = DisplayStyle.Flex;
                tutorialSaveSystem.SetMeetWoodAndChangerTutorialWatched();
            }
        }

        if (tutorial.name == "MeetMetalAndMagnet")
        {
            if (tutorialSaveSystem.GetMeetMetalAndMagnetTutorialWatched())
            {
                logger.Log(LogType.Log, "MeetMetalAndMagnet already shown, no need to again!");
                DismissUIPanel(tutorial);
            }
            else
            {
                rootElement.style.display = DisplayStyle.Flex;
                tutorialSaveSystem.SetMeetMetalAndMagnetTutorialWatched();
            }
        }
    }

    private void DismissUIPanel(GameObject tutorial)
    {
        rootElement.style.display = DisplayStyle.None;
        Destroy(tutorial);
    }

}
