using UnityEngine;

public class UIElementEnabler : MonoBehaviour
{
    [Header("Power Ups")]
    [SerializeField] public bool isCubeChangerEnabled;
    [SerializeField] public bool isTimerEnabled;
    [SerializeField] public bool isMagnetEnabled;
    [SerializeField] public bool isBombEnabled;

    [Header("Cube Counters")]
    [SerializeField] public bool isWoodCubeEnabled;
    [SerializeField] public bool isMetalCubeEnabled;
    [SerializeField] public bool isIceCubeEnabled;

    [Header("Level Spesific Modes")]
    [SerializeField] public bool isGhostModeEnabled;

}
