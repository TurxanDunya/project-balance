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
        if (collider.CompareTag(Constants.PLAYABLE_CUBE))
        {
            coinManager.AddCoin(addCountOnCubeCollision);
        }
    }

    private void NewCoin()
    {
        coinSpawnManager.RemoveCurrentCoin();
        coinSpawnManager.SpawnNewCoin();
    }

}
