using UnityEngine;

public class SoundSystem : MonoBehaviour
{
    [SerializeField] AudioClip[] platformCollisionSounds;
    [SerializeField] AudioClip[] droppedCubeCollisionSounds;

    private AudioSource fallSFXPlayer;

    private void Start()
    {
        fallSFXPlayer = GetComponent<AudioSource>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        bool isPlayableCube = CompareTag(TagConstants.PLAYABLE_CUBE);
        bool isMagnet = CompareTag(TagConstants.MAGNET);
        bool isMainPlatform = collision.collider.CompareTag(TagConstants.MAIN_PLATFORM);
        bool isDroppedCube = CompareTag(TagConstants.DROPPED_CUBE);

        if ((isPlayableCube || isMagnet) && isMainPlatform)
        {
            int soundIndex = Random.Range(0, platformCollisionSounds.Length);
            fallSFXPlayer.clip = platformCollisionSounds[soundIndex];
            PlayFallSfx();

            tag = TagConstants.DROPPED_CUBE;
        }
        else if (isDroppedCube && isMainPlatform)
        {
            int soundIndex = Random.Range(0, platformCollisionSounds.Length);
            fallSFXPlayer.clip = platformCollisionSounds[soundIndex];
            fallSFXPlayer.volume = 0.1f;
            PlayFallSfx();
        }

        if (isPlayableCube && isDroppedCube)
        {
            int soundIndex = Random.Range(0, droppedCubeCollisionSounds.Length);
            fallSFXPlayer.clip = droppedCubeCollisionSounds[soundIndex];
            PlayFallSfx();

            tag = TagConstants.DROPPED_CUBE;
        }
    }

    private void PlayFallSfx()
    {
        fallSFXPlayer.Play();
    }

}
