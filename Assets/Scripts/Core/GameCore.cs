using System.Collections;
using UnityEngine;

public class GameCore : MonoBehaviour
{
    [SerializeField] private GameUIController gameUIController;
    [SerializeField] private int countDownFromForWin;

    private TimeLevelWinner timeLevelWinner;
    private CubeSpawnManagement cubeSpawnManagement;

    private bool isGameEnd = false;
    private bool isWin = false;
    private bool isAboutToWin = false;

    private void Awake()
    {
        timeLevelWinner = GetComponent<TimeLevelWinner>();
        cubeSpawnManagement = FindAnyObjectByType<CubeSpawnManagement>();
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
        if (isWin || isAboutToWin)
        {
            // Win event already started
            return;
        }

        StartCoroutine(CheckGameWinAndSaveStar());
    }

    public void ProcessEndGame()
    {
        if (!isWin)
        {
            StopCoroutine(CheckGameWinAndSaveStar());
            Destroy(timeLevelWinner);
            gameUIController.HideWinnerTimeOnScreen();

            isGameEnd = true;
            gameUIController.GameOverUIVisibility(true);
            cubeSpawnManagement.DestroyCurrentCube();
        }
    }

    private IEnumerator CheckGameWinAndSaveStar() {
        isAboutToWin = true;
        timeLevelWinner.StartCountDownTimer(countDownFromForWin);
        yield return new WaitForSeconds(countDownFromForWin);

        if (!isGameEnd)
        {
            isWin = true;
            LevelManagment levelManagment = LevelManager.INSTANCE.levelManagment;

            Level nextLevel = levelManagment.FindNextLevel();
            nextLevel.status = LevelStatus.Open;

            if(levelManagment.currentLevel.star < gameUIController.GetLevelStar())
            {
                levelManagment.currentLevel.star = gameUIController.GetLevelStar();
            }

            levelManagment.currentLevel = nextLevel;
            levelManagment.levelList.lastPlayedLevelName = nextLevel.name;

            levelManagment.SetLevelData(nextLevel);
            levelManagment.SaveLevels();

            gameUIController.HideWinnerTimeOnScreen();
            gameUIController.WinnerUIVisibility(true);
        }
    }

}
