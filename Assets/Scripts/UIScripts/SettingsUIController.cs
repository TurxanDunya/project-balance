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

    private Button soundOnBtn;
    private Button soundOffBtn;
    private Button musicOnBtn;
    private Button musicOffBtn;

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

        soundOnBtn = soundControlVE.Q<Button>("sound_on_btn");
        soundOnBtn.clicked += () => MakeSoundOff();

        soundOffBtn = soundControlVE.Q<Button>("sound_off_btn");
        soundOffBtn.clicked += () => MakeSoundOn();

        musicOnBtn = musicControlVE.Q<Button>("music_on_btn");
        musicOnBtn.clicked += () => MakeMusicOff();

        musicOffBtn = musicControlVE.Q<Button>("music_off_btn");
        musicOffBtn.clicked += () => MakeMusicOn();

        DefineSoundButtonsState();
    }

    private void DefineSoundButtonsState()
    {
        musicPlayer = FindAnyObjectByType<MusicPlayer>();

        if(musicPlayer.GetIsSoundOn())
        {
            soundOffBtn.style.display = DisplayStyle.None;
            soundOnBtn.style.display = DisplayStyle.Flex;
        }
        else
        {
            soundOffBtn.style.display = DisplayStyle.Flex;
            soundOnBtn.style.display = DisplayStyle.None;
        }

        if (musicPlayer.GetIsMusicOn())
        {
            musicOffBtn.style.display = DisplayStyle.None;
            musicOnBtn.style.display = DisplayStyle.Flex;
        }
        else
        {
            musicOffBtn.style.display = DisplayStyle.Flex;
            musicOnBtn.style.display = DisplayStyle.None;
        }
    }

    private void MakeSoundOn()
    {
        musicPlayer.MakeSoundsOn();
        soundOffBtn.style.display = DisplayStyle.None;
        soundOnBtn.style.display = DisplayStyle.Flex;
    }

    private void MakeSoundOff()
    {
        musicPlayer.MakeSoundsOff();
        soundOnBtn.style.display = DisplayStyle.None;
        soundOffBtn.style.display = DisplayStyle.Flex;
    }

    private void MakeMusicOn()
    {
        musicPlayer.PlayMusic();
        musicOffBtn.style.display = DisplayStyle.None;
        musicOnBtn.style.display = DisplayStyle.Flex;
    }

    private void MakeMusicOff()
    {
        musicPlayer.PauseMusic();
        musicOnBtn.style.display = DisplayStyle.None;
        musicOffBtn.style.display = DisplayStyle.Flex;
    }

}
