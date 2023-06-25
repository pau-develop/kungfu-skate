using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
public class UIMainMenu : MonoBehaviour
{
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
    }

    void OnEnable(){
        currentMenuIndex = 0;
    }

    // Update is called once per frame
   

    void Update()
    {
        getInput();
        if(currentMenu == 0){
            this.transform.Find("press-start").gameObject.SetActive(true);
            this.transform.Find("options").gameObject.SetActive(false);
            this.transform.Find("menu-options").gameObject.SetActive(false);
        }
        else if(currentMenu == 1)  {
            this.transform.Find("press-start").gameObject.SetActive(false);
            this.transform.Find("options").gameObject.SetActive(false);
            this.transform.Find("menu-options").gameObject.SetActive(true);
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
        // if(currentMenu[currentMenuIndex].transform.childCount > 0){
        //     text = currentMenu[currentMenuIndex].transform.GetChild(0).GetComponent<TextMeshProUGUI>();
        //     uiText.textBlinkEffect(text);
        // }
    }

    private void stopTextBlinkEffect(){
        TextMeshProUGUI text = menuOptions[currentMenuIndex].GetComponent<TextMeshProUGUI>();
        uiText.stopTextBlinkEffect(text);
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
