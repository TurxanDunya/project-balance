using System.Collections;
using UnityEngine;
using UnityEngine.UIElements;

public class TutorialController : MonoBehaviour, IControllerTemplate
{
    private static readonly ILogger logger = Debug.unityLogger;

    [SerializeField] private GameObject[] tutorials;
    [SerializeField] private GameObject rootTutorialObject;
    [SerializeField] private float tutorialPopUpInterval = 0.5f;

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
        StateChanger.CheckTutorialsStatus -= CheckStatuses;
    }

    private void CheckStatuses()
    {
        if (tutorials == null || tutorials.Length == 0)
        {
            logger.Log(LogType.Log, "There is no any tutorial to show!");
            Destroy(rootTutorialObject);
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
            continueBtn.RegisterCallback<PointerEnterEvent>((ev) =>
            {
                InputManager.isOverUI = true;
                isBtnClicked = true;
                DismissUIPanel(tutorial);
            });

            yield return new WaitUntil(() => isBtnClicked);

            continueBtn.UnregisterCallback<PointerEnterEvent>((ev) =>
            {
                InputManager.isOverUI = false;
            });

            if (currentTutorialNumber < tutorialCountOnLevel)
            {
                yield return new WaitForSeconds(tutorialPopUpInterval);
            }
        }

        Destroy(rootTutorialObject);
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

        if (tutorial.name == "MoveAndPlayTutorial")
        {
            if (tutorialSaveSystem.GetIsMoveAndPlayTutorialWatched())
            {
                DismissUIPanel(tutorial);
                return true;
            }
            else
            {
                tutorial.SetActive(true);
                rootElement = tutorial.GetComponent<UIDocument>().rootVisualElement.Q<VisualElement>("RootVE");
                rootElement.style.display = DisplayStyle.Flex;
                tutorialSaveSystem.SetIsMoveAndPlayTutorialWatched();
                return false;
            }
        }

        if (tutorial.name == "MeetMetalAndMagnet")
        {
            if (tutorialSaveSystem.GetMeetMetalAndMagnetTutorialWatched())
            {
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

        if (tutorial.name == "GhostCubeTutorial")
        {
            if (tutorialSaveSystem.GetGhostCubeTutorialWatched())
            {
                DismissUIPanel(tutorial);
                return true;
            }
            else
            {
                tutorial.SetActive(true);
                rootElement = tutorial.GetComponent<UIDocument>().rootVisualElement.Q<VisualElement>("RootVE");
                rootElement.style.display = DisplayStyle.Flex;
                tutorialSaveSystem.SetGhostCubeTutorialWatched();
                return false;
            }
        }

        if (tutorial.name == "LightOnOffTutorial")
        {
            if (tutorialSaveSystem.GetLightOnOffTutorialWatched())
            {
                DismissUIPanel(tutorial);
                return true;
            }
            else
            {
                tutorial.SetActive(true);
                rootElement = tutorial.GetComponent<UIDocument>().rootVisualElement.Q<VisualElement>("RootVE");
                rootElement.style.display = DisplayStyle.Flex;
                tutorialSaveSystem.SetLightOnOffTutorialWatched();
                return false;
            }
        }

        if (tutorial.name == "TimerTutorial")
        {
            if (tutorialSaveSystem.GetTimerTutorialWatched())
            {
                DismissUIPanel(tutorial);
                return true;
            }
            else
            {
                tutorial.SetActive(true);
                rootElement = tutorial.GetComponent<UIDocument>().rootVisualElement.Q<VisualElement>("RootVE");
                rootElement.style.display = DisplayStyle.Flex;
                tutorialSaveSystem.SetTimerTutorialWatched();
                return false;
            }
        }

        if (tutorial.name == "InvertModeTutorial")
        {
            if (tutorialSaveSystem.GetInvertModeTutorialWatched())
            {
                DismissUIPanel(tutorial);
                return true;
            }
            else
            {
                tutorial.SetActive(true);
                rootElement = tutorial.GetComponent<UIDocument>().rootVisualElement.Q<VisualElement>("RootVE");
                rootElement.style.display = DisplayStyle.Flex;
                tutorialSaveSystem.SetInvertModeTutorialWatched();
                return false;
            }
        }

        if (tutorial.name == "FallingShapesTutorial")
        {
            if (tutorialSaveSystem.GetFallingShapesTutorialWatched())
            {
                DismissUIPanel(tutorial);
                return true;
            }
            else
            {
                tutorial.SetActive(true);
                rootElement = tutorial.GetComponent<UIDocument>().rootVisualElement.Q<VisualElement>("RootVE");
                rootElement.style.display = DisplayStyle.Flex;
                tutorialSaveSystem.SetFallingShapesTutorialWatched();
                return false;
            }
        }

        if (tutorial.name == "WindModeTutorial")
        {
            if (tutorialSaveSystem.GetWindModeTutorialWatched())
            {
                DismissUIPanel(tutorial);
                return true;
            }
            else
            {
                tutorial.SetActive(true);
                rootElement = tutorial.GetComponent<UIDocument>().rootVisualElement.Q<VisualElement>("RootVE");
                rootElement.style.display = DisplayStyle.Flex;
                tutorialSaveSystem.SetWindModeTutorialWatched();
                return false;
            }
        }

        if (tutorial.name == "CubeLateFallTutorial")
        {
            if (tutorialSaveSystem.GetCubeLateFallTutorialWatched())
            {
                DismissUIPanel(tutorial);
                return true;
            }
            else
            {
                tutorial.SetActive(true);
                rootElement = tutorial.GetComponent<UIDocument>().rootVisualElement.Q<VisualElement>("RootVE");
                rootElement.style.display = DisplayStyle.Flex;
                tutorialSaveSystem.SetCubeLateFallTutorialWatched();
                return false;
            }
        }

        if (tutorial.name == "MeetIceCubeTutorial")
        {
            if (tutorialSaveSystem.GetMeetIceCubeTutorialWatched())
            {
                DismissUIPanel(tutorial);
                return true;
            }
            else
            {
                tutorial.SetActive(true);
                rootElement = tutorial.GetComponent<UIDocument>().rootVisualElement.Q<VisualElement>("RootVE");
                rootElement.style.display = DisplayStyle.Flex;
                tutorialSaveSystem.SetMeetIceCubeTutorialWatched();
                return false;
            }
        }

        if (tutorial.name == "MeetBombTutorial")
        {
            if (tutorialSaveSystem.GetMeetBombTutorialWatched())
            {
                DismissUIPanel(tutorial);
                return true;
            }
            else
            {
                tutorial.SetActive(true);
                rootElement = tutorial.GetComponent<UIDocument>().rootVisualElement.Q<VisualElement>("RootVE");
                rootElement.style.display = DisplayStyle.Flex;
                tutorialSaveSystem.SetMeetBombTutorialWatched();
                return false;
            }
        }

        if (tutorial.name == "CubeChangerActiveTutorial")
        {
            if (tutorialSaveSystem.GetCubeChangerEnabledTutorialWatched())
            {
                DismissUIPanel(tutorial);
                return true;
            }
            else
            {
                tutorial.SetActive(true);
                rootElement = tutorial.GetComponent<UIDocument>().rootVisualElement.Q<VisualElement>("RootVE");
                rootElement.style.display = DisplayStyle.Flex;
                tutorialSaveSystem.SetCubeChangerActiveTutorialWatched();
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

    public void SetDisplayFlex()
    {
        rootElement.style.display = DisplayStyle.Flex;
    }

    public void SetDisplayNone()
    {
        rootElement.style.display = DisplayStyle.None;
    }

}
