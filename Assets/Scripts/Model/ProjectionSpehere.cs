using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.PlayerSettings;

public class ProjectionSpehere : MonoBehaviour
{
    [SerializeField] private float radius = 0.2f;
    [SerializeField] private CubeRayCast raycast;
    [SerializeField] private float gapWithPlatform = 0.05f;


     private void Start()
    {
        float currentRadius = 0.5f * transform.localScale.x;

        float scaleFactor = radius / currentRadius;

        transform.localScale = new Vector3(scaleFactor, scaleFactor, scaleFactor);
    }


    public void SetParentObject(CubeRayCast raycastObj)
    {
        raycast = raycastObj;
    }


    public void SetRadius(float rad)
    {
        radius = rad;
    }



    private void Update()
    {
        if(raycast != null) {
            transform.position = raycast.GetLineRendererHitPosition();
        }

    }


}

