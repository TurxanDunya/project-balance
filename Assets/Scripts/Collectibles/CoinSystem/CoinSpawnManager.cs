using UnityEngine;

public class CoinSpawnManager : MonoBehaviour
{
    [SerializeField] private GameObject platformObj;
    [SerializeField] private GameObject coinPrefab;

    [SerializeField] private float gapWithPlatformY;
    [SerializeField] private float gapFromPlatformEdge = 0.1f;
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

        SpawnCoin();
    }

    public void SpawnCoin()
    {
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
        ResetCoinPositions();

        platformScale = platformObj.transform.localScale;

        coinXDistanceFromCenter = Random.Range(
            -platformScale.x / 2 + gapFromPlatformEdge, platformScale.x / 2 - gapFromPlatformEdge);
        coinZDistanceFromCenter = Random.Range(
            -platformScale.z / 2 + gapFromPlatformEdge, platformScale.z / 2 - gapFromPlatformEdge);

        Vector3 platformUpDirection = platformObj.transform.up;
        Vector3 platformSurfacePosition =
            platformObj.transform.position + platformUpDirection * (platformScale.y / 2);

        coinSpawnPosition = platformObj.transform.TransformPoint(
            new(coinXDistanceFromCenter, 0, coinZDistanceFromCenter));

        coinSpawnPosition = new Vector3(
            coinSpawnPosition.x, platformSurfacePosition.y + gapWithPlatformY, coinSpawnPosition.z);
    }

    private void UpdateCoinPosition()
    {
        if(currentInstantiatedCoin == null)
        {
            return;
        }

        coinNewPosition = platformObj.transform.TransformPoint(
            new(coinXDistanceFromCenter, coinYDistanceFromCenter, coinZDistanceFromCenter));

        coinNewPosition.y += gapWithPlatformY;
        currentInstantiatedCoin.transform.position = coinNewPosition;
    }

    private void ResetCoinPositions()
    {
        coinXDistanceFromCenter = 0;
        coinZDistanceFromCenter = 0;
        coinYDistanceFromCenter = 0;
    }

}
