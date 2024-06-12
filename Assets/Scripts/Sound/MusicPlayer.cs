using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Unity.VisualScripting.Member;

public class MusicPlayer : BaseMusicPlayer
{

    [SerializeField] AudioClip[] musics;
    [SerializeField] AudioSource player;
    private int currentPlayingIndex;


    void Start()
    {
        currentPlayingIndex = Random.Range(0, musics.Length);
        player.clip = musics[currentPlayingIndex];
        player.Play();

        StartCoroutine(MusicQueue());


    }

    IEnumerator MusicQueue()
    {
       
        while (player.isPlaying)
        {
            yield return null;
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
