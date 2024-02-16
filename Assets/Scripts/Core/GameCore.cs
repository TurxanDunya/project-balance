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
        CubeSpawnManagementScript.winGame += ProcessWinEvent;
        CalculateAngle.platformAnge += (degree) => AngleCalculated(degree);
    }

    private void OnDisable()
    {
        CubeFallDetector.playableCubeDetect -= PlayableCubeDetected;
        CubeSpawnManagementScript.winGame -= ProcessWinEvent;
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
        Debug.Log("Degree -> " + degree);
        gameUIController.SetLevelStars(degree);

    }

    public void ProcessWinEvent()
    {
        StartCoroutine(CheckGameWinForDuration());
    }

    private IEnumerator CheckGameWinForDuration() {
        yield return new WaitForSeconds(5);
        Debug.Log("Win game");
        if (!isGameEnd)
        {
            isWin = true;
            gameUIController.WinnerUIVisibility(true);
        }
    }

   
}
