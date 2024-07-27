using UnityEngine;

public class CoinSaveSystem : MonoBehaviour
{
    private readonly int INITIAL_COIN_COUNT = 0;

    private readonly string coinsSaveFile = "coins.dat";
    private CoinSaveData coinSaveData;

    private void OnEnable()
    {
        string coinSaveDataAsString = FileUtil.LoadFromFile(coinsSaveFile);

        if (coinSaveDataAsString != null)
        {
            coinSaveData = JsonUtility.FromJson<CoinSaveData>(coinSaveDataAsString);
        }
        else
        {
            coinSaveData = new();
            coinSaveData.coinCount = INITIAL_COIN_COUNT;

            SaveData();
        }
    }

    public long GetCurrentCoinCount()
    {
        if (coinSaveData == null)
        {
            return INITIAL_COIN_COUNT;
        }

        return coinSaveData.coinCount;
    }

    public void AddCoinAndSave(long count)
    {
        coinSaveData.coinCount += count;
        SaveData();
    }

    public void SubtractCoinAndSave(long count)
    {
        coinSaveData.coinCount -= count;
        SaveData();
    }

    private void SaveData()
    {
        string coinDataJson = JsonUtility.ToJson(coinSaveData, true);
        FileUtil.SaveToFile(coinDataJson, coinsSaveFile);
    }

}
