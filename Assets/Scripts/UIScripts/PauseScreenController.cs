using UnityEngine;
using UnityEngine.UIElements;

public class PauseScreenController : MonoBehaviour
{
    [SerializeField] private StateChanger stateChanger;

    private MusicPlayer musicPlayer;

    private VisualElement rootElement;
    private VisualElement topVE;

    private VisualElement pausePopUpVE;
    private VisualElement buttonsLineVE;
    private VisualElement soundControlVE;
    private VisualElement resumeVE;
    private VisualElement musicControlVE;

    private Button homeButton;

    private Button soundOnBtn;
    private Button soundOffBtn;
    private Button musicOnBtn;
    private Button resumeBtn;
    private Button musicOffBtn;

    void Start()
    {
        rootElement = GetComponent<UIDocument>()
            .rootVisualElement.Q<VisualElement>("RootVE");

        topVE = rootElement.Q<VisualElement>("topVE");
        homeButton = topVE.Q<Button>("Home");
        homeButton.clicked += () => ChangeStateToHomePage();

        pausePopUpVE = rootElement.Q<VisualElement>("PausePopUpVE");
        buttonsLineVE = pausePopUpVE.Q<VisualElement>("buttons_line_ve");
        soundControlVE = buttonsLineVE.Q<VisualElement>("sound_control_ve");
        resumeVE = pausePopUpVE.Q<VisualElement>("resume_ve");
        musicControlVE = buttonsLineVE.Q<VisualElement>("music_control_ve");

        soundOnBtn = soundControlVE.Q<Button>("sound_on_btn");
        soundOnBtn.clicked += () => MakeSoundOff();

        soundOffBtn = soundControlVE.Q<Button>("sound_off_btn");
        soundOffBtn.clicked += () => MakeSoundOn();

        musicOnBtn = musicControlVE.Q<Button>("music_on_btn");
        musicOnBtn.clicked += () => MakeMusicOff();

        musicOffBtn = musicControlVE.Q<Button>("music_off_btn");
        musicOffBtn.clicked += () => MakeMusicOn();

        resumeBtn = resumeVE.Q<Button>("resume_btn");
        resumeBtn.clicked += () => ChangeStateToResume();

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

    public void ChangeStateToPausePage()
    {
        stateChanger.ChangeStateToPause();
    }

    private void ChangeStateToHomePage()
    {
        stateChanger.ChangeStateToMainUI();
    }

    private void ChangeStateToResume()
    {
        stateChanger.ChangeStateToResume();
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
