using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{   
    private RectTransform[] menuDots = new RectTransform[2];
    private int[] dotsVerticalPos;
    [SerializeField]private AudioClip testSound;
    private GameObject[] menuOptions;
    private int currentMenuIndex = 0;
    private UIText uiText;

    // Start is called before the first frame update
    void Start()
    {
        uiText = transform.root.GetComponent<UIText>(); 
        menuOptions = new GameObject[this.transform.childCount];
        for(int i = 0; i < menuOptions.Length; i++) menuOptions[i] = transform.GetChild(i).gameObject;
        if(SceneManager.GetActiveScene().name == "MAIN") menuOptions[0].GetComponent<TextMeshProUGUI>().text = "Back";
        else menuOptions[0].GetComponent<TextMeshProUGUI>().text = "Resume";
        updateVolume(menuOptions[2], GlobalData.musicVolume, "audio-music");
        updateVolume(menuOptions[3], GlobalData.fxVolume, "audio-fx");
        updateDisplay(GlobalData.fullScreen);
        generateDots();
    }

    private void generateDots(){
        for(int i = 0; i < menuDots.Length; i++){ 
            menuDots[i] = this.gameObject.transform.parent.Find("menu-dot" + i.ToString()).GetComponent<RectTransform>();
            menuDots[i].gameObject.SetActive(true);
        } 
        dotsVerticalPos = new int[menuOptions.Length];
        for(int i = 0; i < dotsVerticalPos.Length; i++){
            dotsVerticalPos[i] = (int) menuOptions[i].GetComponent<RectTransform>().position.y;
        }
    }

    void OnEnable(){
        if(menuDots[0] !=null)
            for(int i = 0; i < menuDots.Length; i++) 
                menuDots[i].gameObject.SetActive(true);
        currentMenuIndex = 0;
    }

    void OnDisable(){
        for(int i = 0; i < menuDots.Length; i++) menuDots[i].gameObject.SetActive(false);
        stopTextBlinkEffect();
    }

    // Update is called once per frame
    void Update()
    { 
        textBlinkEffect();
        checkForInput();    
        changeDotPosition();
    }

    private void changeDotPosition(){
        int dotXPos = 42;
        int direction;
        for(int i = 0; i < menuDots.Length; i++){
            if(i == 0) direction = -1;
            else direction = 1;
            menuDots[i].localPosition = new Vector2(dotXPos * direction, dotsVerticalPos[currentMenuIndex] + 1); 
        }
    }

    private void textBlinkEffect(){
        TextMeshProUGUI text = menuOptions[currentMenuIndex].GetComponent<TextMeshProUGUI>();
        uiText.textBlinkEffect(text);
        uiText.dotBlinkEffect(menuDots);
        if(menuOptions[currentMenuIndex].transform.childCount > 0){
            text = menuOptions[currentMenuIndex].transform.GetChild(0).GetComponent<TextMeshProUGUI>();
            uiText.textBlinkEffect(text);
        }
    }

    private void stopTextBlinkEffect(){
        TextMeshProUGUI text = menuOptions[currentMenuIndex].GetComponent<TextMeshProUGUI>();
        uiText.stopTextBlinkEffect(text);
        uiText.stopDotBlinkEffect(menuDots);
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
            if(currentMenuIndex == 0){
                if(SceneManager.GetActiveScene().name == "MAIN")
                    this.transform.parent.GetComponent<UIMainMenu>().currentMenu = 1;
                else GlobalData.gamePaused = false;
            }
            if(currentMenuIndex == 4){
                SceneManager.LoadScene(0);
            }
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
