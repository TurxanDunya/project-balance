using System.Collections;
using UnityEngine;

public class GameCore : MonoBehaviour
{
    [SerializeField] private GameObject uiPrefab;

    [SerializeField] private GameUIController gameUIController;
    [SerializeField] private int countDownFromForWin;

    private TimeLevelWinner timeLevelWinner;
    private CubeSpawnManagement cubeSpawnManagement;

    private bool IsGameEnd = false;
    public bool IsWin { get; private set; } = false;
    public bool IsAboutToWin { get; private set; } = false;

    private void Awake()
    {
        timeLevelWinner = GetComponent<TimeLevelWinner>();
        cubeSpawnManagement = FindAnyObjectByType<CubeSpawnManagement>();

        uiPrefab.SetActive(true);
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
        if (IsWin || IsAboutToWin)
        {
            // Win event already started
            return;
        }

        StartCoroutine(CheckGameWinAndSaveStar());
    }

    public void ProcessEndGame()
    {
        if (!IsWin)
        {
            StopCoroutine(CheckGameWinAndSaveStar());
            Destroy(timeLevelWinner);
            gameUIController.HideWinnerTimeOnScreen();

            IsGameEnd = true;
            gameUIController.GameOverUIVisibility(true);
            cubeSpawnManagement.DestroyCurrentCube();
        }
    }

    private IEnumerator CheckGameWinAndSaveStar() {
        IsAboutToWin = true;
        if(timeLevelWinner != null) timeLevelWinner.StartCountDownTimer(countDownFromForWin);
        yield return new WaitForSeconds(countDownFromForWin);

        if (!IsGameEnd)
        {
            IsWin = true;
            LevelManagment levelManagment = LevelManager.INSTANCE.levelManagment;

            Level nextLevel = levelManagment.FindNextLevel();
            nextLevel.status = LevelStatus.Open;

            if(levelManagment.currentLevel.star < gameUIController.GetLevelStar())
            {
                levelManagment.currentLevel.star = gameUIController.GetLevelStar();
            }

            levelManagment.SetLevelData(nextLevel);
            levelManagment.SaveLevels();

            gameUIController.HideWinnerTimeOnScreen();
            gameUIController.WinnerUIVisibility(true);
        }
    }

}
