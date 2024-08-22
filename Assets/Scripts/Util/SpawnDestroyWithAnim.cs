using UnityEngine;

public class SpawnDestroyWithAnim : MonoBehaviour
{
    [SerializeField] AnimationClip spawnAnimation;
    [SerializeField] AnimationClip destroyAnimation;
    private Animation animation;

    void Start()
    {
        animation = GetComponent<Animation>();

        if (animation == null) {
            gameObject.AddComponent<Animation>();
            animation = GetComponent<Animation>();
        }

        animation.AddClip(spawnAnimation, "spawn");
        animation.AddClip(destroyAnimation, "destroy");
        PlaySpawnAnimation();
    }

    public void PlaySpawnAnimation()
    {
        animation.Play("spawn");
    }

    public void PlayDestroyAnimation()
    {
        animation.Play("destroy");
    }

    public void SpawnAnimationComplete() { }

    public void DestroyAnimationComplete() {
        Destroy(gameObject);
    }
}
