using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameCore : MonoBehaviour
{
    [Header("GameUI Controller")]
    [SerializeField] private GameUIController gameUIController;

    private bool isGameEnd = false;
    private bool isWin = false;

    private void OnEnable()
    {
        CubeFallDetector.playableCubeDetect += PlayableCubeDetected;
        CubeSpawnManagement.winGame += ProcessWinEvent;
    }

    private void OnDisable()
    {
        CubeFallDetector.playableCubeDetect -= PlayableCubeDetected;
        CubeSpawnManagement.winGame -= ProcessWinEvent;
    }

    public void PlayableCubeDetected()
    {
        if (!isWin)
        {
            isGameEnd = true;
            gameUIController.GameOverUIVisibility(true);
        }

    }

    public void ProcessWinEvent()
    {
        StartCoroutine(CheckGameWinForDuration());
    }

    private IEnumerator CheckGameWinForDuration() {
        yield return new WaitForSeconds(5);
        if (!isGameEnd)
        {
            isWin = true;
            gameUIController.WinnerUIVisibility(true);
        }
    }

   
}
