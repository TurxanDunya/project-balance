using System.Collections.Generic;
using UnityEngine;

public class PowerUps : MonoBehaviour
{
    [SerializeField] List<PowerUpsData> powerUpsDatas;

    private Dictionary<PowerUpsData.PowerUpType, int> leftPowerUpByCount;
    private List<PowerUpsData.PowerUpType> unlockedPowerUps;

    public PowerUps()
    {
        unlockedPowerUps = new List<PowerUpsData.PowerUpType>();
        leftPowerUpByCount = new Dictionary<PowerUpsData.PowerUpType, int>();

        // these lines will be removed after we read this data from file
        unlockedPowerUps.Add(PowerUpsData.PowerUpType.RANDOM_NEXT);
        leftPowerUpByCount.Add(PowerUpsData.PowerUpType.RANDOM_NEXT, 2);
        // these lines will be removed after we read this data from file
    }

    public void UnlockPowerUp(PowerUpsData.PowerUpType powerUp)
    {
        unlockedPowerUps.Add(powerUp);
    }

    public bool IsPowerUpUnlocked(PowerUpsData.PowerUpType powerUp)
    {
        return unlockedPowerUps.Contains(powerUp);
    }

    public int GetLeftCount(PowerUpsData.PowerUpType powerUpType)
    {
        return leftPowerUpByCount[powerUpType];
    }

    public void IncreaseLeftCount(PowerUpsData.PowerUpType powerUpType, int count)
    {
        int increasedCount = leftPowerUpByCount[powerUpType] + count;
        leftPowerUpByCount.Add(powerUpType, increasedCount);
    }

    public void DecreaseLeftCount(PowerUpsData.PowerUpType powerUpType, int count)
    {
        int increasedCount = leftPowerUpByCount[powerUpType] - count;
        leftPowerUpByCount.Add(powerUpType, increasedCount);
    }

}
