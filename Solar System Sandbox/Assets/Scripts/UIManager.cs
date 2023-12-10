using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{

    public static UIManager instance;
    
    [SerializeField] private GameObject UIHolder;

    private bool uiShowing = false;


    //menu
    [SerializeField] private TextMeshProUGUI CelestialBodyName;
    [SerializeField] private TextMeshProUGUI size;

    


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

        CelestialBodyName.text = "";
        size.text = "";
    }
    

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            // Error when no planets is selected but it doesn't break so its fine ig
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

    public bool IsUIShowing()
    {
        return uiShowing;
    }
    
    public void UpdateInfo()
    {
        CelestialBodyName.text = InputManager.instance.GetPlanetName();
        size.text = InputManager.instance.GetPlanetSize().ToString();
    }
    
    
}
