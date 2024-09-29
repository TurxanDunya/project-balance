using UnityEngine;

public class CoinCollider : MonoBehaviour
{
    [SerializeField] private int addCountOnCubeCollision;

    private CoinManager coinManager;
    private CoinSpawnManager coinSpawnManager;

    private void Start()
    {
        coinManager = FindObjectOfType<CoinManager>();
        coinSpawnManager = FindAnyObjectByType<CoinSpawnManager>();
    }

    private void OnTriggerEnter(Collider collider)
    {
        if (collider.CompareTag(TagConstants.PLAYABLE_CUBE))
        {
            coinManager.AddCoin(addCountOnCubeCollision);
        }
        else
        {
            coinSpawnManager.RemoveCurrentCoin();
        }
    }

}
