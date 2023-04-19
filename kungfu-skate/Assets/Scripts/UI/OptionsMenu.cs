using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PauseMenu : MonoBehaviour
{
    private GameObject[] menuOptions;
    private int currentMenuIndex = 0;
    private int currentResolutionIndex = 0;
    private Vector2[] resolutions;
    private bool fullScreen = false;
    private float blinkTimer = 0;
    private bool flashingColor = false;
    // Start is called before the first frame update
    void Start()
    {
        resolutions = new Vector2[]{
            new Vector2(320,180),
            new Vector2(640,360),
            new Vector2(1240,720),
        };
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
            if(currentMenuIndex == 2) {
                fullScreen = !fullScreen;
                if(fullScreen) updateText("full screen");
                else updateText("windowed");
            }
            if(currentMenuIndex == 1){
                if(currentResolutionIndex == 0) currentResolutionIndex = resolutions.Length -1;
                else currentResolutionIndex--;
                string resolutionString = resolutions[currentResolutionIndex].x.ToString()+"x"+resolutions[currentResolutionIndex].y.ToString(); 
                updateText(resolutionString);
            }
        } 
        if(Input.GetKeyUp(KeyCode.D)){
            if(currentMenuIndex == 2) {
                fullScreen = !fullScreen;
                if(fullScreen) updateText("full screen");
                else updateText("windowed");
            }
            if(currentMenuIndex == 1){
                if(currentResolutionIndex == resolutions.Length -1) currentResolutionIndex = 0;
                else currentResolutionIndex++;
                string resolutionString = resolutions[currentResolutionIndex].x.ToString()+"x"+resolutions[currentResolutionIndex].y.ToString(); 
                updateText(resolutionString);
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
