using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
public class UIMainMenu : MonoBehaviour
{
    private GameObject[] menuOptions;
    private int currentMenuIndex = 0;
    private UIText uiText;
    

    // Start is called before the first frame update
    void Start()
    {
        uiText = transform.root.GetComponent<UIText>(); 
        menuOptions = new GameObject[this.transform.Find("menu-options").transform.childCount];
        for(int i = 0; i < menuOptions.Length; i++) 
            menuOptions[i] = this.transform.Find("menu-options").transform.GetChild(i).gameObject;
    }

    void OnEnable(){
        currentMenuIndex = 0;
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
            if(currentMenuIndex == 0) SceneManager.LoadScene("STAGE1");
            if(currentMenuIndex == 2) Application.Quit();
        }
    }
}
