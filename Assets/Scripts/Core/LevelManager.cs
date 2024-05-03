using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public static LevelManager INSTANCE;
    public LevelManagment levelManagment;

    private void Awake()
    {
        if (INSTANCE == null)
        {
            INSTANCE = this;
        }
        else if (INSTANCE != this) {
            Destroy(this);
        }
        DontDestroyOnLoad(this);
    }

    void Start()
    {
        levelManagment = new LevelManagment();
    }
}
