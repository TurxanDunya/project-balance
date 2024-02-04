using System.Collections;
using System.Collections.Generic;
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
        Button setting = menuRoot.Q<Button>("btn_setting");
        Button quit = menuRoot.Q<Button>("btn_quit");

        start.clicked += () => {
            NavigateScene(Constants.MAIN_SCENE_INDEX);
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
