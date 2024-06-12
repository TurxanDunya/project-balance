using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public static LevelManager INSTANCE;
    public LevelManagment levelManagment;

    private void Awake()
    {
        levelManagment = new LevelManagment();

        if (INSTANCE == null)
        {
            INSTANCE = this;
            DontDestroyOnLoad(this);
        }
        else if (INSTANCE != this) {
            Destroy(this);
        }
      
    }

}
