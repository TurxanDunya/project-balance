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
            tutorialSaveData.isMeetWoodAndChangerTutorialWatched = false;
            tutorialSaveData.isMeetMetalAndMagnetTutorialWatched = false;
            tutorialSaveData.isGhostCubeTutorialWatched = false;
            tutorialSaveData.isLightOnOffTutorialWatched = false;
            SaveData();
        }
    }

    public bool GetIsWelcomeTutorialWatched()
    {
        return tutorialSaveData.isWelcomeTutorialWatched;
    }

    public bool GetMeetWoodAndChangerTutorialWatched()
    {
        return tutorialSaveData.isMeetWoodAndChangerTutorialWatched;
    }

    public bool GetMeetMetalAndMagnetTutorialWatched()
    {
        return tutorialSaveData.isMeetMetalAndMagnetTutorialWatched;
    }

    public bool GetGhostCubeTutorialWatched()
    {
        return tutorialSaveData.isGhostCubeTutorialWatched;
    }

    public bool GetLightOnOffTutorialWatched()
    {
        return tutorialSaveData.isLightOnOffTutorialWatched;
    }

    public void SetWelcomeTutorialWatched()
    {
        tutorialSaveData.isWelcomeTutorialWatched = true;
        SaveData();
    }

    public void SetMeetWoodAndChangerTutorialWatched()
    {
        tutorialSaveData.isMeetWoodAndChangerTutorialWatched = true;
        SaveData();
    }

    public void SetMeetMetalAndMagnetTutorialWatched()
    {
        tutorialSaveData.isMeetMetalAndMagnetTutorialWatched = true;
        SaveData();
    }

    public void SetGhostCubeTutorialWatched()
    {
        tutorialSaveData.isGhostCubeTutorialWatched = true;
        SaveData();
    }

    public void SetLightOnOffTutorialWatched()
    {
        tutorialSaveData.isLightOnOffTutorialWatched = true;
        SaveData();
    }

    private void SaveData()
    {
        string tutorialDataJson = JsonUtility.ToJson(tutorialSaveData, true);
        FileUtil.SaveToFile(tutorialDataJson, tutorialSaveFile);
    }
}
