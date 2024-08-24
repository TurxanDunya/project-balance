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

        if (isPlayableCube && collision.collider.CompareTag(TagConstants.MAIN_PLATFORM))
        {
            int soundIndex = Random.Range(0, platformCollisionSounds.Length);
            fallSFXPlayer.clip = platformCollisionSounds[soundIndex];
            PlayFallSfx();

            tag = TagConstants.DROPPED_CUBE;
        }
        else if (!isPlayableCube && collision.collider.CompareTag(TagConstants.MAIN_PLATFORM))
        {
            int soundIndex = Random.Range(0, platformCollisionSounds.Length);
            fallSFXPlayer.clip = platformCollisionSounds[soundIndex];
            fallSFXPlayer.volume = 0.25f;
            PlayFallSfx();
        }

        if (isPlayableCube && collision.collider.CompareTag(TagConstants.DROPPED_CUBE))
        {
            int soundIndex = Random.Range(0, droppedCubeCollisionSounds.Length);
            fallSFXPlayer.clip = droppedCubeCollisionSounds[soundIndex];
            PlayFallSfx();

            tag = TagConstants.DROPPED_CUBE;
        }
        else if (!isPlayableCube && collision.collider.CompareTag(TagConstants.MAIN_PLATFORM))
        {
            int soundIndex = Random.Range(0, droppedCubeCollisionSounds.Length);
            fallSFXPlayer.clip = droppedCubeCollisionSounds[soundIndex];
            fallSFXPlayer.volume = 0.25f;
            PlayFallSfx();
        }
    }

    private void PlayFallSfx()
    {
        fallSFXPlayer.Play();
    }

}
