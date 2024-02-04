using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class GameUIController : MonoBehaviour
{
    [Header("GameOver UI")]
    [SerializeField] private GameObject gameOverUI;
    private VisualElement rootGameOver;
   
    void Start()
    {
        rootGameOver = gameOverUI.GetComponent<UIDocument>().rootVisualElement.Q<VisualElement>("root_container");
        rootGameOver.Q<Button>("btn_home").clicked += () => GameOverHomeClick();
    }

    void GameOverHomeClick() {
        NavigateScene(Constants.START_SCENE_INDEX);
    }

    public void GameOverUIVisibility(bool visibility) {
        if (visibility)
        {
            rootGameOver.style.display = DisplayStyle.Flex;
        }
        else {
            rootGameOver.style.display = DisplayStyle.None;
        }
    }

    void NavigateScene(int scene)
    {
        SceneManager.LoadScene(scene);
    }
}
