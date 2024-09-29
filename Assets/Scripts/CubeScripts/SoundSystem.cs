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
        MakeSound(collision);
    }

    private Quaternion lastRotation;
    private float rotationThreshold = 90f;
    private void OnCollisionStay(Collision collision)
    {
        bool isOtherMainPlatform = collision.collider.CompareTag(TagConstants.MAIN_PLATFORM);

        float angleDifference = Quaternion.Angle(lastRotation, transform.rotation);

        if (isOtherMainPlatform && angleDifference >= rotationThreshold)
        {
            lastRotation = transform.rotation;

            int soundIndex = Random.Range(0, platformCollisionSounds.Length);
            fallSFXPlayer.clip = platformCollisionSounds[soundIndex];
            fallSFXPlayer.volume = 0.1f;
            PlayFallSfx();
        }
    }

    public void MakeSound(Collision collision)
    {
        lastRotation = transform.rotation;

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
        }
        else if (isThisDroppedCube && isOtherMainPlatform)
        {
            int soundIndex = Random.Range(0, platformCollisionSounds.Length);
            fallSFXPlayer.clip = platformCollisionSounds[soundIndex];
            fallSFXPlayer.volume = 0.1f;
            PlayFallSfx();
        }

        if (isThisPlayableCube && isOtherDroppedCube)
        {
            int soundIndex = Random.Range(0, platformCollisionSounds.Length);
            fallSFXPlayer.clip = platformCollisionSounds[soundIndex];
            PlayFallSfx();
        }

        if (isThisDroppedCube && isOtherDroppedCube)
        {
            int soundIndex = Random.Range(0, droppedCubeCollisionSounds.Length);
            fallSFXPlayer.clip = droppedCubeCollisionSounds[soundIndex];
            fallSFXPlayer.volume = 0.1f;
            PlayFallSfx();
        }

        if (isThisMagnet && isOtherMainPlatform)
        {
            int soundIndex = Random.Range(0, platformCollisionSounds.Length);
            fallSFXPlayer.clip = platformCollisionSounds[soundIndex];
            PlayFallSfx();
        }

        tag = TagConstants.DROPPED_CUBE;
    }

    private void PlayFallSfx()
    {
        fallSFXPlayer.Play();
    }

}
