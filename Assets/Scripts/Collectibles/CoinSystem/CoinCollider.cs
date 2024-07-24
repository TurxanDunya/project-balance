using UnityEngine;

public class CoinCollider : MonoBehaviour
{
    [SerializeField] private int addCountOnCubeCollision;

    private CoinManager coinManager;

    private void Start()
    {
        coinManager = FindObjectOfType<CoinManager>();
    }

    private void OnTriggerEnter(Collider collider)
    {
        if (collider.CompareTag(TagConstants.PLAYABLE_CUBE))
        {
            coinManager.AddCoin(addCountOnCubeCollision);
        }
    }

}
