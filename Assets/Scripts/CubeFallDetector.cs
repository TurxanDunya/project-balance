using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeFallDetector : MonoBehaviour
{
    const string PLAYABLE_CUBE = "PlayableCube";

    /*
    public delegate void PlayableCubeDetect();
    public static event PlayableCubeDetect playableCubeDetect;
    */
    public static event Action playableCubeDetect;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == PLAYABLE_CUBE)
        {
            playableCubeDetect?.Invoke();
          
        }
    }

  


}
