using UnityEngine;

public class CoinManager : MonoBehaviour
{
    public delegate void CoinAddEvent();
    public event CoinAddEvent OnCoinAddEvent;

    public delegate void CoinSubtractEvent();
    public event CoinSubtractEvent OnCoinSubtractEvent;

    private long coinCount = 100;

    public long CoinCount
    {
        get { return coinCount; }
    }

    public void AddCoin(long addCount)
    {
        coinCount += addCount;

        OnCoinAddEvent?.Invoke();
    }

    public void SubtractCoin(long subtractCount)
    {
        coinCount -= subtractCount;

        OnCoinSubtractEvent?.Invoke();
    }

}
