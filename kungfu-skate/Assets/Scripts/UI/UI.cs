using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class UI : MonoBehaviour
{
    public static bool gamePaused = false;
    private GameObject debugger;
    private bool displayingDebugger = true;
    private bool displayingOptions = false;
    
    private GameObject options;
    // Start is called before the first frame update
    void Start()
    {
        options = transform.Find("options").gameObject;
        options.SetActive(false);
        debugger = transform.Find("debugger").gameObject;
        
    }

    // Update is called once per frame
    void Update()
    {
        controlUI();   
    }

    private void controlUI(){
        displayDebugger();
        displayOptionsMenu();
    }

    private void displayOptionsMenu(){
        if(Input.GetKeyUp(KeyCode.Escape)) displayingOptions = !displayingOptions;
        if(displayingOptions) {
            gamePaused = true;
            options.SetActive(true);
            Time.timeScale = 0;
        }
        else {
            gamePaused = false;
            options.SetActive(false);
            Time.timeScale = 1;
        }
    }
    

    private void displayDebugger(){
        if(Input.GetKeyUp(KeyCode.Q)) displayingDebugger = !displayingDebugger;
        if(displayingDebugger) debugger.SetActive(true);
        else debugger.SetActive(false);
    }
}
