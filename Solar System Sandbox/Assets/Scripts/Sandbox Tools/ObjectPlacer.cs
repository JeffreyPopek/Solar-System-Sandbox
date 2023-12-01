using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPlacer : MonoBehaviour
{
    [SerializeField] private GameObject[] objects;
    // Start is called before the first frame update
    

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Debug.Log("Placing Object");
        }
    }
}
