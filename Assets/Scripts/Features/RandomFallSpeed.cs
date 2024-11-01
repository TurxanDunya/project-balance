using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class RandomFallSpeed : MonoBehaviour
{
    [SerializeField] int minFallDrag = 30;
    [SerializeField] int maxFallDrag = 40;
    [SerializeField] float restoreSpeedTime = 1.5f;
    [SerializeField] bool randomlyActive = true;
    [SerializeField] int randomActiveFactor = 1;

    private float defaultDrag;

    private Rigidbody rb;

    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody>();
        if (rb == null) { Destroy(this); return; };
        defaultDrag = rb.drag;
        if (randomlyActive)
        {
            if (Random.Range(0, randomActiveFactor + 1) == randomActiveFactor) rb.drag = Random.Range(minFallDrag, maxFallDrag);
        }
        else
        {
            rb.drag = Random.Range(minFallDrag, maxFallDrag);
        }
    }

    public void RestoreDefaultSpeed()
    {
        StartCoroutine(RestoreSpeed());
    }

    IEnumerator RestoreSpeed()
    {
        yield return new WaitForSeconds(restoreSpeedTime);
        rb.drag = defaultDrag;
    }

}