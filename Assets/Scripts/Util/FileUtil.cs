using System.IO;
using UnityEngine;

public class FileUtil 
{
    public static void SaveToFile(string jsonData, string file)
    {
        var path = "/Users/turkhand/Desktop" + "/" + file;
        File.WriteAllText(path, jsonData);
    }

    #nullable enable
    public static string? LoadFromFile(string file)
    {
        var path = "/Users/turkhand/Desktop" + "/" + file;
        if (File.Exists(path)) {
            var stringData = File.ReadAllText(path);
            return stringData;
        }
        else
        {
            return null;
        }
    }

    public void RemoveMyFiles() {
        var file1 = Application.persistentDataPath + "/lvl.dat";
        var file2 = Application.persistentDataPath + "/level.dat";
        var file3 = Application.persistentDataPath + "/levels.dat";

        File.Delete(file1);
        File.Delete(file2);
        File.Delete(file3);
    }
}
