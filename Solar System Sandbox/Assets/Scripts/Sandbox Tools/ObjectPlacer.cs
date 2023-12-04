using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class ObjectPlacer : MonoBehaviour
{
    [SerializeField] private GameObject[] objects;
    [SerializeField] private int index = 0;

    private Vector2 mousePos;
    private Vector2 worldPos;

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            mousePos = Input.mousePosition;
            
            CreateSelectedObject(index);
            
            Debug.Log("Placing Object at " + worldPos);
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
        worldPos = Camera.main.ScreenToWorldPoint(mousePos);
        Instantiate(objects[index], worldPos, quaternion.identity);
    }
}
