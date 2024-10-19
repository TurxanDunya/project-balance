using System.Collections;
using UnityEngine;

public class WindEffect : MonoBehaviour
{
    [SerializeField] private Rigidbody affectedMeshRb;

    [Header("Sound Params")]
    [SerializeField] private AudioClip audioClip;

    [Header("Params")]
    [SerializeField] private float initialDelay = 5f;
    [SerializeField] private float maxWindForce = 5f;     
    [SerializeField] private float windRampTime = 2f;
    [SerializeField] private float windInterval = 4f;
    [SerializeField] private float windDuration = 2f;

    private AudioSource player;

    private Vector3 windDirection;
    private float currentWindForce = 0f;

    void Start()
    {
        player = GetComponent<AudioSource>();

        StartCoroutine(WindCycle());
    }

    private IEnumerator WindCycle()
    {
        yield return new WaitForSeconds(initialDelay);

        while (true)
        {
            windDirection = GetRandomDirection();

            StartCoroutine(ApplyWind());
            yield return new WaitForSeconds(windRampTime + windDuration + windRampTime);
            yield return new WaitForSeconds(windInterval);
        }
    }

    private IEnumerator ApplyWind()
    {
        PlaySfxByState(true);

        yield return StartCoroutine(RampWindForce(0f, maxWindForce));
        yield return new WaitForSeconds(windDuration);

        PlaySfxByState(false);
    }

    private IEnumerator RampWindForce(float startForce, float endForce)
    {
        float elapsedTime = 0f;

        while (elapsedTime < windRampTime)
        {
            elapsedTime += Time.deltaTime;
            currentWindForce = Mathf.Lerp(startForce, endForce, elapsedTime / windRampTime);
            affectedMeshRb.AddForce(windDirection * currentWindForce, ForceMode.Force);
            yield return null;
        }
    }

    private Vector3 GetRandomDirection()
    {
        float randomAngle = Random.Range(0, 360);

        float x = Mathf.Cos(randomAngle * Mathf.Deg2Rad);
        float z = Mathf.Sin(randomAngle * Mathf.Deg2Rad);

        return new Vector3(x, 0, z).normalized;
    }

    private void PlaySfxByState(bool state)
    {
        if (state && !player.isPlaying)
        {
            player.clip = audioClip;
            player.Play();
        }
        else if (!state && player.isPlaying)
        {
            player.Stop();
        }
        
    }

}
