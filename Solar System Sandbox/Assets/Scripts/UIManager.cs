using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    [SerializeField] private GameObject UIHolder;
    [SerializeField] private Vector2 UIOut;
    [SerializeField] private Vector2 UIIn;


    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            MoveOut();
        }
    }

    private void MoveOut()
    {
        //UIHolder.transform.position = Vector2.Lerp(UIIn, UIOut, 1.0f);
    }
}
