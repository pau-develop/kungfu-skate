﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    private GameObject[] menuOptions;
    // Start is called before the first frame update
    void Start()
    {
        menuOptions = new GameObject[this.transform.childCount];
        Debug.Log(menuOptions.Length);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
