using UnityEngine;

public class CoinManager : MonoBehaviour
{
    public delegate void CoinCountChangeEvent(long coinCount);
    public event CoinCountChangeEvent OnCoinCountChangeEvent;

    private CoinSaveSystem coinSaveSystem;

    private long currentCoinCount = 0;

    private void Awake()
    {
        coinSaveSystem = GetComponent<CoinSaveSystem>();
        currentCoinCount = coinSaveSystem.GetCurrentCoinCount();
    }

    public long CoinCount
    {
        get { return currentCoinCount; }
    }

    public void AddCoin(long addCount)
    {
        currentCoinCount += addCount;
        coinSaveSystem.AddCoinAndSave(addCount);

        OnCoinCountChangeEvent?.Invoke(currentCoinCount);
    }

    public void SubtractCoin(long subtractCount)
    {
        currentCoinCount -= subtractCount;
        coinSaveSystem.SubtractCoinAndSave(subtractCount);

        OnCoinCountChangeEvent?.Invoke(currentCoinCount);
    }

    public bool IsCoinEnough(long countToCompare)
    {
        return currentCoinCount >= countToCompare;
    }

}
