using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PauseMenu : MonoBehaviour
{
    private GameObject[] menuOptions;
    private int currentMenuIndex = 0;
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
            if(currentMenuIndex == 0) currentMenuIndex = menuOptions.Length-1;
            else currentMenuIndex--;
        }
        if(Input.GetKeyUp(KeyCode.S)){
            if(currentMenuIndex == menuOptions.Length-1) currentMenuIndex = 0;
            else currentMenuIndex++;
        }
        if(Input.GetKeyUp(KeyCode.M)){
            if(currentMenuIndex == 0) UI.gamePaused = false;
        } 
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
}
