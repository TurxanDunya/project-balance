using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
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
        CalculateAngle.platformAnge += (degree) => AngleCalculated(degree);
      
    }

    private void OnDisable()
    {
        CubeFallDetector.playableCubeDetect -= PlayableCubeDetected;
        CubeSpawnManagement.winGame -= ProcessWinEvent;
        CalculateAngle.platformAnge -= (degree) => AngleCalculated(degree);
    }

    public void PlayableCubeDetected()
    {
        if (!isWin)
        {
            isGameEnd = true;
            gameUIController.GameOverUIVisibility(true);
        }

    }

    public void AngleCalculated(int degree)
    {
        var progress = 90 - degree;
        gameUIController.SetLevelStars(progress);

    }

    public void ProcessWinEvent()
    {
        UnityEngine.Debug.Log("GameCore WINProcessWinEvent");
        StartCoroutine(CheckGameWinForDuration());
    }

    private IEnumerator CheckGameWinForDuration() {
        yield return new WaitForSeconds(5);
        if (!isGameEnd)
        {
            UnityEngine.Debug.Log("GameCore CheckGameWinForDuration");
            isWin = true;
            gameUIController.WinnerUIVisibility(true);
        }
    }

   
}
