using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlCamera : MonoBehaviour
{
    void Start()
    {
		if(Screen.fullScreen) Screen.SetResolution(1280, 720, true);
		else Screen.SetResolution(1280, 720, false);  
    }
}
