using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UI : MonoBehaviour
{
    private GameObject debugger;
    private TextMeshPro fpsText;
    private bool isDisplaying = false;
    // Start is called before the first frame update
    void Start()
    {
        debugger = transform.Find("debugger").gameObject;
        fpsText =  debugger.transform.Find("fps").GetComponent<TextMeshPro>();
        Debug.Log(fpsText);
    }

    // Update is called once per frame
    void Update()
    {
        displayDebugger();
        countFrames();   
    }

    private void displayDebugger(){
        if(Input.GetKeyUp(KeyCode.Q)) isDisplaying = !isDisplaying;
        if(isDisplaying) debugger.SetActive(true);
        else debugger.SetActive(false);
    }

    private void countFrames(){
		float fps = (1 / Time.unscaledDeltaTime);
		fpsText.text = "FPS: " + (int)fps;
	}
}
