using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JsonToTypeConverter
{
    public static T ConvertFromJson<T>(string json) {
        var data = JsonUtility.FromJson<T>(json);
        return data;
    }
}
