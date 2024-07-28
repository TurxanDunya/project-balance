using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion : MonoBehaviour
{

    [SerializeField] private int delaySecond = 2;

    void Start()
    {
        StartCoroutine(SelfDestroy());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private IEnumerator SelfDestroy()
    {
        yield return new WaitForSeconds(delaySecond);
        Destroy(gameObject);
    }

  
}
