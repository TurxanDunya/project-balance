using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class LevelManagment
{
    private string levelFile = "levels.dat";
    private string levelsPath = "Assets/Scenes/Levels";
    public LevelList levelList;
    public Level currentLevel;


    public LevelManagment()
    {
        String levelsStatus = FileUtil.LoadFromFile(levelFile);

        var folderLevels = GetLevelNamesFromFolder(levelsPath);

        if (levelsStatus != null)
        {
            levelList = JsonUtility.FromJson<LevelList>(levelsStatus);
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
        var levelList = new LevelList();
        List<Level> initials = new List<Level>();
        foreach (string level in levels)
        {
            Level newLevel = new Level();
            if (level.Equals("Level 1"))
            {
                newLevel.status = LevelStatus.Open;
            }
            newLevel.name = level;

            initials.Add(newLevel);
        }

        levelList.levels = initials;
        return levelList;
    }


    private List<string> GetLevelNamesFromFolder(string folderPath)
    {
        List<string> levelNames = new List<string>();

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

    public void SetLevelData(Level l)
    {
        var cl = levelList.levels.Find(c => c.name == l.name);
        cl.star = l.star;
        cl.status = l.status;

    }

   public Level FindNextLevel() {
        var index = levelList.levels.IndexOf(currentLevel);
        var nextIndex = index + 1;
        if (nextIndex <= levelList.levels.Count - 1) 
        {
            return levelList.levels[nextIndex];
        }
        
        return null;
   }

    public void SaveLevels()
    {
        string levelJson = JsonUtility.ToJson(levelList, true);
        FileUtil.SaveToFile(levelJson, levelFile);
    }

    public Level GetLastStatusOpenLevel()
    {
        return levelList.levels.FindLast(c => c.status == LevelStatus.Open);
    }

}

