using UnityEngine;

public class Coin : MonoBehaviour
{
    private CoinSpawnManager coinSpawnManager;

    private void Start()
    {
        coinSpawnManager = FindObjectOfType<CoinSpawnManager>();
    }

    private void OnEnable()
    {
        Platform.PlayableCubeLanded += NewCoin;
    }

    private void OnDisable()
    {
        Platform.PlayableCubeLanded -= NewCoin;
    }

    private void NewCoin()
    {
        coinSpawnManager.RemoveCurrentCoin();
        coinSpawnManager.SpawnNewCoinByChance();
    }

}
