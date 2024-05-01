using System.Collections;
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
        
        StartCoroutine(CheckGameWinForDuration());
    }

    private IEnumerator CheckGameWinForDuration() {
        yield return new WaitForSeconds(5);
        if (!isGameEnd)
        {
            LevelManager.INSTANCE.levelManagment.currentLevel.star = gameUIController.GetLevelStar();
            LevelManager.INSTANCE.levelManagment.SetLevelData(LevelManager.INSTANCE.levelManagment.currentLevel);
            var nextLevel = LevelManager.INSTANCE.levelManagment.FindNextLevel();
            nextLevel.status = LevelStatus.Open;
            LevelManager.INSTANCE.levelManagment.SetLevelData(nextLevel);
            LevelManager.INSTANCE.levelManagment.currentLevel = nextLevel;
            LevelManager.INSTANCE.levelManagment.SaveLevels();
            gameUIController.WinnerUIVisibility(true);

        }
    }

   
}
