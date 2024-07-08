using UnityEngine;

public class CoinManager : MonoBehaviour
{
    public delegate void CoinAddEvent();
    public event CoinAddEvent OnCoinAddEvent;

    public delegate void CoinSubtractEvent();
    public event CoinSubtractEvent OnCoinSubtractEvent;

    private CoinSaveSystem coinSaveSystem;

    private long coinCount = 0;

    private void Start()
    {
        coinSaveSystem = GetComponent<CoinSaveSystem>();
        coinCount = coinSaveSystem.GetCurrentCoinCount();
    }

    public long CoinCount
    {
        get { return coinCount; }
    }

    public void AddCoin(long addCount)
    {
        coinCount += addCount;
        coinSaveSystem.AddCoinAndSave(addCount);

        OnCoinAddEvent?.Invoke();
    }

    public void SubtractCoin(long subtractCount)
    {
        coinCount -= subtractCount;
        coinSaveSystem.SubtractCoinAndSave(subtractCount);

        OnCoinSubtractEvent?.Invoke();
    }

}
