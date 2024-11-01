using UnityEngine;

public class SpawnDestroyWithAnim : MonoBehaviour
{
    [SerializeField] AnimationClip spawnAnimation;
    [SerializeField] AnimationClip destroyAnimation;

    private Animation animationComponent;

    void Start()
    {
        animationComponent = GetComponent<Animation>();

        if (animationComponent == null) {
            gameObject.AddComponent<Animation>();
            animationComponent = GetComponent<Animation>();
        }

        animationComponent.AddClip(spawnAnimation, "spawn");
        animationComponent.AddClip(destroyAnimation, "destroy");
        PlaySpawnAnimation();
    }

    public void PlaySpawnAnimation()
    {
        animationComponent.Play("spawn");
    }

    public void PlayDestroyAnimation()
    {
        animationComponent.Play("destroy");
    }

    public void SpawnAnimationComplete() { }

    public void DestroyAnimationComplete() {
        Destroy(gameObject);
    }
}
