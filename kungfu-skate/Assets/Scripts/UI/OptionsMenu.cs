using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PauseMenu : MonoBehaviour
{
    private GameObject[] menuOptions;
    private int currentMenuIndex = 0;
    private int currentResolutionIndex = 1;
    private Vector2[] resolutions;
    private bool fullScreen = false;
    private float blinkTimer = 0;
    private bool flashingColor = false;
    // Start is called before the first frame update
    void Start()
    {
        menuOptions = new GameObject[this.transform.childCount];
        for(int i = 0; i < menuOptions.Length; i++) menuOptions[i] = transform.GetChild(i).gameObject;
        Debug.Log(flashingColor);
    }

    // Update is called once per frame
    void Update()
    { 
        textBlinkEffect();
        checkForInput();    
    }

    private void checkForInput(){
        if(Input.GetKeyUp(KeyCode.W)){
            stopTextBlinkEffect(currentMenuIndex);
            if(currentMenuIndex == 0) currentMenuIndex = menuOptions.Length-1;
            else currentMenuIndex--;
        }
        if(Input.GetKeyUp(KeyCode.S)){
            stopTextBlinkEffect(currentMenuIndex);
            if(currentMenuIndex == menuOptions.Length-1) currentMenuIndex = 0;
            else currentMenuIndex++;
        }
        if(Input.GetKeyUp(KeyCode.M)){
            if(currentMenuIndex == 0) UI.gamePaused = false;
        }
        if(Input.GetKeyUp(KeyCode.A)){
            if(currentMenuIndex == 1){
                fullScreen = !fullScreen;
                if(fullScreen) updateText("full screen");
                else updateText("windowed");
                Screen.SetResolution(1280, 720, fullScreen);
            }
        } 
        if(Input.GetKeyUp(KeyCode.D)){
            if(currentMenuIndex == 1) {
                fullScreen = !fullScreen;
                if(fullScreen) updateText("full screen");
                else updateText("windowed");
                Screen.SetResolution(1280, 720, fullScreen);
            }
        }
    }

    private void updateText(string text){
        menuOptions[currentMenuIndex].transform.GetChild(0).GetComponent<TextMeshPro>().text = text;
    }

    private void textBlinkEffect(){
        blinkTimer += Time.unscaledDeltaTime;
        if(blinkTimer >= 0.1f){
            flashingColor = !flashingColor;
            blinkTimer = 0;
        }
        if(!flashingColor) menuOptions[currentMenuIndex].GetComponent<TextMeshPro>().color = new Color32(0,255,150,255);
        else menuOptions[currentMenuIndex].GetComponent<TextMeshPro>().color = new Color32(255,255,255,255);
    }

    private void stopTextBlinkEffect(int index){
        menuOptions[index].GetComponent<TextMeshPro>().color = new Color32(0,255,150,255);
    }
}
