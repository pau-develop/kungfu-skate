using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class UI : MonoBehaviour
{
    private GameObject debugger;
    private bool displayingDebugger = true;    
    private GameObject options;
    private GameObject continueScreen;
    // Start is called before the first frame update
    void Start()
    {

        options = transform.Find("options").gameObject;
        continueScreen = transform.Find("continue-screen").gameObject;
        options.SetActive(false);
        continueScreen.SetActive(false);
        debugger = transform.Find("debugger").gameObject;        
    }

    // Update is called once per frame
    void Update()
    {
        controlUI();   
    }

    

    private void controlUI(){
        getInput();
        displayDebugger();
        displayPauseMenu();
        displayContinueMenu();
    }

    private void displayContinueMenu(){
        if(GlobalData.inContinueScreen) {
            Time.timeScale = 0;
            continueScreen.SetActive(true);
        }
        else {
            if(!GlobalData.gamePaused){
                Time.timeScale = 1;
                continueScreen.SetActive(false);
            }
        }
    }

    private void displayPauseMenu(){
        if(GlobalData.gamePaused) {
            Time.timeScale = 0;
            options.SetActive(true);
        }
        else {
            Time.timeScale = 1;
            options.SetActive(false);
        }
    }

    private void getInput(){
        if(Input.GetKeyUp(KeyCode.Escape) && !GlobalData.inContinueScreen) 
            GlobalData.gamePaused = !GlobalData.gamePaused;
    }
    

    private void displayDebugger(){
        if(Input.GetKeyUp(KeyCode.Q)) displayingDebugger = !displayingDebugger;
        if(displayingDebugger) debugger.SetActive(true);
        else debugger.SetActive(false);
    }
}
