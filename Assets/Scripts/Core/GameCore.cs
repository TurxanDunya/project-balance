using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameCore : MonoBehaviour
{
    [Header("GameUI Controller")]
    [SerializeField] private GameUIController gameUIController;

    private bool isGameEnded = false;

    private void OnEnable()
    {
        CubeFallDetector.playableCubeDetect += PlayableCubeDetected;

    }

    private void OnDisable()
    {
        CubeFallDetector.playableCubeDetect -= PlayableCubeDetected;
    }

    public void PlayableCubeDetected()
    {
        isGameEnded = true;
        gameUIController.GameOverUIVisibility(true);
    }
}
