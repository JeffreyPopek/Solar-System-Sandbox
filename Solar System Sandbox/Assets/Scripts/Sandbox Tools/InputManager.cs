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

    [SerializeField] private TextMeshProUGUI indexDisplay;

    private Vector2 mousePos;
    private Vector2 worldPos;

    // Planets
    public List<CelestialBody> planets;
    private int selectedPlanet;

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            CreateSelectedObject(index);
            
            Debug.Log("Placing Object at " + worldPos);
        }
        
        if (Input.GetMouseButtonDown(1))
        {
            int planetIndex = 0;
            bool planetFound = false;

            // If simulation is already paused, search for a planet at mouse position
            if (Universe.instance.gamePaused)
            {
                foreach (CelestialBody planet in planets)
                {
                    float distance = (planet.transform.position - GetWorldPos()).magnitude;

                    Debug.Log(distance);

                    if (distance <= planet.GetComponent<Sphere>().Radius)
                    {
                        selectedPlanet = planetIndex;
                        planetFound = true;

                        // If a planet is found, start editing that planet
                        indexDisplay.text = "Editing: " + planetIndex.ToString();
                    }

                    planetIndex++;
                }

                // If a planet is not found, unpause the simulation
                if (!planetFound)
                {
                    indexDisplay.text = " ";

                    Universe.instance.PauseGame();
                    Debug.Log("Unpausing Game!");
                }
            }

            // If simulation is running, pause it
            else
            {
                Universe.instance.PauseGame();
                Debug.Log("Pausing Game!");
            }
        }
        
        if (Input.GetKeyDown(KeyCode.Q))
        {
            //
        }
        
        if (Input.GetKeyDown(KeyCode.E))
        {
            //
        }
    }

    private void CreateSelectedObject(int index)
    {
        GameObject temp = Instantiate(objects[index], GetWorldPos(), quaternion.identity);
        planets.Add(temp.GetComponent<CelestialBody>());
    }

    public Vector3 GetWorldPos()
    {
        mousePos = Input.mousePosition;
        worldPos = Camera.main.ScreenToWorldPoint(mousePos);
        return worldPos;
    }
    
}
