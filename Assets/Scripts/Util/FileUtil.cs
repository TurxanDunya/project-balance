using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class FileUtil 
{
    public static void SaveToFile(string file, string jsonData)
    {
        var path = Application.persistentDataPath + "/" + file;
        File.WriteAllText(path, jsonData);

    }

    public static string? LoadFromFile(string file)
    {
        var path = Application.persistentDataPath + "/" + file;
        if (File.Exists(path)) {
            var stringData = File.ReadAllText(path);
            return stringData;
        }else
        {
            return null;
        }

        
    }
}
