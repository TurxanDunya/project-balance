using UnityEngine;
using UnityEngine.UIElements;

public class PauseScreenController : MonoBehaviour, IControllerTemplate
{
    private static readonly ILogger logger = Debug.unityLogger;

    [SerializeField] private StateChanger stateChanger;

    private MusicPlayer musicPlayer;

    private VisualElement rootElement;
    private VisualElement topVE;

    private VisualElement pausePopUpVE;
    private VisualElement buttonsLineVE;
    private VisualElement soundControlVE;
    private VisualElement resumeVE;
    private VisualElement restartVE;
    private VisualElement musicControlVE;

    private Button homeButton;

    private VisualElement soundOnVE;
    private VisualElement soundOffVE;
    private VisualElement musicMuteVE;
    private VisualElement musicUnmuteVE;

    private Button soundOnBtn;
    private Button soundOffBtn;
    private Button musicUnmuteBtn;
    private Button resumeBtn;
    private Button restartBtn;
    private Button musicMuteBtn;

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
        restartVE = pausePopUpVE.Q<VisualElement>("restart_ve");
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

        resumeBtn = resumeVE.Q<Button>("resume_btn");
        resumeBtn.clicked += () => ChangeStateToResume();

        restartBtn = restartVE.Q<Button>("restart_btn");
        restartBtn.clicked += () => ReloadLevel();

        DefineSoundButtonsState();

        SafeArea.ApplySafeArea(rootElement);
    }

    private void DefineSoundButtonsState()
    {
        musicPlayer = FindAnyObjectByType<MusicPlayer>();

        if(!musicPlayer)
        {
            logger.Log(LogType.Warning,
                "MusicPlayer object was null, " +
                "start from LOAD SCREEN to use full functionlity");
            return;
        }

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

    public void ChangeStateToPausePage()
    {
        stateChanger.ChangeStateToPause();
    }

    private void ChangeStateToHomePage()
    {
        stateChanger.ChangeStateToHome();
    }

    private void ChangeStateToResume()
    {
        stateChanger.ChangeStateToResume();
    }

    private void ReloadLevel()
    {
        stateChanger.ChangeStateToHome();
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

    public void SetDisplayFlex()
    {
        rootElement.style.display = DisplayStyle.Flex;
    }

    public void SetDisplayNone()
    {
        rootElement.style.display = DisplayStyle.None;
    }

    public bool IsOverUI()
    {
        return rootElement.style.display == DisplayStyle.Flex;
    }

}
