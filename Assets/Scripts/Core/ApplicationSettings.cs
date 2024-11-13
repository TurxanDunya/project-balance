using UnityEngine;
using LunarConsolePlugin;

public class ApplicationSettings : MonoBehaviour
{
    private void Start()
    {
        Application.targetFrameRate = 60;

        LunarConsole.Hide();
        LunarConsole.Clear();
        LunarConsole.SetConsoleEnabled(false);
    }
}
