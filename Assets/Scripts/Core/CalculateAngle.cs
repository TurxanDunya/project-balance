using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class CalculateAngle: MonoBehaviour
{
    [SerializeField]
    Transform target;

   
    public static event Action<int> platformAnge;

    float currentX = 0;
    float currentZ = 0;

    private void Start()
    {
        currentX = transform.rotation.eulerAngles.x;
        currentZ = transform.rotation.eulerAngles.z;
    }

    void Update()
    {
        var eulerAnglesX = target.rotation.eulerAngles.x;
        var eulerAnglesZ = target.rotation.eulerAngles.z;
        var angleX = (int)Mathf.Abs(Mathf.DeltaAngle(currentX, eulerAnglesX));
        var angleZ = (int)Mathf.Abs(Mathf.DeltaAngle(currentZ, eulerAnglesZ));
        var finalAngle = Mathf.Max(angleX, angleZ);
        platformAnge?.Invoke(finalAngle);
    }

}
