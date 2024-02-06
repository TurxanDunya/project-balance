using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameCore : MonoBehaviour
{
    [Header("GameUI Controller")]
    [SerializeField] private GameUIController gameUIController;

    private void OnEnable()
    {
        CubeFallDetector.playableCubeDetect += PlayableCubeDetected;
        CubeSpawnManagementScript.winGame += GameWin;
    }

    private void OnDisable()
    {
        CubeFallDetector.playableCubeDetect -= PlayableCubeDetected;
        CubeSpawnManagementScript.winGame -= GameWin;
    }

    public void PlayableCubeDetected()
    {
        gameUIController.GameOverUIVisibility(true);
    }

    public void GameWin()
    {
        gameUIController.WinnerUIVisibility(true);
    }
}
