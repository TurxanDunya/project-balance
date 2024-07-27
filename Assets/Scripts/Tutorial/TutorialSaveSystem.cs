using UnityEngine;

public class TutorialSaveSystem : MonoBehaviour
{
    private readonly string tutorialSaveFile = "tutorials.dat";
    private TutorialSaveData tutorialSaveData;

    private void OnEnable()
    {
        string tutorialSaveDataAsString = FileUtil.LoadFromFile(tutorialSaveFile);

        if (tutorialSaveDataAsString != null)
        {
            tutorialSaveData = JsonUtility.FromJson<TutorialSaveData>(tutorialSaveDataAsString);
        }
        else
        {
            tutorialSaveData = new();
            tutorialSaveData.isWelcomeTutorialWatched = false;
            SaveData();
        }
    }

    public bool GetIsWelcomeTutorialWatched()
    {
        return tutorialSaveData.isWelcomeTutorialWatched;
    }

    public void SetWelcomeTutorialWatched()
    {
        tutorialSaveData.isWelcomeTutorialWatched = true;
        SaveData();
    }

    private void SaveData()
    {
        string tutorialDataJson = JsonUtility.ToJson(tutorialSaveData, true);
        FileUtil.SaveToFile(tutorialDataJson, tutorialSaveFile);
    }
}
