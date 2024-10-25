using UnityEngine;

public class SettingSaveSystem : MonoBehaviour
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

        if (settingsData == null || settingsData.languageSettingData == null)
        {
            LanguageSettingData languageSettingData = new();
            languageSettingData.currentLang = Assets.Scripts.Model.Language.ENG;

            if (settingsData == null)
            {
                settingsData = new();
            }

            settingsData.languageSettingData = languageSettingData;
            SaveData();
        }
    }

    public LanguageSettingData GetLanguageSettingData()
    {
        return settingsData.languageSettingData;
    }

    public void SetLanguageSettingData(LanguageSettingData languageSettingData)
    {
        settingsData.languageSettingData = languageSettingData;
    }

    public void SaveLanguageSettingData()
    {
        SaveData();
    }

    private void SaveData()
    {
        string settingsJson = JsonUtility.ToJson(settingsData, true);
        FileUtil.SaveToFile(settingsJson, settingsSaveFile);
    }

}
