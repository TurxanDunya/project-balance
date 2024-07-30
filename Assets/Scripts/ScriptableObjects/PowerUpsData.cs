using UnityEngine;

[CreateAssetMenu(fileName = "PowerUpsData", menuName = "ScriptableObjects/PowerUpsData")]
public class PowerUpsData : ScriptableObject
{
    public PowerUpType powerUpType;
    public int maxCount;
    public int costOfUse;
    public int canUnlockableAfterLevel;
    public int unlockPrice;

    public enum PowerUpType
    {
        RANDOM_NEXT,
        PLUS_5_SECOND,
        MAGNET_CUBE,
        BOMB_CUBE
    }
}
