using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.SceneManagement;

public class InGameButtonsController : MonoBehaviour
{
    [Header("Dependant controllers")]
    [SerializeField] private PausePopUpController pausePopUpController;

    private VisualElement rootElement;
    private VisualElement topVE;
    private Button pauseButton;
    private Button levelsButton;

    void Start()
    {
        rootElement = GetComponent<UIDocument>().rootVisualElement.Q<VisualElement>("RootVE");
        topVE = rootElement.Q<VisualElement>("top-VE");
        pauseButton = topVE.Q<Button>("Pause");
        levelsButton = topVE.Q<Button>("Levels");

        pauseButton.clicked += () => PauseGame();
        levelsButton.clicked += () => ShowLevels();
    }

    private void PauseGame()
    {
        Time.timeScale = 0;
        pausePopUpController.Show();
    }

    private void ShowLevels()
    {
        SceneManager.LoadScene(LevelNameConstants.LEVEL_SCENE_NAME);
    }

}
