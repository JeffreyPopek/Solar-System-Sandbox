using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIManager : MonoBehaviour
{

    public static UIManager instance;
    
    [SerializeField] private GameObject UIHolder;

    private bool uiShowing = false;
    
    
    //menu
    [SerializeField] private TextMeshProUGUI CelestialBodyName;

    


    private void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
            return;
        }

        instance = this;
        DontDestroyOnLoad(gameObject);
        
        UIHolder.SetActive(false);
    }
    

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            ShowUI();
        }
    }

    public void ShowUI()
    {
        UpdateInfo();
        
        if (uiShowing)
        {
            UIHolder.SetActive(false);
            uiShowing = false;
        }
        else
        {
            UIHolder.SetActive(true);
            uiShowing = true;
        }
    }

    private void UpdateInfo()
    {
        //CelestialBodyName.text = InputManager.instance.GetCelestialBodyindex();
    }
}
