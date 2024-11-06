using System.Collections;
using UnityEngine.VFX;
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
    [SerializeField] private float minWindInterval = 5f;
    [SerializeField] private float maxWindInterval = 15f;
    [SerializeField] private float windDuration = 2f;
    [SerializeField] private float distanceFromCamera = 0.1f;

    [Header("Fade")]
    [SerializeField] private float fadeInDuration = 1f;
    [SerializeField] private float fadeOutDuration = 3f;

    private AudioSource player;
    private VisualEffect windVFX;

    private Vector3 windDirection;
    private float currentWindForce = 0f;
    private float currentAlpha = 0f;
    bool isVFXPlaying = false;

    void Start()
    {
        player = GetComponent<AudioSource>();

        windVFX = GetComponent<VisualEffect>();
        windVFX.Stop();
        windVFX.SetFloat("Alpha", currentAlpha);

        StartCoroutine(WindCycle());
        StartCoroutine(UpdateVFXPosition());
    }

    private IEnumerator WindCycle()
    {
        yield return new WaitForSeconds(initialDelay);

        while (true)
        {
            windDirection = GetRandomDirection();
            windVFX.SetVector2("WindDirection", new(windDirection.y, windDirection.x));

            StartCoroutine(ApplyWind());
            yield return new WaitForSeconds(windRampTime + windDuration + windRampTime);

            float windInterval = Random.Range(minWindInterval, maxWindInterval);
            yield return new WaitForSeconds(windInterval);
        }
    }

    private IEnumerator ApplyWind()
    {
        PlayVfxByState(true);
        PlaySfxByState(true);

        yield return StartCoroutine(RampWindForce(0f, maxWindForce));
        yield return new WaitForSeconds(windDuration - fadeOutDuration);

        PlayVfxByState(false);

        yield return new WaitForSeconds(fadeOutDuration);
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

    private void PlayVfxByState(bool state)
    {
        if (state)
        {
            isVFXPlaying = true;
            StartCoroutine(FadeInVfx());
        }
        else
        {
            
            StartCoroutine(FadeOutVfx());
        }
    }

    public IEnumerator FadeInVfx()
    {
        windVFX.Play();
        float elapsedTime = 0f;

        while (elapsedTime < fadeInDuration)
        {
            currentAlpha = Mathf.Lerp(0f, 1f, elapsedTime / fadeInDuration);
            windVFX.SetFloat("Alpha", currentAlpha);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        windVFX.SetFloat("Alpha", 1f);
    }

    public IEnumerator FadeOutVfx()
    {
        float elapsedTime = 0f;

        while (elapsedTime < fadeOutDuration)
        {
            currentAlpha = Mathf.Lerp(1f, 0f, elapsedTime / fadeOutDuration);
            windVFX.SetFloat("Alpha", currentAlpha);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        windVFX.SetFloat("Alpha", 0f);
        windVFX.Stop();
        isVFXPlaying = false;
    }

    private IEnumerator UpdateVFXPosition()
    {
        while (true)
        {
            if (windVFX)
            {
                yield return new WaitUntil(() => isVFXPlaying);
            }

            Transform cameraPosition = Camera.main.transform;
            transform.SetPositionAndRotation(
                cameraPosition.position + cameraPosition.forward * distanceFromCamera,
                Quaternion.LookRotation(-cameraPosition.forward) * Quaternion.Euler(0, 0, 90));
        }
    }

}
