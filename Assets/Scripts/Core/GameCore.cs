using System.Collections;
using UnityEngine;

public class GameCore : MonoBehaviour
{
    [SerializeField] private GameUIController gameUIController;
    [SerializeField] private int countDownFromForWin;

    private TimeLevelWinner timeLevelWinner;

    private bool isGameEnd = false;
    private bool isWin = false;

    private void Awake()
    {
        timeLevelWinner = GetComponent<TimeLevelWinner>();
    }

    private void OnEnable()
    {
        CubeFallDetector.playableCubeDetect += PlayableCubeDetected;
        CubeSpawnManagement.winGame += ProcessWinEvent;
        timeLevelWinner.OnUpdateSecond += gameUIController.ShowAndUpdateWinnerTimeOnScreen;
    }

    private void OnDisable()
    {
        CubeFallDetector.playableCubeDetect -= PlayableCubeDetected;
        CubeSpawnManagement.winGame -= ProcessWinEvent;
        timeLevelWinner.OnUpdateSecond -= gameUIController.ShowAndUpdateWinnerTimeOnScreen;
    }

    public void PlayableCubeDetected()
    {
        ProcessEndGame();
    }

    public void ProcessWinEvent()
    {
        StartCoroutine(CheckGameWin());
    }

    public void ProcessEndGame()
    {
        if (!isWin)
        {
            isGameEnd = true;
            gameUIController.GameOverUIVisibility(true);
            Time.timeScale = 0;
        }
    }

    private IEnumerator CheckGameWin() {
        timeLevelWinner.StartCountDownTimer(countDownFromForWin);
        yield return new WaitForSeconds(countDownFromForWin);

        if (!isGameEnd)
        {
            LevelManagment levelManagment = LevelManager.INSTANCE.levelManagment;

            levelManagment.currentLevel.star = gameUIController.GetLevelStar();
            levelManagment.SetLevelData(levelManagment.currentLevel);

            Level nextLevel = levelManagment.FindNextLevel();
            nextLevel.status = LevelStatus.Open;
            
            levelManagment.SetLevelData(nextLevel);
            levelManagment.currentLevel = nextLevel;
            levelManagment.SaveLevels();

            gameUIController.HideWinnerTimeOnScreen();
            gameUIController.WinnerUIVisibility(true);
        }
    }

}
