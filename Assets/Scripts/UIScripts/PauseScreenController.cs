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

        homeButton.RegisterCallback<PointerDownEvent>(ChangeStateToHomePage, TrickleDown.TrickleDown);
        homeButton.RegisterCallback<PointerUpEvent>(ChangeStateToHomePage);

        pausePopUpVE = rootElement.Q<VisualElement>("PausePopUpVE");
        buttonsLineVE = pausePopUpVE.Q<VisualElement>("buttons_line_ve");
        soundControlVE = buttonsLineVE.Q<VisualElement>("sound_control_ve");
        resumeVE = pausePopUpVE.Q<VisualElement>("resume_ve");
        restartVE = pausePopUpVE.Q<VisualElement>("restart_ve");
        musicControlVE = buttonsLineVE.Q<VisualElement>("music_control_ve");

        soundOnVE = soundControlVE.Q<VisualElement>("sound_on_ve");
        soundOnBtn = soundOnVE.Q<Button>("sound_on_btn");

        soundOnBtn.RegisterCallback<PointerDownEvent>(MakeSoundOff, TrickleDown.TrickleDown);
        soundOnBtn.RegisterCallback<PointerUpEvent>(MakeSoundOff);

        soundOffVE = soundControlVE.Q<VisualElement>("sound_off_ve");
        soundOffBtn = soundOffVE.Q<Button>("sound_off_btn");

        soundOffBtn.RegisterCallback<PointerDownEvent>(MakeSoundOn, TrickleDown.TrickleDown);
        soundOffBtn.RegisterCallback<PointerUpEvent>(MakeSoundOn);

        musicUnmuteVE = musicControlVE.Q<VisualElement>("music_unmute_ve");
        musicUnmuteBtn = musicUnmuteVE.Q<Button>("music_unmute_btn");

        musicUnmuteBtn.RegisterCallback<PointerDownEvent>(MakeMusicOff, TrickleDown.TrickleDown);
        musicUnmuteBtn.RegisterCallback<PointerUpEvent>(MakeMusicOff);

        musicMuteVE = musicControlVE.Q<VisualElement>("music_mute_ve");
        musicMuteBtn = musicMuteVE.Q<Button>("music_mute_btn");

        musicMuteBtn.RegisterCallback<PointerDownEvent>(MakeMusicOn, TrickleDown.TrickleDown);
        musicMuteBtn.RegisterCallback<PointerUpEvent>(MakeMusicOn);

        resumeBtn = resumeVE.Q<Button>("resume_btn");

        resumeBtn.RegisterCallback<PointerDownEvent>(ChangeStateToResume, TrickleDown.TrickleDown);
        resumeBtn.RegisterCallback<PointerUpEvent>(ChangeStateToResume);

        restartBtn = restartVE.Q<Button>("restart_btn");

        restartBtn.RegisterCallback<PointerDownEvent>(ReloadLevel, TrickleDown.TrickleDown);
        restartBtn.RegisterCallback<PointerUpEvent>(ReloadLevel);

        DefineSoundButtonsState();

        SafeArea.ApplySafeArea(rootElement);
    }

    private void OnDisable()
    {
        homeButton.UnregisterCallback<PointerDownEvent>(ChangeStateToHomePage);
        homeButton.UnregisterCallback<PointerUpEvent>(ChangeStateToHomePage);

        soundOnBtn.UnregisterCallback<PointerDownEvent>(MakeSoundOff);
        soundOnBtn.UnregisterCallback<PointerUpEvent>(MakeSoundOff);

        soundOffBtn.UnregisterCallback<PointerDownEvent>(MakeSoundOn);
        soundOffBtn.UnregisterCallback<PointerUpEvent>(MakeSoundOn);

        musicUnmuteBtn.UnregisterCallback<PointerDownEvent>(MakeMusicOff);
        musicUnmuteBtn.UnregisterCallback<PointerUpEvent>(MakeMusicOff);

        musicMuteBtn.UnregisterCallback<PointerDownEvent>(MakeMusicOn);
        musicMuteBtn.UnregisterCallback<PointerUpEvent>(MakeMusicOn);

        resumeBtn.UnregisterCallback<PointerDownEvent>(ChangeStateToResume);
        resumeBtn.UnregisterCallback<PointerUpEvent>(ChangeStateToResume);

        restartBtn.UnregisterCallback<PointerDownEvent>(ReloadLevel);
        restartBtn.UnregisterCallback<PointerUpEvent>(ReloadLevel);
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

    private void ChangeStateToHomePage(PointerDownEvent ev)
    {
        InputManager.isOverUI = true;
    }

    private void ChangeStateToHomePage(PointerUpEvent ev)
    {
        stateChanger.ChangeStateToHome();
        InputManager.isOverUI = false;
    }

    private void ChangeStateToResume(PointerDownEvent ev)
    {
        InputManager.isOverUI = true;
    }

    private void ChangeStateToResume(PointerUpEvent ev)
    {
        stateChanger.ChangeStateToResume();
        InputManager.isOverUI = false;
    }

    private void ReloadLevel(PointerDownEvent ev)
    {
        InputManager.isOverUI = true;
    }

    private void ReloadLevel(PointerUpEvent ev)
    {
        stateChanger.ChangeStateToHome();
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

    public void SetDisplayFlex()
    {
        rootElement.style.display = DisplayStyle.Flex;
    }

    public void SetDisplayNone()
    {
        rootElement.style.display = DisplayStyle.None;
    }

}
