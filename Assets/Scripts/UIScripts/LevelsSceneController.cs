using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class LevelsSceneController : MonoBehaviour
{
    private VisualElement rootElement;
    private VisualElement levelsPanelVE;
    private ScrollView scrollView;

    void Start()
    {
        rootElement = GetComponent<UIDocument>().rootVisualElement;
        levelsPanelVE = rootElement.Q<VisualElement>("LevelsPanelVE");
        scrollView = levelsPanelVE.Q<ScrollView>("ScrollView");
        IEnumerable<Button> buttons = scrollView.Query<Button>().ToList();

        foreach (Button button in buttons)
        {
            button.clickable.clicked += () =>
            {
                NavigateScene(int.Parse(button.text));
            };
        }
    }

    void NavigateScene(int scene)
    {
        SceneManager.LoadScene(scene);
    }

}
