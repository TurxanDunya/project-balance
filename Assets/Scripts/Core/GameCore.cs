using System.Collections;
using UnityEngine;

public class GameCore : MonoBehaviour
{
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
        ProcessEndGame();
    }

    public void ProcessWinEvent()
    {
        StartCoroutine(CheckGameWinForDuration());
    }

    public void ProcessEndGame()
    {
        if (!isWin)
        {
            isGameEnd = true;
            gameUIController.GameOverUIVisibility(true);
            Time.timeScale = 0; // freezes gameplay
        }
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
