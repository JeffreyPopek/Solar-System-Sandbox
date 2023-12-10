using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;
using TMPro;

public class InputManager : MonoBehaviour
{
    public static InputManager instance;
    [SerializeField] private GameObject[] objects;
    [SerializeField] private int index = 0;

    private int planetIndex;
    
    private Vector2 mousePos;
    private Vector2 worldPos;

    // Planets
    public List<CelestialBody> planets;
    private int selectedPlanet = -1;

    private bool planetSelected = false;

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
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

                if (distance <= planet.GetComponent<Sphere>().Radius)
                {
                    selectedPlanet = planetIndex;
                    planetFound = true;

                    // If a planet is found, start editing that planet
                    //indexDisplay.text = "Editing: " + planetIndex.ToString();
                    UIManager.instance.ShowUI();

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
                //indexDisplay.text = " ";
                planetSelected = false;
            }
        }
        
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (Universe.instance.gamePaused)
            {
                Universe.instance.UnpauseGame();
            }
            else
            {
                Universe.instance.PauseGame();
            }
        }

        if (Input.GetKeyDown(KeyCode.E))
        {
            index++;
            if (index >= objects.Length)
                index = 0;
        }
    }

    private void CreateSelectedObject(int index)
    {
        GameObject temp = Instantiate(objects[index], GetWorldPos(), quaternion.identity);
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
    
    public string GetCelestialBodyindex()
    {
        return planetIndex.ToString();
    }

}
