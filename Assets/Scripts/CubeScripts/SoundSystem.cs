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

    public void MakeSound(Collision collision)
    {
        bool isThisPlayableCube = CompareTag(TagConstants.PLAYABLE_CUBE);
        bool isThisMagnet = CompareTag(TagConstants.MAGNET);
        bool isThisDroppedCube = CompareTag(TagConstants.DROPPED_CUBE);

        bool isOtherMainPlatform = collision.collider.CompareTag(TagConstants.MAIN_PLATFORM);
        bool isOtherDroppedCube = collision.collider.CompareTag(TagConstants.DROPPED_CUBE);

        if (isThisPlayableCube && isOtherMainPlatform)
        {
            int soundIndex = Random.Range(0, platformCollisionSounds.Length);
            fallSFXPlayer.clip = platformCollisionSounds[soundIndex];
            PlayFallSfx();

            tag = TagConstants.DROPPED_CUBE;
            return;
        }
        else if (isThisDroppedCube && isOtherMainPlatform)
        {
            int soundIndex = Random.Range(0, platformCollisionSounds.Length);
            fallSFXPlayer.clip = platformCollisionSounds[soundIndex];
            fallSFXPlayer.volume = 0.1f;
            PlayFallSfx();
            return;
        }

        if (isThisPlayableCube && isOtherDroppedCube)
        {
            int soundIndex = Random.Range(0, platformCollisionSounds.Length);
            fallSFXPlayer.clip = platformCollisionSounds[soundIndex];
            PlayFallSfx();

            tag = TagConstants.DROPPED_CUBE;
            return;
        }

        if (isThisDroppedCube && isOtherDroppedCube)
        {
            int soundIndex = Random.Range(0, droppedCubeCollisionSounds.Length);
            fallSFXPlayer.clip = droppedCubeCollisionSounds[soundIndex];
            fallSFXPlayer.volume = 0.1f;
            PlayFallSfx();

            tag = TagConstants.DROPPED_CUBE;
            return;
        }

        if (isThisMagnet && isOtherMainPlatform)
        {
            int soundIndex = Random.Range(0, platformCollisionSounds.Length);
            fallSFXPlayer.clip = platformCollisionSounds[soundIndex];
            PlayFallSfx();
            return;
        }
    }

    private void PlayFallSfx()
    {
        fallSFXPlayer.Play();
    }

}
