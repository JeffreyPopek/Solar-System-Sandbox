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
    [SerializeField] private TextMeshProUGUI velocity;
    [SerializeField] private TextMeshProUGUI position;


    private Vector2 currPosition;
    


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
        velocity.text = "";
        position.text = "";
    }
    

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            // Error when no planets is selected but it doesn't break so its fine ig
            ShowUI();
        }

        if (uiShowing)
        {
            UpdateInfo();
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

    public void CloseUI()
    {
        UIHolder.SetActive(false);
        uiShowing = false;
    }

    public bool IsUIShowing()
    {
        return uiShowing;
    }
    
    public void UpdateInfo()
    {
        CelestialBodyName.text = InputManager.instance.GetPlanetName();
        size.text = InputManager.instance.GetPlanetSize().ToString();
        if (InputManager.instance.GetPlanet().GetComponent<Sphere>().Radius == 1.0f || InputManager.instance.GetPlanet().GetComponent<Sphere>().Radius == 5.0f)
        {
            size.color = Color.red;
        }
        else
        {
            size.color = Color.white;
        }

        velocity.text = InputManager.instance.GetPlanet().GetComponent<CelestialBody>().velocity.ToString();
        currPosition = InputManager.instance.GetPlanet().transform.position;
        currPosition.x = (int)currPosition.x;
        currPosition.y = (int)currPosition.y;
        position.text = currPosition.ToString();


    }
    
    
}
