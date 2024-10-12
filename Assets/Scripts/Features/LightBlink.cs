using System.Collections;
using UnityEngine;

public class LightBlink : MonoBehaviour
{
    [Header("Params")]
    [SerializeField] private int initialDelay = 3;
    [SerializeField] private int maxLightOffDuration = 3;
    [SerializeField] private int minLightOffDuration = 1;
    [SerializeField] private int maxLightOnDuration = 5;
    [SerializeField] private int minLightOnDuration = 3;

    [Header("Blinking")]
    [SerializeField] private float blinkFrequency = 0.1f;
    [SerializeField] private int blinkCount = 3;

    [SerializeField] private Light[] lights;

    [Header("Sound effect params")]
    [SerializeField] private AudioClip lightOnSound;
    [SerializeField] private AudioClip lightOffSound;
    private AudioSource audioSource;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();

        StartCoroutine(BlinkLight());
    }

    private IEnumerator BlinkLight()
    {
        yield return new WaitForSeconds(initialDelay);

        while (true)
        {
            int lightOnDuration = Random.Range(minLightOnDuration, maxLightOnDuration);
            int lightOffDuration = Random.Range(minLightOffDuration, maxLightOffDuration);

            StartCoroutine(BlinkWhileOn());
            yield return new WaitForSeconds(lightOnDuration);
            SetLightsState(false);
            yield return new WaitForSeconds(lightOffDuration);
        }
    }

    private IEnumerator BlinkWhileOn()
    {
        for (int i = 0; i < blinkCount; i++)
        {
            SetLightsState(true);
            yield return new WaitForSeconds(blinkFrequency);

            SetLightsState(false);
            yield return new WaitForSeconds(blinkFrequency);
        }

        SetLightsState(true);
    }

    private void SetLightsState(bool state)
    {
        foreach (Light light in lights)
        {
            light.enabled = state;
            PlayRelativeSoundBy(state);
        }
    }

    private void PlayRelativeSoundBy(bool state)
    {
        if(state && !audioSource.isPlaying)
        {
            audioSource.clip = lightOnSound;
            audioSource.Play();
        }
        else if (!state && !audioSource.isPlaying)
        {
            audioSource.clip = lightOffSound;
            audioSource.Play();
        }
    }

}
