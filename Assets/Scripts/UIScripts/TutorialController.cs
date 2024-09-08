using System.Collections;
using UnityEngine;
using UnityEngine.UIElements;

public class TutorialController : MonoBehaviour
{
    private static readonly ILogger logger = Debug.unityLogger;

    [SerializeField] private GameObject[] tutorials;
    [SerializeField] private GameObject levelSpecificObjectSelf;

    private TutorialSaveSystem tutorialSaveSystem;

    private VisualElement rootElement;
    private Button continueBtn;

    private void OnEnable()
    {
        tutorialSaveSystem = GetComponent<TutorialSaveSystem>();

        CheckStatuses();
        StateChanger.CheckTutorialsStatus += CheckStatuses;
    }

    private void OnDisable()
    {
        StateChanger.CheckTutorialsStatus += CheckStatuses;
    }

    private void CheckStatuses()
    {
        if (tutorials == null || tutorials.Length == 0)
        {
            logger.Log(LogType.Log, "There is no any tutorial to show!");
            Destroy(levelSpecificObjectSelf);
            return;
        }

        StartCoroutine(CheckStatusesCoroutine());
    }

    private IEnumerator CheckStatusesCoroutine()
    {
        int tutorialCountOnLevel = tutorials.Length;
        int currentTutorialNumber = 0;

        foreach (GameObject tutorial in tutorials)
        {
            currentTutorialNumber++;

            if (CheckTutorialWatchedOrShow(tutorial))
            {
                continue;
            }

            continueBtn = rootElement.Q<Button>("continue_btn");

            bool isBtnClicked = false;
            continueBtn.clicked += () =>
            {
                isBtnClicked = true;
                DismissUIPanel(tutorial);
            };

            yield return new WaitUntil(() => isBtnClicked);

            if (currentTutorialNumber < tutorialCountOnLevel)
            {
                yield return new WaitForSeconds(3);
            }
        }

        Destroy(levelSpecificObjectSelf);
    }

    private bool CheckTutorialWatchedOrShow(GameObject tutorial)
    {
        if (!tutorial)
        {
            return false;
        }    

        if (tutorial.name == "WelcomeTutorialUI")
        {
            if (tutorialSaveSystem.GetIsWelcomeTutorialWatched())
            {
                logger.Log(LogType.Log, "WelcomeTutorialUI already shown, no need to again!");
                DismissUIPanel(tutorial);
                return true;
            }
            else
            {
                tutorial.SetActive(true);
                rootElement = tutorial.GetComponent<UIDocument>().rootVisualElement.Q<VisualElement>("RootVE");
                rootElement.style.display = DisplayStyle.Flex;
                tutorialSaveSystem.SetWelcomeTutorialWatched();
                return false;
            }
        }

        if (tutorial.name == "MeetWoodAndChanger")
        {
            if (tutorialSaveSystem.GetMeetWoodAndChangerTutorialWatched())
            {
                logger.Log(LogType.Log, "MeetWoodAndChanger already shown, no need to again!");
                DismissUIPanel(tutorial);
                return true;
            }
            else
            {
                tutorial.SetActive(true);
                rootElement = tutorial.GetComponent<UIDocument>().rootVisualElement.Q<VisualElement>("RootVE");
                rootElement.style.display = DisplayStyle.Flex;
                tutorialSaveSystem.SetMeetWoodAndChangerTutorialWatched();
                return false;
            }
        }

        if (tutorial.name == "MeetMetalAndMagnet")
        {
            if (tutorialSaveSystem.GetMeetMetalAndMagnetTutorialWatched())
            {
                logger.Log(LogType.Log, "MeetMetalAndMagnet already shown, no need to again!");
                DismissUIPanel(tutorial);
                return true;
            }
            else
            {
                tutorial.SetActive(true);
                rootElement = tutorial.GetComponent<UIDocument>().rootVisualElement.Q<VisualElement>("RootVE");
                rootElement.style.display = DisplayStyle.Flex;
                tutorialSaveSystem.SetMeetMetalAndMagnetTutorialWatched();
                return false;
            }
        }

        return false;
    }

    private void DismissUIPanel(GameObject tutorial)
    {
        if (rootElement == null)
        {
            return;
        }

        rootElement.style.display = DisplayStyle.None;
        Destroy(tutorial);
    }

}
