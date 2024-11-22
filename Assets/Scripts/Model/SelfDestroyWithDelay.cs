using System.Collections;
using UnityEngine;

public class SelfDestroyWithDelay : MonoBehaviour
{
    [SerializeField] private int delaySecond = 2;

    void Start()
    {
        StartCoroutine(SelfDestroy());
    }

    private void OnDisable()
    {
        StopAllCoroutines();
    }

    private IEnumerator SelfDestroy()
    {
        yield return new WaitForSeconds(delaySecond);
        Destroy(gameObject);
    }

}
