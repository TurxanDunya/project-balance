using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class LevelsSceneController : MonoBehaviour, IControllerTemplate
{
    [SerializeField] private VisualTreeAsset levelItem;
    [SerializeField] private StateChanger stateChanger;

    private static StyleColor FADED_COLOR =
        new(new Color(0.2264151f, 0.2264151f, 0.2264151f, 1.0f));

    private VisualElement rootContainer;
    private ScrollView scrollView;
    private Button resumeBtn;

    void Start()
    {
        rootContainer = GetComponent<UIDocument>()
            .rootVisualElement.Q<VisualElement>("root_container");
        scrollView = rootContainer.Q<ScrollView>("ScrollView");
        resumeBtn = rootContainer.Q<Button>("resume_btn");

        resumeBtn.clicked += () => HideLevelMenu();

        AddItems();

        SafeArea.ApplySafeArea(rootContainer);
    }

    private void AddItems()
    {
        foreach (Level level in LevelManager.INSTANCE.levelManagment.levelList.levels)
        {
            var levelView = levelItem.Instantiate();

            var itemVE = levelView.Q<VisualElement>("item");
            var backgroundVE = itemVE.Q<VisualElement>("background");
            var levelNameLbl = backgroundVE.Q<Label>("name");
            var starsVE = backgroundVE.Q<VisualElement>("stars");
            var statusLockedVE = backgroundVE.Q<VisualElement>("status");

            levelNameLbl.text = level.name;

            var star1 = starsVE.Q<VisualElement>("star1");
            var star2 = starsVE.Q<VisualElement>("star2");
            var star3 = starsVE.Q<VisualElement>("star3");

            if(level.status == LevelStatus.Locked)
            {
                starsVE.style.display = DisplayStyle.None;
                statusLockedVE.style.display = DisplayStyle.Flex;

                backgroundVE.style.unityBackgroundImageTintColor = FADED_COLOR;
            }
            else
            {
                starsVE.style.display = DisplayStyle.Flex;
                statusLockedVE.style.display = DisplayStyle.None;

                switch (level.star)
                {
                    case 0:
                        star1.style.unityBackgroundImageTintColor = FADED_COLOR;
                        star2.style.unityBackgroundImageTintColor = FADED_COLOR;
                        star3.style.unityBackgroundImageTintColor = FADED_COLOR;
                        break;
                    case 1:
                        star2.style.unityBackgroundImageTintColor = FADED_COLOR;
                        star3.style.unityBackgroundImageTintColor = FADED_COLOR;
                        break;
                    case 2:
                        star3.style.unityBackgroundImageTintColor = FADED_COLOR;
                        break;
                }
            }

            itemVE.AddManipulator(new Clickable(evt =>
            {
                if (level.status == LevelStatus.Open)
                {
                    LevelManager.INSTANCE.levelManagment.currentLevel = level;
                    LevelManager.INSTANCE.levelManagment.levelList.lastPlayedLevelName = level.name;

                    SceneManager.LoadScene(LevelNameConstants.START_LOAD_SCREEN);
                    LevelManager.INSTANCE.levelManagment.SaveLevels();
                }
            }));

            scrollView.Add(levelView);
        }

        AddToBeContinued();
    }

    private void AddToBeContinued()
    {
        var levelView = levelItem.Instantiate();

        var itemVE = levelView.Q<VisualElement>("item");
        var backgroundVE = itemVE.Q<VisualElement>("background");
        var levelNameLbl = backgroundVE.Q<Label>("name");
        var starsVE = backgroundVE.Q<VisualElement>("stars");
        var statusLockedVE = backgroundVE.Q<VisualElement>("status");

        levelNameLbl.text = "New levels soon..";
        levelNameLbl.style.fontSize = 70;
        levelNameLbl.style.color = Color.black;

        starsVE.style.display = DisplayStyle.None;
        statusLockedVE.style.display = DisplayStyle.None;

        scrollView.Add(levelView);
    }

    private void HideLevelMenu()
    {
        stateChanger.HideLevelMenu();
    }

    public void SetDisplayFlex()
    {
        rootContainer.style.display = DisplayStyle.Flex;
    }

    public void SetDisplayNone()
    {
        rootContainer.style.display = DisplayStyle.None;
    }

    public bool IsOverUI()
    {
        return rootContainer.style.display == DisplayStyle.Flex;
    }

}
