using UnityEngine;

public class UIElementEnabler : MonoBehaviour
{
    [Header("Power Ups")]
    public bool isCubeChangerEnabled;
    public bool isTimerEnabled;
    public bool isMagnetEnabled;
    public bool isBombEnabled;

    [Header("Cube Counters")]
    public bool isWoodCubeEnabled;
    public bool isMetalCubeEnabled;
    public bool isIceCubeEnabled;

    [Header("Level Spesific Modes")]
    public bool isGhostModeEnabled;
    public bool isLightBlinkModeEnabled;

}
