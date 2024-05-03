using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.SceneManagement;

public class MainMenuController : MonoBehaviour
{
    private VisualElement menuRoot;
   
    void Start()
    {
        menuRoot = GetComponent<UIDocument>().rootVisualElement;
        Button start = menuRoot.Q<Button>("btn_start");
        Button levels = menuRoot.Q<Button>("btn_levels");
        Button quit = menuRoot.Q<Button>("btn_quit");


        start.clicked += () => {
            var lastLevel = new LevelManagment().currentLevel;
            SceneManager.LoadScene(lastLevel.name);
        };

        levels.clicked += () => {
            NavigateScene(Constants.LEVELS_SCENE_INDEX);
        };

        quit.clicked += () => Quit();
    }

    void NavigateScene(int scene)
    {
        SceneManager.LoadScene(scene);
    }

    void Quit()
    {
        Application.Quit();
    }
}
