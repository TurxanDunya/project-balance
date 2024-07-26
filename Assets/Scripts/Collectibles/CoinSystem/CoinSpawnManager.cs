using UnityEngine;

public class CoinSpawnManager : MonoBehaviour
{
    [SerializeField] private GameObject platformObj;
    [SerializeField] private GameObject coinPrefab;

    [SerializeField] private float gapWithPlatformY;
    [SerializeField] private int coinChance;

    private GameObject currentInstantiatedCoin;

    private Vector3 platformScale;
    private Vector3 coinSpawnPosition;
    private Vector3 coinNewPosition;

    float coinXDistanceFromCenter;
    float coinZDistanceFromCenter;
    float coinYDistanceFromCenter;

    private void Start()
    {
        SpawnNewCoinByChance();
    }

    private void Update()
    {
        UpdateCoinPosition();
    }

    public void SpawnNewCoinByChance()
    {
        if (Random.Range(1, coinChance) != 1)
        {
            return;
        }

        DefineCoinSpawnPosition();

        currentInstantiatedCoin = Instantiate(
            coinPrefab,
            coinSpawnPosition,
            new Quaternion(0, -0.707106769f, -0.707106769f, 0));
    }

    public void RemoveCurrentCoin()
    {
        if(currentInstantiatedCoin != null)
        {
            Destroy(currentInstantiatedCoin);
        }
    }

    private void DefineCoinSpawnPosition()
    {
        platformScale = platformObj.transform.localScale;

        coinXDistanceFromCenter = Random.Range(-platformScale.x, platformScale.x);
        coinZDistanceFromCenter = Random.Range(-platformScale.z, platformScale.z);
        coinYDistanceFromCenter = platformScale.y;

        coinSpawnPosition = platformObj.transform.TransformPoint(
            new(coinXDistanceFromCenter, coinYDistanceFromCenter, coinZDistanceFromCenter));
        
        coinSpawnPosition.y += gapWithPlatformY;
    }

    private void UpdateCoinPosition()
    {
        if(currentInstantiatedCoin == null)
        {
            return;
        }

        platformScale = platformObj.transform.localScale;

        coinNewPosition = platformObj.transform.TransformPoint(
            new(coinXDistanceFromCenter, coinYDistanceFromCenter, coinZDistanceFromCenter));

        coinNewPosition.y += gapWithPlatformY;
        currentInstantiatedCoin.transform.position = coinNewPosition;
    }

}
