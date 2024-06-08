using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class LevelsSceneController : MonoBehaviour
{
    [SerializeField] private VisualTreeAsset levelItem;
    [SerializeField] private StateChanger stateChanger;

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

        foreach (Level level in LevelManager.INSTANCE.levelManagment.levelList.levels) {
            var levelView = levelItem.Instantiate();

            var item = levelView.Q<VisualElement>("item");
            var background = item.Q<VisualElement>("background");
            var levelName = background.Q<Label>("name");
            var stars = background.Q<VisualElement>("stars");
            var starsLocked = background.Q<VisualElement>("stars_locked");
            var statusLocked = background.Q<VisualElement>("status");

            levelName.text = level.name;

            if (level.star == 0) {
                starsLocked.style.display = DisplayStyle.Flex;
            }
            else
            {
                var star1 = stars.Q<VisualElement>("star1");
                var star2 = stars.Q<VisualElement>("star2");
                var star3 = stars.Q<VisualElement>("star3");

                switch (level.star) {
                    case 1:
                        star1.style.display = DisplayStyle.Flex;
                        break;
                    case 2:
                        star1.style.display = DisplayStyle.Flex;
                        star2.style.display = DisplayStyle.Flex;
                        break;
                    case 3:
                        star1.style.display = DisplayStyle.Flex;
                        star2.style.display = DisplayStyle.Flex;
                        star3.style.display = DisplayStyle.Flex;
                        break;
                }

                stars.style.display = DisplayStyle.Flex;
            }

            if (level.status == LevelStatus.Open) {
                statusLocked.style.display = DisplayStyle.None;
            }

            item.AddManipulator(new Clickable(evt =>
            {
                if (level.status == LevelStatus.Open) {

                    LevelManager.INSTANCE.levelManagment.currentLevel = level;
                    SceneManager.LoadScene(level.name); // TODO: use our loader class
                }
            }));

            scrollView.Add(levelView);
        }
    }

    private void HideLevelMenu()
    {
        stateChanger.HideLevelMenu();
    }

}
