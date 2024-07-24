using UnityEngine;

public class Coin : MonoBehaviour
{
    [SerializeField] private int addCountOnCubeCollision;

    private CoinManager coinManager;
    private CoinSpawnManager coinSpawnManager;

    private void Start()
    {
        coinManager = FindObjectOfType<CoinManager>();
        coinSpawnManager = FindObjectOfType<CoinSpawnManager>();
    }

    private void OnEnable()
    {
        Platform.CubeLanded += NewCoin;
    }

    private void OnDisable()
    {
        Platform.CubeLanded -= NewCoin;
    }

    private void OnTriggerEnter(Collider collider)
    {
        if (collider.CompareTag(TagConstants.PLAYABLE_CUBE))
        {
            coinManager.AddCoin(addCountOnCubeCollision);
        }
    }

    private void NewCoin()
    {
        Debug.Log("5");
        coinSpawnManager.RemoveCurrentCoin();
        coinSpawnManager.SpawnNewCoinByChance();
    }

}
