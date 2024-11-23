using UnityEngine;
using LunarConsolePlugin;

public class ApplicationSettings : MonoBehaviour
{
    private void Start()
    {
        Application.targetFrameRate = 60;

        CheckLunarConsoleState();
    }

    private void CheckLunarConsoleState()
    {
        #if UNITY_EDITOR || DEVELOPMENT_BUILD
             LunarConsole.SetConsoleEnabled(true);
        #else
             LunarConsole.SetConsoleEnabled(false);
        #endif
    }

}
