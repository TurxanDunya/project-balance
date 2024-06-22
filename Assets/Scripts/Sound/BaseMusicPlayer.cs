using UnityEngine;

public class BaseMusicPlayer : MonoBehaviour
{
    private static BaseMusicPlayer INSTANCE;

    private void Awake()
    {
        if (INSTANCE == null)
        {
            INSTANCE = this;
            DontDestroyOnLoad(this);
        }
        else
        {
            Destroy(this);
        }
    }
}
