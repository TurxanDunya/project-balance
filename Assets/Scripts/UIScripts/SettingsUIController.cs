using Assets.Scripts.Model;
using UnityEngine;
using UnityEngine.Localization.Settings;
using UnityEngine.UIElements;

public class SettingsUIController : MonoBehaviour, IControllerTemplate
{
    private static readonly ILogger logger = Debug.unityLogger;

    [SerializeField] private StateChanger stateChanger;

    private SettingSaveSystem settingSaveSystem;
    private MusicPlayer musicPlayer;

    private VisualElement rootElement;
    private VisualElement topVE;

    private VisualElement settingsPopUpVE;
    private VisualElement buttonsLineVE;
    private VisualElement localizationVE;
    private VisualElement soundControlVE;
    private VisualElement musicControlVE;

    private Button homeButton;

    private VisualElement soundOnVE;
    private VisualElement soundOffVE;
    private VisualElement musicMuteVE;
    private VisualElement musicUnmuteVE;

    private Button soundOnBtn;
    private Button soundOffBtn;
    private Button musicUnmuteBtn;
    private Button musicMuteBtn;

    private RadioButton azeRbtn;
    private RadioButton engRbtn;

    void Start()
    {
        musicPlayer = FindAnyObjectByType<MusicPlayer>();

        settingSaveSystem = GetComponent<SettingSaveSystem>();
        MakeBindings();

        SafeArea.ApplySafeArea(rootElement);
    }

    private void OnDisable()
    {
        homeButton.UnregisterCallback<PointerDownEvent>(ChangeStateToMainUIWithoutLoadPage);
        homeButton.UnregisterCallback<PointerUpEvent>(ChangeStateToMainUIWithoutLoadPage);

        soundOnBtn.UnregisterCallback<PointerDownEvent>(MakeSoundOff);
        soundOnBtn.UnregisterCallback<PointerUpEvent>(MakeSoundOff);

        soundOffBtn.UnregisterCallback<PointerDownEvent>(MakeSoundOn);
        soundOffBtn.UnregisterCallback<PointerUpEvent>(MakeSoundOn);

        musicUnmuteBtn.UnregisterCallback<PointerDownEvent>(MakeMusicOff);
        musicUnmuteBtn.UnregisterCallback<PointerUpEvent>(MakeMusicOff);

        musicMuteBtn.UnregisterCallback<PointerDownEvent>(MakeMusicOn);
        musicMuteBtn.UnregisterCallback<PointerUpEvent>(MakeMusicOn);

        azeRbtn.UnregisterCallback<ClickEvent>(evt => ChangeLanguageToAze());
        engRbtn.UnregisterCallback<ClickEvent>(evt => ChangeLanguageToEng());
    }

    public void MakeBindings()
    {
        rootElement = GetComponent<UIDocument>()
            .rootVisualElement.Q<VisualElement>("RootVE");

        topVE = rootElement.Q<VisualElement>("topVE");
        homeButton = topVE.Q<Button>("Home");
        homeButton.RegisterCallback<PointerDownEvent>(ChangeStateToMainUIWithoutLoadPage);
        homeButton.RegisterCallback<PointerUpEvent>(ChangeStateToMainUIWithoutLoadPage);

        settingsPopUpVE = rootElement.Q<VisualElement>("SettingsPopUpVE");
        buttonsLineVE = settingsPopUpVE.Q<VisualElement>("buttons_line_ve");
        localizationVE = settingsPopUpVE.Q<VisualElement>("localization_ve");
        soundControlVE = buttonsLineVE.Q<VisualElement>("sound_control_ve");
        musicControlVE = buttonsLineVE.Q<VisualElement>("music_control_ve");

        soundOnVE = soundControlVE.Q<VisualElement>("sound_on_ve");
        soundOnBtn = soundOnVE.Q<Button>("sound_on_btn");
        soundOnBtn.RegisterCallback<PointerDownEvent>(MakeSoundOff);
        soundOnBtn.RegisterCallback<PointerUpEvent>(MakeSoundOff);

        soundOffVE = soundControlVE.Q<VisualElement>("sound_off_ve");
        soundOffBtn = soundOffVE.Q<Button>("sound_off_btn");
        soundOffBtn.RegisterCallback<PointerDownEvent>(MakeSoundOn);
        soundOffBtn.RegisterCallback<PointerUpEvent>(MakeSoundOn);

        musicUnmuteVE = musicControlVE.Q<VisualElement>("music_unmute_ve");
        musicUnmuteBtn = musicUnmuteVE.Q<Button>("music_unmute_btn");
        musicUnmuteBtn.RegisterCallback<PointerDownEvent>(MakeMusicOff);
        musicUnmuteBtn.RegisterCallback<PointerUpEvent>(MakeMusicOff);

        musicMuteVE = musicControlVE.Q<VisualElement>("music_mute_ve");
        musicMuteBtn = musicMuteVE.Q<Button>("music_mute_btn");
        musicMuteBtn.RegisterCallback<PointerDownEvent>(MakeMusicOn);
        musicMuteBtn.RegisterCallback<PointerUpEvent>(MakeMusicOn);

        azeRbtn = localizationVE.Q<RadioButton>("aze_rbtn");
        engRbtn = localizationVE.Q<RadioButton>("eng_rbtn");
        azeRbtn.RegisterCallback<ClickEvent>(evt => ChangeLanguageToAze());
        engRbtn.RegisterCallback<ClickEvent>(evt => ChangeLanguageToEng());

        DefineSoundButtonsState();
        DefineLanguageSettingState();
    }

    private void DefineSoundButtonsState()
    {
        if (!musicPlayer)
        {
            logger.Log(LogType.Warning,
                "MusicPlayer object was null, " +
                "start from LOAD SCREEN to use full functionlity");
            return;
        }

        if (musicPlayer.GetIsSoundOn())
        {
            soundOffVE.style.display = DisplayStyle.None;
            soundOnVE.style.display = DisplayStyle.Flex;
        }
        else
        {
            soundOffVE.style.display = DisplayStyle.Flex;
            soundOnVE.style.display = DisplayStyle.None;
        }

        if (musicPlayer.GetIsMusicOn())
        {
            musicMuteVE.style.display = DisplayStyle.None;
            musicUnmuteVE.style.display = DisplayStyle.Flex;
        }
        else
        {
            musicMuteVE.style.display = DisplayStyle.Flex;
            musicUnmuteVE.style.display = DisplayStyle.None;
        }
    }

    private void DefineLanguageSettingState()
    {
        Language lang = settingSaveSystem.GetLanguageSettingData().currentLang;

        switch (lang) {
            case Language.AZ:
                azeRbtn.SetValueWithoutNotify(true);
                engRbtn.SetValueWithoutNotify(false);
                break;
            case Language.ENG:
                azeRbtn.SetValueWithoutNotify(false);
                engRbtn.SetValueWithoutNotify(true);
                break;
            default:
                throw new System.Exception("Unsupported lang");
        }
    }

    private void ChangeStateToMainUIWithoutLoadPage(PointerDownEvent ev)
    {
        InputManager.isOverUI = true;
    }

    private void ChangeStateToMainUIWithoutLoadPage(PointerUpEvent ev)
    {
        stateChanger.ChangeStateToMainUIWithoutLoadPage();
        InputManager.isOverUI = false;
    }

    private void MakeSoundOn(PointerDownEvent ev)
    {
        InputManager.isOverUI = true;
    }

    private void MakeSoundOn(PointerUpEvent ev)
    {
        musicPlayer.MakeSoundsOn();
        soundOffVE.style.display = DisplayStyle.None;
        soundOnVE.style.display = DisplayStyle.Flex;
        InputManager.isOverUI = false;
    }

    private void MakeSoundOff(PointerDownEvent ev)
    {
        InputManager.isOverUI = true;
    }

    private void MakeSoundOff(PointerUpEvent ev)
    {
        musicPlayer.MakeSoundsOff();
        soundOnVE.style.display = DisplayStyle.None;
        soundOffVE.style.display = DisplayStyle.Flex;
        InputManager.isOverUI = false;
    }

    private void MakeMusicOn(PointerDownEvent ev)
    {
        InputManager.isOverUI = true;
    }

    private void MakeMusicOn(PointerUpEvent ev)
    {
        musicPlayer.PlayMusic();
        musicMuteVE.style.display = DisplayStyle.None;
        musicUnmuteVE.style.display = DisplayStyle.Flex;
        InputManager.isOverUI = false;
    }

    private void MakeMusicOff(PointerDownEvent ev)
    {
        InputManager.isOverUI = true;
    }

    private void MakeMusicOff(PointerUpEvent ev)
    {
        musicPlayer.MuteMusic();
        musicUnmuteVE.style.display = DisplayStyle.None;
        musicMuteVE.style.display = DisplayStyle.Flex;
        InputManager.isOverUI = false;
    }

    private void ChangeLanguageToAze()
    {
        SaveLangSetting(Language.AZ);
        LocalizationSettings.SelectedLocale = LocalizationSettings.AvailableLocales.Locales[0];
    }

    private void ChangeLanguageToEng()
    {
        SaveLangSetting(Language.ENG);
        LocalizationSettings.SelectedLocale = LocalizationSettings.AvailableLocales.Locales[1];
    }

    private void SaveLangSetting(Language newLang)
    {
        LanguageSettingData languageSettingData = settingSaveSystem.GetLanguageSettingData();
        languageSettingData.currentLang = newLang;
        settingSaveSystem.SetLanguageSettingData(languageSettingData);
        settingSaveSystem.SaveLanguageSettingData();
    }

    public void SetDisplayFlex()
    {
        rootElement.style.display = DisplayStyle.Flex;
    }

    public void SetDisplayNone()
    {
        rootElement.style.display = DisplayStyle.None;
    }

}
