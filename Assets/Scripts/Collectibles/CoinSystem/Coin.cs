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
        Platform.CubeLanded += NewCoin;
    }

    private void OnDisable()
    {
        Platform.CubeLanded -= NewCoin;
    }

    private void NewCoin()
    {
        coinSpawnManager.SpawnNewCoinByChance();
    }

}
