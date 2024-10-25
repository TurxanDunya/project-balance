using System.Collections;
using Assets.Scripts.Model;
using UnityEngine;
using UnityEngine.Localization.Settings;

public class LevelManager : MonoBehaviour
{
    public static LevelManager INSTANCE;
    public LevelManagment levelManagment;
    private SettingSaveSystem settingSaveSystem;

    private void Awake()
    {
        levelManagment = new LevelManagment();

        if (INSTANCE == null)
        {
            INSTANCE = this;
            DontDestroyOnLoad(this);
        }
        else if (INSTANCE != this) {
            Destroy(this);
        }
      
    }

    private void Start()
    {
        settingSaveSystem = GetComponent<SettingSaveSystem>();
        StartCoroutine(SetGameLanguage());
    }


    IEnumerator SetGameLanguage()
    {
        Language lang = settingSaveSystem.GetLanguageSettingData().currentLang;
        yield return LocalizationSettings.InitializationOperation;
        switch (lang)
        {
            case Language.AZ:
                LocalizationSettings.SelectedLocale = LocalizationSettings.AvailableLocales.Locales[0];
                break;
            case Language.ENG:
                LocalizationSettings.SelectedLocale = LocalizationSettings.AvailableLocales.Locales[1];
                break;
        }

    }


}
