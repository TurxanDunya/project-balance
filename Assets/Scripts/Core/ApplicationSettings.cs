using UnityEngine;
using LunarConsolePlugin;

public class ApplicationSettings : MonoBehaviour
{
    [SerializeField] private GameObject lunarConsolePrefab; 

    private void Start()
    {
        Application.targetFrameRate = 60;

        CheckLunarConsoleState();
    }

    private void CheckLunarConsoleState()
    {
#if UNITY_EDITOR || DEVELOPMENT_BUILD
        LunarConsole.SetConsoleEnabled(true);
        Destroy(lunarConsolePrefab);
#else
        LunarConsole.SetConsoleEnabled(false);
        Destroy(lunarConsolePrefab);
#endif
    }

}
