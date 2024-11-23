using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManagment
{
    private readonly string levelFile = "levels.dat";
    public LevelList levelList;
    public Level currentLevel;

    public LevelManagment()
    {
        string levelsData = FileUtil.LoadFromFile(levelFile);

        var folderLevels = GetLevelNamesFromBuildSettings();

        if (levelsData != null)
        {
            levelList = JsonUtility.FromJson<LevelList>(levelsData);
        }
        else
        {
            levelList = MapToInitialLevel(folderLevels);
            SaveLevels();
        }

        currentLevel = GetLastStatusOpenLevel();
    }

    private LevelList MapToInitialLevel(List<string> levels)
    {
        List<Level> initials = new();
        foreach (string level in levels)
        {
            Level newLevel = new();
            if (level.Equals(LevelNameConstants.LEVEL_1_NAME))
            {
                newLevel.status = LevelStatus.Open;
            }
            newLevel.name = level;

            initials.Add(newLevel);
        }

        var levelList = new LevelList
        {
            levels = initials,
            lastPlayedLevelName = LevelNameConstants.LEVEL_1_NAME
        };

        return levelList;
    }

    private List<string> GetLevelNamesFromFolder(string folderPath)
    {
        List<string> levelNames = new();

        if (!Directory.Exists(folderPath))
        {
            return levelNames;
        }

        string[] files = Directory.GetFiles(folderPath);

        foreach (string file in files)
        {
            if (file.EndsWith(".unity"))
            {
                string sceneName = Path.GetFileNameWithoutExtension(file);
                levelNames.Add(sceneName);
            }
        }

        return levelNames;
    }

    public void SetLevelData(Level level)
    {
        var cl = levelList.levels.Find(c => c.name == level.name);
        cl.star = level.star;
        cl.status = level.status;
    }

    private List<string> GetLevelNamesFromBuildSettings()
    {
        List<string> levelNames = new();
        int sceneCount = SceneManager.sceneCountInBuildSettings;
       
        for (int i = 0; i < sceneCount; i++)
        {
            var fileName = SceneUtility.GetScenePathByBuildIndex(i);
            if (fileName.Contains("/Levels/") && fileName.EndsWith(".unity")) {
                var levelName = Path.GetFileNameWithoutExtension(fileName);
                levelNames.Add(levelName);
            }
        }

        return levelNames;
    }

   public Level FindNextLevel() {
        var index = levelList.levels.IndexOf(currentLevel);

        if (index >= levelList.levels.Count)
        {
            return levelList.levels[index];
        }

        if (index < levelList.levels.Count - 1) 
        {
            var nextIndex = ++index;
            return levelList.levels[nextIndex];
        }

        return levelList.levels[index];
   }

    public void SaveLevels()
    {
        string levelJson = JsonUtility.ToJson(levelList, true);
        FileUtil.SaveToFile(levelJson, levelFile);
    }

    public Level GetLastStatusOpenLevel()
    {
        return levelList.levels.FindLast(level => level.status == LevelStatus.Open);
    }

}

