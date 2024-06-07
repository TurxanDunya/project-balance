using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class LevelsSceneController : MonoBehaviour
{
    private VisualElement rootContainer;
    private ScrollView scrollView;

    [SerializeField] private VisualTreeAsset levelItem;

    void Start()
    {
        rootContainer = GetComponent<UIDocument>()
            .rootVisualElement.Q<VisualElement>("root_container");
        scrollView = rootContainer.Q<ScrollView>("ScrollView");

        foreach (Level level in LevelManager.INSTANCE.levelManagment.levelList.levels) {
            var levelView = levelItem.Instantiate();

            var item = levelView.Q<VisualElement>("item");
            var background = item.Q<VisualElement>("background");
            var levelName = background.Q<Label>("name");
            levelName.text = level.name;

            var stars = item.Q<VisualElement>("stars");
            var starsLocked = item.Q<VisualElement>("stars_locked");
            var statusLocked = item.Q<VisualElement>("status");

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

}
