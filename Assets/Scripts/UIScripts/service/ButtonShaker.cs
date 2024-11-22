using System.Collections;
using UnityEngine;
using UnityEngine.UIElements;

public class ButtonShaker : MonoBehaviour
{
    public float shakeDuration = 0.5f;
    public float shakeMagnitude = 5f;
    public float zoomAmount = 1.1f;
    public float interval = 2f;

    private Quaternion originalRotation;
    private Vector3 originalScale;

    private Button button;

    private void OnDisable()
    {
        StopAllCoroutines();
    }

    public void ShakeButton(Button button)
    {
        this.button = button;
        originalRotation = button.transform.rotation;
        originalScale = button.transform.scale;

        StartCoroutine(ShakeAndZoomCoroutine());
    }

    private IEnumerator ShakeAndZoomCoroutine()
    {
        while(true)
        {
            yield return new WaitForSeconds(interval);

            float elapsed = 0f;
            while (elapsed < shakeDuration)
            {
                float magnitude = SwitchToggle() * shakeMagnitude;

                button.transform.rotation =
                    originalRotation * Quaternion.Euler(0, 0, magnitude);
                button.transform.scale = originalScale * zoomAmount;

                elapsed += Time.deltaTime;

                yield return null;
            }

            button.transform.rotation = originalRotation;
            button.transform.scale = originalScale;
        }
    }

    private bool IsToggleOn = true;
    private int SwitchToggle()
    {
        if(IsToggleOn)
        {
            IsToggleOn = false;
            return 1;
        }
        else
        {
            IsToggleOn = true;
            return -1;
        }
    }
}
