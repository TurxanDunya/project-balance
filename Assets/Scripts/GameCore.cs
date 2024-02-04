using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameCore : MonoBehaviour
{
    [Header("GameUI Controller")]
    [SerializeField]
    public GameUIController gameUIController;

    private bool isGameEnded = false;

    void Start()
    {
       

    }

    // Update is called once per frame
    void Update()
    {
       
    }

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
