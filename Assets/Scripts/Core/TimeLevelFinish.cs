using UnityEngine;

public class TimeLevelFinish : MonoBehaviour
{
    [SerializeField] private bool enableFinishTime;
    [SerializeField] private float timeToFinishLevel;

    private float startTime;
    private float elapsedTime;

    private GameCore gameCore;

    public void IncreaseFinishTime(float duration)
    {
        timeToFinishLevel += duration;
    }

    private void Start()
    {
        gameCore = FindAnyObjectByType<GameCore>();

        startTime = Time.time;
    }

    private void Update()
    {
        elapsedTime = Time.time - startTime;

        if (enableFinishTime && elapsedTime > timeToFinishLevel)
        {
            gameCore.ProcessEndGame();
        }
    }

}
