using UnityEngine;

public class CoinManager : MonoBehaviour
{
    private long coinCount = 100;

    public long CoinCount
    {
        get { return coinCount; }
    }

    public void AddCoin(long addCount)
    {
        coinCount += addCount;
    }

    public void SubtractCoin(long subtractCount)
    {
        coinCount -= subtractCount;
    }

}
