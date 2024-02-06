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

    [Header("Winner UI")]
    [SerializeField] private GameObject winnerUI;
    private VisualElement rootWinner;

    void Start()
    {
        rootGameOver = gameOverUI.GetComponent<UIDocument>().rootVisualElement.Q<VisualElement>("root_container");
        rootWinner = winnerUI.GetComponent<UIDocument>().rootVisualElement.Q<VisualElement>("root_container");
        rootGameOver.Q<Button>("btn_home").clicked += () => GameOverHomeClick();
        rootWinner.Q<Button>("btn_next").clicked += () => GoNextLevel();
    }

    void GameOverHomeClick() {
        NavigateScene(Constants.START_SCENE_INDEX);
    }

    void GoNextLevel()
    {
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

    public void WinnerUIVisibility(bool visibility)
    {
        if (visibility)
        {
            rootWinner.style.display = DisplayStyle.Flex;
        }
        else
        {
            rootWinner.style.display = DisplayStyle.None;
        }
    }

    void NavigateScene(int scene)
    {
        SceneManager.LoadScene(scene);
    }
}
