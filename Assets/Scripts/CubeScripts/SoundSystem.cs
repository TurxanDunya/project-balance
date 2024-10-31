using UnityEngine;

public class SoundSystem : MonoBehaviour
{
    [Header("After drop properties")]
    [SerializeField] float volumeAfterDrop = 0.1f;
    private readonly float velocityThreshold = 0.1f;
    private readonly float angularVelocityThreshold = 5f;

    [SerializeField] AudioClip[] rollingSounds;
    [SerializeField] AudioClip[] platformCollisionSounds;
    [SerializeField] AudioClip[] droppedCubeCollisionSounds;

    private AudioSource sfxPlayer;
    private Rigidbody rb;

    private void Start()
    {
        sfxPlayer = GetComponent<AudioSource>();
        rb = GetComponent<Rigidbody>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        MakeInstantSound(collision);
    }

    private void OnCollisionStay(Collision collision)
    {
        MakeRollingSound(collision);
    }

    bool isPlayingRollingSound = false;
    public void MakeRollingSound(Collision collision)
    {
        bool isOtherMainPlatform = collision.collider.CompareTag(TagConstants.MAIN_PLATFORM) 
            || collision.collider.CompareTag(TagConstants.MAIN_PLATFORM_COLLIDER);

        if (isOtherMainPlatform
            && rb.velocity.magnitude >= velocityThreshold
            && rb.angularVelocity.magnitude >= angularVelocityThreshold
            && !sfxPlayer.isPlaying)
        {
            isPlayingRollingSound = true;
            PlaySfx(rollingSounds, volumeAfterDrop);
        }
        else if (rb.velocity.magnitude <= velocityThreshold
            && rb.angularVelocity.magnitude <= angularVelocityThreshold
            && sfxPlayer.isPlaying
            && isPlayingRollingSound)
        {
            sfxPlayer.Stop();
            isPlayingRollingSound = false;
        }
    }

    public void MakeInstantSound(Collision collision)
    {
        bool isThisPlayableCube = CompareTag(TagConstants.PLAYABLE_CUBE);
        bool isThisDroppedCube = CompareTag(TagConstants.DROPPED_CUBE);

        bool isOtherMainPlatform = collision.collider.CompareTag(TagConstants.MAIN_PLATFORM)
            || collision.collider.CompareTag(TagConstants.MAIN_PLATFORM_COLLIDER);
        bool isOtherDroppedCube = collision.collider.CompareTag(TagConstants.DROPPED_CUBE);

        if (isThisPlayableCube && isOtherMainPlatform)
        {
            PlaySfx(platformCollisionSounds, 1.0f);
        }
        else if (isThisDroppedCube && isOtherMainPlatform)
        {
            PlaySfx(platformCollisionSounds, volumeAfterDrop);
        }

        if (isThisPlayableCube && isOtherDroppedCube)
        {
            PlaySfx(platformCollisionSounds, 1.0f);
        }

        if (isThisDroppedCube && isOtherDroppedCube)
        {
            PlaySfx(droppedCubeCollisionSounds, volumeAfterDrop);
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
    }

}
