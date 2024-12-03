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
            tutorialSaveData.isMoveAndPlayTutorialWatched = false;
            tutorialSaveData.isMeetMetalAndMagnetTutorialWatched = false;
            tutorialSaveData.isGhostCubeTutorialWatched = false;
            tutorialSaveData.isLightOnOffTutorialWatched = false;
            tutorialSaveData.isTimerTutorialWatched = false;
            tutorialSaveData.isInvertModeTutorialWatched = false;
            tutorialSaveData.isFallingShapesModeTutorialWatched = false;
            tutorialSaveData.isWindModeTutorialWatched = false;
            tutorialSaveData.isCubeLateFallTutorialWatched = false;
            tutorialSaveData.isMeetIceCubeTutorialWatched = false;
            tutorialSaveData.isMeetBombTutorialWatched = false;
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

    public bool GetMeetIceCubeTutorialWatched()
    {
        return tutorialSaveData.isMeetIceCubeTutorialWatched;
    }

    public bool GetIsMoveAndPlayTutorialWatched()
    {
        return tutorialSaveData.isMoveAndPlayTutorialWatched;
    }

    public bool GetMeetBombTutorialWatched()
    {
        return tutorialSaveData.isMeetBombTutorialWatched;
    }

    public bool GetCubeChangerEnabledTutorialWatched()
    {
        return tutorialSaveData.isCubeChangerActiveTutorialWatched;
    }

    public bool GetGhostCubeTutorialWatched()
    {
        return tutorialSaveData.isGhostCubeTutorialWatched;
    }

    public bool GetLightOnOffTutorialWatched()
    {
        return tutorialSaveData.isLightOnOffTutorialWatched;
    }

    public bool GetTimerTutorialWatched()
    {
        return tutorialSaveData.isTimerTutorialWatched;
    }

    public bool GetInvertModeTutorialWatched()
    {
        return tutorialSaveData.isInvertModeTutorialWatched;
    }

    public bool GetFallingShapesTutorialWatched()
    {
        return tutorialSaveData.isFallingShapesModeTutorialWatched;
    }

    public bool GetWindModeTutorialWatched()
    {
        return tutorialSaveData.isWindModeTutorialWatched;
    }

    public bool GetCubeLateFallTutorialWatched()
    {
        return tutorialSaveData.isCubeLateFallTutorialWatched;
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

    public void SetIsMoveAndPlayTutorialWatched()
    {
        tutorialSaveData.isMoveAndPlayTutorialWatched = true;
        SaveData();
    }

    public void SetMeetMetalAndMagnetTutorialWatched()
    {
        tutorialSaveData.isMeetMetalAndMagnetTutorialWatched = true;
        SaveData();
    }

    public void SetMeetIceCubeTutorialWatched()
    {
        tutorialSaveData.isMeetIceCubeTutorialWatched = true;
        SaveData();
    }

    public void SetMeetBombTutorialWatched()
    {
        tutorialSaveData.isMeetBombTutorialWatched = true;
        SaveData();
    }

    public void SetCubeChangerActiveTutorialWatched()
    {
        tutorialSaveData.isCubeChangerActiveTutorialWatched = true;
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

    public void SetTimerTutorialWatched()
    {
        tutorialSaveData.isTimerTutorialWatched = true;
        SaveData();
    }

    public void SetInvertModeTutorialWatched()
    {
        tutorialSaveData.isInvertModeTutorialWatched = true;
        SaveData();
    }

    public void SetFallingShapesTutorialWatched()
    {
        tutorialSaveData.isFallingShapesModeTutorialWatched = true;
        SaveData();
    }

    public void SetWindModeTutorialWatched()
    {
        tutorialSaveData.isWindModeTutorialWatched = true;
        SaveData();
    }

    public void SetCubeLateFallTutorialWatched()
    {
        tutorialSaveData.isCubeLateFallTutorialWatched = true;
        SaveData();
    }

    private void SaveData()
    {
        string tutorialDataJson = JsonUtility.ToJson(tutorialSaveData, true);
        FileUtil.SaveToFile(tutorialDataJson, tutorialSaveFile);
    }
}
