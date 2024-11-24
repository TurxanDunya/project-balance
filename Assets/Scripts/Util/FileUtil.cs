using System.IO;
using UnityEngine;

public class FileUtil 
{
    public static void SaveToFile(string jsonData, string file)
    {
        var path = Application.persistentDataPath + "/" + file;
        File.WriteAllText(path, jsonData);
    }

    #nullable enable
    public static string? LoadFromFile(string file)
    {
        var path = Application.persistentDataPath + "/" + file;
        if (File.Exists(path)) {
            var stringData = File.ReadAllText(path);
            return stringData;
        }
        else
        {
            return null;
        }
    }

    public void RemoveFileByName(string fileName) {
        var filePath = Application.persistentDataPath + "/" + fileName;
        File.Delete(filePath);
    }

}
