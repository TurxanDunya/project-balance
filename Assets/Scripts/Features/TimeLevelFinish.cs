using System.Collections;
using UnityEngine;

/**
 * IMPORTANT NOTE! InGameUiController class should inject this class to work properly!
 */
public class TimeLevelFinish : MonoBehaviour
{
    [Header("Params")]
    [SerializeField] private int timeToFinishLevel;

    [Header("Dependencies")]
    [SerializeField] private HomeScreenController homeScreenController;

    public delegate void UpdateSecondEvent(int second);
    public event UpdateSecondEvent OnUpdateSecondEvent;

    private GameCore gameCore;

    public void IncreaseFinishTime(int duration)
    {
        timeToFinishLevel += duration;
    }

    private void Start()
    {
        gameCore = FindAnyObjectByType<GameCore>();

        StartCoroutine(UpdateSecond());
    }

    private IEnumerator UpdateSecond()
    {
        while (timeToFinishLevel >= 0)
        {
            if (homeScreenController && homeScreenController.IsDisplayFlex())
            {
                yield return new WaitForSeconds(1);
            }
            else
            {
                OnUpdateSecondEvent(timeToFinishLevel);
                timeToFinishLevel--;
                yield return new WaitForSeconds(1);
            }
        }

        gameCore.ProcessEndGame();
    }

}
