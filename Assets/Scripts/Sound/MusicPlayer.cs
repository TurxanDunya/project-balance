using System.Collections;
using UnityEngine;

public class MusicPlayer : BaseMusicPlayer
{
    [SerializeField] AudioClip[] musics;
    [SerializeField] AudioSource player;
    private int currentPlayingIndex;

    private SoundSaveSystem soundSaveSystem;

    private bool isMusicOn;
    private bool isSoundOn;

    void Start()
    {
        InitializeVolumeSettings();

        currentPlayingIndex = Random.Range(0, musics.Length);
        player.clip = musics[currentPlayingIndex];

        StartCoroutine(MusicQueue());
    }

    private void OnDisable()
    {
        StopAllCoroutines();
    }

    public bool GetIsMusicOn()
    {
        return isMusicOn;
    }

    public bool GetIsSoundOn()
    {
        return isSoundOn;
    }

    public void MuteMusic()
    {
        isMusicOn = false;

        SoundSettingsData soundSettingsData = new();
        soundSettingsData.isMusicOn = isMusicOn;
        soundSaveSystem.SetSoundSettingsData(soundSettingsData);
        soundSaveSystem.SaveSoundSettingsData();

        player.mute = true;
    }

    public void PlayMusic()
    {
        isMusicOn = true;

        SoundSettingsData soundSettingsData = new();
        soundSettingsData.isMusicOn = isMusicOn;
        soundSaveSystem.SetSoundSettingsData(soundSettingsData);
        soundSaveSystem.SaveSoundSettingsData();

        player.mute = false;
    }

    public void MakeSoundsOn()
    {
        AudioListener.volume = 1;

        SoundSettingsData soundSettingsData = new();
        soundSettingsData.isSoundOn = true;
        soundSaveSystem.SetSoundSettingsData(soundSettingsData);
        soundSaveSystem.SaveSoundSettingsData();
    }

    public void MakeSoundsOff()
    {
        AudioListener.volume = 0;

        SoundSettingsData soundSettingsData = new();
        soundSettingsData.isSoundOn = false;
        soundSaveSystem.SetSoundSettingsData(soundSettingsData);
        soundSaveSystem.SaveSoundSettingsData();
    }

    private void InitializeVolumeSettings()
    {
        soundSaveSystem = GetComponent<SoundSaveSystem>();
        isMusicOn = soundSaveSystem.GetSoundSettingsData().isMusicOn;
        isSoundOn = soundSaveSystem.GetSoundSettingsData().isSoundOn;

        if (isSoundOn)
        {
            AudioListener.volume = 1;
        }
        else
        {
            AudioListener.volume = 0;
        }

        if (isMusicOn)
        {
            player.Play();
        }
    }

    IEnumerator MusicQueue()
    {
        while (player.isPlaying)
        {
            yield return new WaitForSeconds(5);
        }

        if (currentPlayingIndex == musics.Length - 1)
        {
            currentPlayingIndex = 0;
        }
        else {
            currentPlayingIndex += 1;
        }

        player.clip = musics[currentPlayingIndex];
        player.Play();
        StartCoroutine(MusicQueue());
    }

}
