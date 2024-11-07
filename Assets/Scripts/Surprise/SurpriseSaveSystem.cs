using UnityEngine;

public class SurpriseSaveSystem : MonoBehaviour
{
    private readonly string surpriseSaveFile = "surprises.dat";
    private SurpriseData surpriseData;

    private void OnEnable()
    {
        string surpriseSaveData = FileUtil.LoadFromFile(surpriseSaveFile);

        if (surpriseSaveData != null)
        {
            surpriseData = JsonUtility.FromJson<SurpriseData>(surpriseSaveData);
        }

        if (surpriseData == null)
        {
            surpriseData = new();
            surpriseData.isSurpriseStarWatched = false;
            SaveData();
        }
    }

    public SurpriseData GetSurpriseData()
    {
        return surpriseData;
    }

    public void SetSurpriseData(SurpriseData surpriseData)
    {
        this.surpriseData = surpriseData;
    }

    public void SaveSurpriseData()
    {
        SaveData();
    }

    private void SaveData()
    {
        string surpriseJson = JsonUtility.ToJson(surpriseData, true);
        FileUtil.SaveToFile(surpriseJson, surpriseSaveFile);
    }
}
