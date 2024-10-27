using UnityEngine;

public class MagnetSound : MonoBehaviour
{
    [SerializeField] AudioClip magnetSound;
    [SerializeField] AudioClip[] platformCollisionSounds;
    [SerializeField] AudioClip[] droppedCubeCollisionSounds;

    private AudioSource sfxPlayer;
    private Magnet magnet;

    private void Start()
    {
        magnet = GetComponent<Magnet>();

        sfxPlayer = GetComponent<AudioSource>();
        PlaySfx(new[] { magnetSound }, 0.5f);
    }

    private void OnCollisionEnter(Collision collision)
    {
        MakeInstantSound(collision);
    }

    public void MakeInstantSound(Collision collision)
    {
        bool isOtherDroppedCube = collision.collider.CompareTag(TagConstants.DROPPED_CUBE);
        bool isOtherMainPlatform = collision.collider.CompareTag(TagConstants.MAIN_PLATFORM);

        if (isOtherMainPlatform)
        {
            PlaySfx(platformCollisionSounds, 1.0f);
        }

        if (isOtherDroppedCube)
        {
            PlaySfx(new[] { magnetSound }, 0.1f);
        }
    }

    private void PlaySfx(AudioClip[] soundArray, float volume)
    {
        if (soundArray.Length == 0)
        {
            return;
        }

        int soundIndex = Random.Range(0, soundArray.Length);
        sfxPlayer.clip = soundArray[soundIndex];
        sfxPlayer.volume = volume;
        sfxPlayer.Play();
        magnet.PlayVFXEffect();
    }

}
