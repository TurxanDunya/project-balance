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

    [Header("Sound")]
    [SerializeField] private AudioClip clockTickSound;

    public delegate void UpdateSecondEvent(int second);
    public event UpdateSecondEvent OnUpdateSecondEvent;

    private GameCore gameCore;
    private AudioSource audioSource;

    public void IncreaseFinishTime(int duration)
    {
        timeToFinishLevel += duration;
    }

    private void Start()
    {
        gameCore = FindAnyObjectByType<GameCore>();
        audioSource = GetComponent<AudioSource>();

        audioSource.clip = clockTickSound;

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
                StartClockTickSound(timeToFinishLevel);
            }
        }

        gameCore.ProcessEndGame();
    }

    private void StartClockTickSound(int second)
    {
        if (second <= 10 && second > 0 && !audioSource.isPlaying)
        {
            audioSource.Play();
        }
        else if ((second > 10 || second <= 0) && audioSource.isPlaying)
        {
            audioSource.Stop();
        }
    }

}
