using System.Collections;
using UnityEngine;

public class TimeLevelWinner : MonoBehaviour
{
    private int currentSecond;

    public delegate void UpdateSecond(int currentSecond);
    public event UpdateSecond OnUpdateSecond;

    private void OnDisable()
    {
        StopAllCoroutines();
    }

    public void StartCountDownTimer(int seconds)
    {
        StartCoroutine(CountDownFrom(seconds));
    }

    private IEnumerator CountDownFrom(int seconds)
    {
        currentSecond = seconds;

        while (currentSecond > 0)
        {
            OnUpdateSecond?.Invoke(currentSecond);
            currentSecond--;
            yield return new WaitForSeconds(1);
        }
    }

}
