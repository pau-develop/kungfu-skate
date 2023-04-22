using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PauseMenu : MonoBehaviour
{
    [SerializeField]private AudioClip testSound;
    private GameObject[] menuOptions;
    private int currentMenuIndex = 0;
    private int currentResolutionIndex = 1;
    private Vector2[] resolutions;
    private float blinkTimer = 0;
    private bool flashingColor = false;
    private UIText uiText;
    // Start is called before the first frame update
    void Start()
    {
        uiText = transform.root.GetComponent<UIText>(); 
        menuOptions = new GameObject[this.transform.childCount];
        for(int i = 0; i < menuOptions.Length; i++) menuOptions[i] = transform.GetChild(i).gameObject;
        updateVolume(menuOptions[2], GlobalData.musicVolume, "audio-music");
        updateVolume(menuOptions[3], GlobalData.fxVolume, "audio-fx");
        updateDisplay(GlobalData.fullScreen);
    }

    void OnEnable(){
        currentMenuIndex = 0;
    }

    void OnDisable(){
        stopTextBlinkEffect();
    }

    // Update is called once per frame
    void Update()
    { 
        textBlinkEffect();
        checkForInput();    
    }

    private void textBlinkEffect(){
        TextMeshProUGUI text = menuOptions[currentMenuIndex].GetComponent<TextMeshProUGUI>();
        uiText.textBlinkEffect(text);
        if(menuOptions[currentMenuIndex].transform.childCount > 0){
            text = menuOptions[currentMenuIndex].transform.GetChild(0).GetComponent<TextMeshProUGUI>();
            uiText.textBlinkEffect(text);
        }
    }

    private void stopTextBlinkEffect(){
        TextMeshProUGUI text = menuOptions[currentMenuIndex].GetComponent<TextMeshProUGUI>();
        uiText.stopTextBlinkEffect(text);
        if(menuOptions[currentMenuIndex].transform.childCount > 0){
            text = menuOptions[currentMenuIndex].transform.GetChild(0).GetComponent<TextMeshProUGUI>();
            uiText.stopTextBlinkEffect(text);
        }
    }

    private void updateVolume(GameObject menu, float volume, string source){
        menu.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = volume.ToString();
        float floatValue = (volume/10) * 0.1f;
        GameObject.Find(source).GetComponent<AudioSource>().volume = floatValue;
    }

    private void checkForInput(){
        if(Input.GetKeyUp(KeyCode.W)){
            stopTextBlinkEffect();
            if(currentMenuIndex == 0) currentMenuIndex = menuOptions.Length-1;
            else currentMenuIndex--;
        }
        if(Input.GetKeyUp(KeyCode.S)){
            stopTextBlinkEffect();
            if(currentMenuIndex == menuOptions.Length-1) currentMenuIndex = 0;
            else currentMenuIndex++;
        }
        if(Input.GetKeyUp(KeyCode.M)){
            if(currentMenuIndex == 0) GlobalData.gamePaused = false;
        }
        if(Input.GetKeyUp(KeyCode.A)){
            if(currentMenuIndex == 1){
                GlobalData.fullScreen = !GlobalData.fullScreen;
                if(GlobalData.fullScreen) updateDisplay(GlobalData.fullScreen);
                else updateDisplay(GlobalData.fullScreen);
            }
            if(currentMenuIndex == 2){
                if(GlobalData.musicVolume >= 10) GlobalData.musicVolume -= 10;
                updateVolume(menuOptions[currentMenuIndex], GlobalData.musicVolume, "audio-music");
            }
            if(currentMenuIndex == 3){
                if(GlobalData.fxVolume >= 10) GlobalData.fxVolume -= 10;
                updateVolume(menuOptions[currentMenuIndex], GlobalData.fxVolume, "audio-fx");
                GameObject.Find("audio").GetComponent<AudioController>().playSound(testSound);
            }
        } 
        if(Input.GetKeyUp(KeyCode.D)){
            if(currentMenuIndex == 1) {
                GlobalData.fullScreen = !GlobalData.fullScreen;
                if(GlobalData.fullScreen) updateDisplay(GlobalData.fullScreen);
                else updateDisplay(GlobalData.fullScreen);
                Screen.SetResolution(1280, 720, GlobalData.fullScreen);
            }
            if(currentMenuIndex == 2){
                if(GlobalData.musicVolume <= 90) GlobalData.musicVolume += 10;
                updateVolume(menuOptions[currentMenuIndex], GlobalData.musicVolume, "audio-music");
            }
            if(currentMenuIndex == 3){
                if(GlobalData.fxVolume <= 90) GlobalData.fxVolume += 10;
                updateVolume(menuOptions[currentMenuIndex], GlobalData.fxVolume, "audio-fx");
                GameObject.Find("audio").GetComponent<AudioController>().playSound(testSound);
            }
        }
    }

    private void updateDisplay(bool fullScreen){
        string text;
        if(fullScreen) text = "full";
        else text = "window";
        menuOptions[1].transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = text;
        Screen.SetResolution(1280, 720, GlobalData.fullScreen);
    }

    

   
}
