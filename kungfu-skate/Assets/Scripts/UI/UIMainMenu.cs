using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
public class UIMainMenu : MonoBehaviour
{
    private RectTransform[] menuDots = new RectTransform[2];
    private int[] dotsVerticalPos;
    private GameObject[] menuOptions;
    private GameObject[] optionsMenuOptions;
    private GameObject startMenu;
    private int currentMenuIndex = 0;
    public int currentMenu = 0;
    private UIText uiText;

    // Start is called before the first frame update
    void Start()
    {
        GlobalData.gamePaused = false;
        uiText = transform.root.GetComponent<UIText>(); 
        menuOptions = new GameObject[this.transform.Find("menu-options").transform.childCount];
        optionsMenuOptions = new GameObject[this.transform.Find("options").transform.childCount];
        startMenu = transform.Find("press-start").gameObject;
        for(int i = 0; i < menuOptions.Length; i++) 
            menuOptions[i] = this.transform.Find("menu-options").transform.GetChild(i).gameObject;
        for(int i = 0; i < optionsMenuOptions.Length; i++) 
            optionsMenuOptions[i] = this.transform.Find("options").transform.GetChild(i).gameObject;
        this.transform.Find("options").gameObject.SetActive(false);
        generateDots();
    }

    private void generateDots(){
        for(int i = 0; i < menuDots.Length; i++){ 
            menuDots[i] = this.gameObject.transform.Find("menu-dot" + i.ToString()).GetComponent<RectTransform>();
            menuDots[i].gameObject.SetActive(true);
        } 
        dotsVerticalPos = new int[menuOptions.Length];
        for(int i = 0; i < dotsVerticalPos.Length; i++){
            dotsVerticalPos[i] = (int) menuOptions[i].GetComponent<RectTransform>().position.y;
        }
    }

    void OnEnable(){
        currentMenuIndex = 0;
    }

    // Update is called once per frame
   

    void Update()
    {
        getInput();
        if(currentMenu == 0){
            this.transform.Find("menu-dot0").gameObject.SetActive(false);
            this.transform.Find("menu-dot1").gameObject.SetActive(false);
            this.transform.Find("press-start").gameObject.SetActive(true);
            this.transform.Find("options").gameObject.SetActive(false);
            this.transform.Find("menu-options").gameObject.SetActive(false);
        }
        else if(currentMenu == 1)  {
            this.transform.Find("menu-dot0").gameObject.SetActive(true);
            this.transform.Find("menu-dot1").gameObject.SetActive(true);
            this.transform.Find("press-start").gameObject.SetActive(false);
            this.transform.Find("options").gameObject.SetActive(false);
            this.transform.Find("menu-options").gameObject.SetActive(true);
            changeDotPosition();
        }
        else{
            this.transform.Find("press-start").gameObject.SetActive(false);
            this.transform.Find("options").gameObject.SetActive(true);
            this.transform.Find("menu-options").gameObject.SetActive(false);
        }
        if(currentMenu != 2) {
            if(currentMenu == 1)textBlinkEffect(menuOptions);
            else textBlinkEffect(new GameObject[]{startMenu.gameObject});
            checkForInput();
        }   
    }

    private void changeDotPosition(){
        int dotXPos = 24;
        int direction;
        for(int i = 0; i < menuDots.Length; i++){
            if(i == 0) direction = -1;
            else direction = 1;
            menuDots[i].localPosition = new Vector2(dotXPos * direction, dotsVerticalPos[currentMenuIndex] + 1); 
        }
    }
    private void getInput(){
        if(Input.GetKeyUp(KeyCode.Escape)){
            stopTextBlinkEffect();
            if(currentMenu == 1) currentMenuIndex = 2;
            if(currentMenu == 2) currentMenu = 1;
        }
    }

    private void textBlinkEffect(GameObject[] currentMenu){
        TextMeshProUGUI text = currentMenu[currentMenuIndex].GetComponent<TextMeshProUGUI>();
        uiText.textBlinkEffect(text);
        uiText.dotBlinkEffect(menuDots);
        // if(currentMenu[currentMenuIndex].transform.childCount > 0){
        //     text = currentMenu[currentMenuIndex].transform.GetChild(0).GetComponent<TextMeshProUGUI>();
        //     uiText.textBlinkEffect(text);
        // }
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
            if(currentMenu == 0) currentMenu = 1;
            else{
                if(currentMenuIndex == 0) SceneManager.LoadScene("STAGE1");
                if(currentMenuIndex == 1) currentMenu = 2;
                if(currentMenuIndex == 2) Application.Quit();
            }
        }
    }
}
