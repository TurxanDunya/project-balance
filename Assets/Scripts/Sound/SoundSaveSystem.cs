using UnityEngine;

public class SoundSaveSystem : MonoBehaviour
{
    private readonly string settingsSaveFile = "settings.dat";
    private SettingsData settingsData;

    private void OnEnable()
    {
        string settingsSaveData = FileUtil.LoadFromFile(settingsSaveFile);

        if (settingsSaveData != null)
        {
            settingsData = JsonUtility.FromJson<SettingsData>(settingsSaveData);
        }
        else
        {
            SoundSettingsData soundSettingsData = new();
            soundSettingsData.isSoundOn = true;
            soundSettingsData.isMusicOn = true;

            settingsData = new();
            settingsData.soundSettingsData = soundSettingsData;
            SaveData();
        }
    }

    public SoundSettingsData GetSoundSettingsData()
    {
        return settingsData.soundSettingsData;
    }

    public void SetSoundSettingsData(SoundSettingsData soundSettingsData)
    {
        settingsData.soundSettingsData = soundSettingsData;
    }

    public void SaveSoundSettingsData()
    {
        SaveData();
    }

    private void SaveData()
    {
        string settingsJson = JsonUtility.ToJson(settingsData, true);
        FileUtil.SaveToFile(settingsJson, settingsSaveFile);
    }

}
