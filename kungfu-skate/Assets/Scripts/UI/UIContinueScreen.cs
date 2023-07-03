using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIContinueScreen : MonoBehaviour
{
    // Start is called before the first frame update
    

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyUp(KeyCode.Space)) GlobalData.inContinueScreen = false;
    }
}
