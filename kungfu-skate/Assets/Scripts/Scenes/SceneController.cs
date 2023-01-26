using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class SceneController : MonoBehaviour
{
    // Update is called once per frame
    void Update()
    {
       if(Input.GetKeyUp(KeyCode.F1)) SceneManager.LoadScene(0);

    }
}
