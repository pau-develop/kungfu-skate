using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PauseMenu : MonoBehaviour
{
    [SerializeField]private AudioClip testSound;
    private float musicVolume = 100;
    private float fxVolume = 100;
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
    }

    // Update is called once per frame
    void Update()
    { 
        textBlinkEffect();
        checkForInput();    
    }

    private void updateVolume(GameObject menu, float volume, string source){
        menu.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = volume.ToString();
        float floatValue = (volume/10) * 0.1f;
        GameObject.Find(source).GetComponent<AudioSource>().volume = floatValue;
        if(source == "audio-fx") GameObject.Find("audio").GetComponent<AudioController>().playSound(testSound);
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
            if(currentMenuIndex == 2){
                if(musicVolume >= 10) musicVolume -= 10;
                updateVolume(menuOptions[currentMenuIndex], musicVolume, "audio-music");
            }
            if(currentMenuIndex == 3){
                if(fxVolume >= 10) fxVolume -= 10;
                updateVolume(menuOptions[currentMenuIndex], fxVolume, "audio-fx");
            }
        } 
        if(Input.GetKeyUp(KeyCode.D)){
            if(currentMenuIndex == 1) {
                fullScreen = !fullScreen;
                if(fullScreen) updateText("full screen");
                else updateText("windowed");
                Screen.SetResolution(1280, 720, fullScreen);
            }
            if(currentMenuIndex == 2){
                if(musicVolume <= 90) musicVolume += 10;
                updateVolume(menuOptions[currentMenuIndex], musicVolume, "audio-music");
            }
            if(currentMenuIndex == 3){
                if(fxVolume <= 90) fxVolume += 10;
                updateVolume(menuOptions[currentMenuIndex], fxVolume, "audio-fx");
            }
        }
    }

    private void updateText(string text){
        menuOptions[currentMenuIndex].transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = text;
    }

    private void textBlinkEffect(){
        blinkTimer += Time.unscaledDeltaTime;
        if(blinkTimer >= 0.1f){
            flashingColor = !flashingColor;
            blinkTimer = 0;
        }
        if(!flashingColor) menuOptions[currentMenuIndex].GetComponent<TextMeshProUGUI>().color = new Color32(0,255,150,255);
        else menuOptions[currentMenuIndex].GetComponent<TextMeshProUGUI>().color = new Color32(255,255,255,255);
    }

    private void stopTextBlinkEffect(int index){
        menuOptions[index].GetComponent<TextMeshProUGUI>().color = new Color32(0,255,150,255);
    }
}
