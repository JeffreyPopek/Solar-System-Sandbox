using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Universe : MonoBehaviour
{
    public static Universe instance { get; private set; }

    [HideInInspector] public bool gamePaused;

    private void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
            return;
        }

        instance = this;
        DontDestroyOnLoad(gameObject);
        gamePaused = false;
    }

    
    public void PauseGame()
    {
        gamePaused = true;
    }
    
    public void UnpauseGame()
    {
        gamePaused = false;
    }
}
