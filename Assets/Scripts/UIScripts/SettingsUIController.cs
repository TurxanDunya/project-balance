using UnityEngine;
using UnityEngine.UIElements;

public class SettingsUIController : MonoBehaviour
{
    [SerializeField] private StateChanger stateChanger;

    private MusicPlayer musicPlayer;

    private VisualElement rootElement;
    private VisualElement topVE;

    private VisualElement settingsPopUpVE;
    private VisualElement buttonsLineVE;
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

    void Start()
    {
        MakeBindings();

        SafeArea.ApplySafeArea(rootElement);
    }

    public void MakeBindings()
    {
        rootElement = GetComponent<UIDocument>()
            .rootVisualElement.Q<VisualElement>("RootVE");

        topVE = rootElement.Q<VisualElement>("topVE");
        homeButton = topVE.Q<Button>("Home");
        homeButton.clicked += () => stateChanger.ChangeStateToMainUIWithoutLoadPage();

        settingsPopUpVE = rootElement.Q<VisualElement>("SettingsPopUpVE");
        buttonsLineVE = settingsPopUpVE.Q<VisualElement>("buttons_line_ve");
        soundControlVE = buttonsLineVE.Q<VisualElement>("sound_control_ve");
        musicControlVE = buttonsLineVE.Q<VisualElement>("music_control_ve");

        soundOnVE = soundControlVE.Q<VisualElement>("sound_on_ve");
        soundOnBtn = soundOnVE.Q<Button>("sound_on_btn");
        soundOnBtn.clicked += () => MakeSoundOff();

        soundOffVE = soundControlVE.Q<VisualElement>("sound_off_ve");
        soundOffBtn = soundOffVE.Q<Button>("sound_off_btn");
        soundOffBtn.clicked += () => MakeSoundOn();

        musicUnmuteVE = musicControlVE.Q<VisualElement>("music_unmute_ve");
        musicUnmuteBtn = musicUnmuteVE.Q<Button>("music_unmute_btn");
        musicUnmuteBtn.clicked += () => MakeMusicOff();

        musicMuteVE = musicControlVE.Q<VisualElement>("music_mute_ve");
        musicMuteBtn = musicMuteVE.Q<Button>("music_mute_btn");
        musicMuteBtn.clicked += () => MakeMusicOn();

        DefineSoundButtonsState();
    }

    private void DefineSoundButtonsState()
    {
        musicPlayer = FindAnyObjectByType<MusicPlayer>();

        if(musicPlayer.GetIsSoundOn())
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

    private void MakeSoundOn()
    {
        musicPlayer.MakeSoundsOn();
        soundOffVE.style.display = DisplayStyle.None;
        soundOnVE.style.display = DisplayStyle.Flex;
    }

    private void MakeSoundOff()
    {
        musicPlayer.MakeSoundsOff();
        soundOnVE.style.display = DisplayStyle.None;
        soundOffVE.style.display = DisplayStyle.Flex;
    }

    private void MakeMusicOn()
    {
        musicPlayer.PlayMusic();
        musicMuteVE.style.display = DisplayStyle.None;
        musicUnmuteVE.style.display = DisplayStyle.Flex;
    }

    private void MakeMusicOff()
    {
        musicPlayer.MuteMusic();
        musicUnmuteVE.style.display = DisplayStyle.None;
        musicMuteVE.style.display = DisplayStyle.Flex;
    }

}
