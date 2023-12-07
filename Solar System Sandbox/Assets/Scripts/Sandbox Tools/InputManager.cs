using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    public static InputManager instance;
    [SerializeField] private GameObject[] objects;
    [SerializeField] private int index = 0;

    private Vector2 mousePos;
    private Vector2 worldPos;
    
    // Planets
    private CelestialBody[] planets;
    private int listIndex = 0;

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            CreateSelectedObject(index);
            
            Debug.Log("Placing Object at " + worldPos);
        }
        
        if (Input.GetMouseButtonDown(1))
        {
            Universe.instance.PauseGame();
            
            
            Debug.Log("Pausing/Unpausing Game!");
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
        GetMousePos();
        GameObject temp = Instantiate(objects[index], worldPos, quaternion.identity);
        //planets[listIndex] = temp.GetComponent<CelestialBody>();
        //listIndex++;
    }


    private void GetMousePos()
    {
        mousePos = Input.mousePosition;
        worldPos = Camera.main.ScreenToWorldPoint(mousePos);
    }

    public Vector2 GetWorldPos()
    {
        return worldPos;
    }
    
}
