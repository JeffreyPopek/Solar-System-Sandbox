using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIManager : MonoBehaviour
{

    public static UIManager instance;
    
    [SerializeField] private GameObject UIHolder;
    [SerializeField] private Vector2 UIOut;
    [SerializeField] private Vector2 UIIn;

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

        //CelestialBodyName;

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
        if (uiShowing)
        {
            UIHolder.SetActive(false);
            uiShowing = false;
            //UIHolder.transform.position = Vector2.Lerp(UIIn, UIOut, 1.0f);
        }
        else
        {
            UIHolder.SetActive(true);
            uiShowing = true;
        }
    }
}
