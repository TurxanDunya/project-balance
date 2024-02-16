using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class CalculateAngle: MonoBehaviour
{
    [SerializeField]
    Transform target,origin;

   
    public static event Action<int> platformAnge;

    float sign = 1;
    float offset = 0;

    // Update is called once per frame
    void Update()
    {
        var direction = target.position - origin.position;

        sign = (direction.y >= 0) ? 1: -1;
        // offset = (sign >= 0) ? 0 : 360;

        var angle = Vector3.Angle(Vector3.right, direction) * sign;
        Debug.Log("angle -> " + angle);
        var degree = (int)(Mathf.Abs((sign > 0) ? angle - 90 : angle + 90));
        Debug.Log("abc -> " + degree);
        platformAnge?.Invoke(degree);
    }

}
