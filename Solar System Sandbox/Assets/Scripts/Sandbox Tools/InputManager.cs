using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;
using TMPro;
using UnityEngine.EventSystems;

public class InputManager : MonoBehaviour
{
    public static InputManager instance;
    
    [SerializeField] private GameObject[] objects;

    private GameObject selectedPlanetGameObject;
    [SerializeField] private int index = 0;

    private int planetIndex;
    private int numPlanets = 0;
    
    private Vector2 mousePos;
    private Vector2 worldPos;

    // Planets
    public List<CelestialBody> planets;
    private int selectedPlanet = -1;

    private bool planetSelected = false;


    private Vector3 TempVector = new Vector3(2, 2, 2);
    
    
    //Pause
    [SerializeField] private GameObject pauseSymbol;
    [SerializeField] private GameObject unpauseSymbol;

    private bool CRRunning = false;

    
    
    private void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
            return;
        }

        instance = this;
        DontDestroyOnLoad(gameObject);
        
        pauseSymbol.SetActive(false);
        unpauseSymbol.SetActive(false);
    }
    

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            // Fixes placing an object when clicking on the UI
            if(EventSystem.current.IsPointerOverGameObject())
                return;
            
            
            CreateSelectedObject(index);
            
            Debug.Log("Placing Object at " + worldPos);
        }
        
        if (Input.GetMouseButtonDown(1))
        {
            planetIndex = 0;
            bool planetFound = false;

            foreach (CelestialBody planet in planets)
            {
                float distance = (planet.transform.position - GetWorldPos()).magnitude;

                Debug.Log(distance);

                if (distance <= 4 * planet.GetComponent<Sphere>().Radius)
                {
                    selectedPlanet = planetIndex;
                    planetFound = true;

                    // If a planet is found, start editing that planet
                    // If UI isn't already showing then show it, if it is then just update the info on the menu
                    if (UIManager.instance.IsUIShowing())
                    {
                        UIManager.instance.UpdateInfo();
                    }
                    else
                    {
                        UIManager.instance.ShowUI();
                    }

                    planetSelected = true;
                }

                planetIndex++;
            }
            
            // If a planet is found, pause the simulation
            if (planetFound)
            {
                Universe.instance.PauseGame();
                Debug.Log("Pausing Game!");
            }
            else
            {
                planetSelected = false;
            }
        }
        
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Debug.Log("Pausing");
            if (Universe.instance.gamePaused)
            {
                Universe.instance.UnpauseGame();
            }
            else
            {
                Universe.instance.PauseGame();
            }

            PauseLogic();
        }
        //
        // if (Input.GetKeyDown(KeyCode.E))
        // {
        //     index++;
        //     if (index >= objects.Length)
        //         index = 0;
        // }
    }

    private void CreateSelectedObject(int index)
    {
        numPlanets++;
        GameObject temp = Instantiate(objects[index], GetWorldPos(), quaternion.identity);
        temp.name = "Planet " + numPlanets;
        planets.Add(temp.GetComponent<CelestialBody>());
        temp.GetComponent<CelestialBody>().planetIndex = planets.Count - 1;
    }

    public Vector3 GetWorldPos()
    {
        mousePos = Input.mousePosition;
        worldPos = Camera.main.ScreenToWorldPoint(mousePos);
        return worldPos;
    }

    public bool GetPlanetSelectedStatus()
    {
        return planetSelected;
    }

    public CelestialBody GetPlanet()
    {
        if (selectedPlanet < 0)
        {
            CelestialBody temp = new CelestialBody();
            return temp;
        }
        return planets[selectedPlanet];
    }
    
    public string GetPlanetName()
    {
        if (selectedPlanet < 0)
        {
            string temp = "";
            return temp;
        }
        return planets[selectedPlanet].name;

    }

    public float GetPlanetSize()
    {
        if (selectedPlanet < 0)
        {
            return 0;
        }
        
        return planets[selectedPlanet].GetComponent<Sphere>().Radius;
    }

    public void RemoveCelestialBody()
    {
        if (UIManager.instance.IsUIShowing())
        {
            UIManager.instance.ShowUI();
        }
        
        if (!planets[selectedPlanet].gameObject.CompareTag("Sun"))
        {
            Debug.Log("Selected Planet" + selectedPlanet);
            planets[selectedPlanet].GetComponent<CelestialBody>().DestroyObject();
            planets.RemoveAt(selectedPlanet);

            selectedPlanet = -1;
        }
    }

    public void IncreasePlanetSize()
    {
        Debug.Log("Increasing Planet Size");
        if (planets[selectedPlanet].GetComponent<Sphere>().Radius == 5.0f)
            return;
        
        planets[selectedPlanet].transform.localScale += TempVector;
        planets[selectedPlanet].GetComponent<Sphere>().Radius += 1;
        planets[selectedPlanet].inverseMass /= 2.0f;
        
        UIManager.instance.UpdateInfo();
    }

    public void DecreasePlanetSize()
    {
        Debug.Log("Decreasing Planet Size");

        if (planets[selectedPlanet].GetComponent<Sphere>().Radius == 1.0f)
            return;
        
        planets[selectedPlanet].transform.localScale -= TempVector;
        planets[selectedPlanet].GetComponent<Sphere>().Radius -= 1;
        planets[selectedPlanet].inverseMass *= 2.0f;

        UIManager.instance.UpdateInfo();
    }

    private void PauseLogic()
    {
        if (Universe.instance.gamePaused)
        {
            pauseSymbol.SetActive(true);
            unpauseSymbol.SetActive(false);
        }
        else if(!Universe.instance.gamePaused)
        {
            pauseSymbol.SetActive(false);
            unpauseSymbol.SetActive(true);
        }

        if (CRRunning)
        {
            StopCoroutine(PauseTimer());
            CRRunning = false;
        }
        
        StartCoroutine(PauseTimer());
    }


    private IEnumerator PauseTimer()
    {
        CRRunning = true;
        
        yield return new WaitForSeconds(2);
        
        pauseSymbol.SetActive(false);
        unpauseSymbol.SetActive(false);
    }
}
