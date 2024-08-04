using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelfDestroyWithDelay : MonoBehaviour
{

    [SerializeField] private int delaySecond = 2;

    void Start()
    {
        StartCoroutine(SelfDestroy());
    }

    private IEnumerator SelfDestroy()
    {
        yield return new WaitForSeconds(delaySecond);
        Destroy(gameObject);
    }

  
}
