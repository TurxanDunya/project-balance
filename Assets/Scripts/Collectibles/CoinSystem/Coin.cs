using UnityEngine;

public class Coin : MonoBehaviour
{
    [SerializeField] private GameObject selfPrefab;
    [SerializeField] private int addCountOnCubeCollision;

    public delegate void CoinAddEvent();
    public event CoinAddEvent OnCoinAddEvent;

    private CoinManager coinManager;

    private void Start()
    {
        coinManager = FindObjectOfType<CoinManager>();
    }

    private void OnTriggerEnter(Collider collider)
    {
        if (collider.CompareTag(Constants.PLAYABLE_CUBE))
        {
            coinManager.AddCoin(addCountOnCubeCollision);
            OnCoinAddEvent?.Invoke();
            Destroy(selfPrefab);
        }
    }

}
