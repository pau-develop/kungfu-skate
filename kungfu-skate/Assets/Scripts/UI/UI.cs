using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UI : MonoBehaviour
{
    private GameObject debugger;
    private TextMeshPro fpsText;
    private TextMeshPro scrollXText;
    private TextMeshPro timeText;
    public int scrollX;
    private bool isDisplaying = false;
    private float timePassed = 0;
    // Start is called before the first frame update
    void Start()
    {
        debugger = transform.Find("debugger").gameObject;
        fpsText =  debugger.transform.Find("fps").GetComponent<TextMeshPro>();
        scrollXText =  debugger.transform.Find("scrollX").GetComponent<TextMeshPro>();
        timeText =  debugger.transform.Find("time").GetComponent<TextMeshPro>();
    }

    // Update is called once per frame
    void Update()
    {
        displayDebugger();
        countFrames();
        displayScrollX();
        displayTimeOnScene();   
    }

    private void displayDebugger(){
        if(Input.GetKeyUp(KeyCode.Q)) isDisplaying = !isDisplaying;
        if(isDisplaying) debugger.SetActive(true);
        else debugger.SetActive(false);
    }

    private void countFrames(){
		float fps = (1 / Time.unscaledDeltaTime);
		fpsText.text = "FPS:  " + (int)fps;
	}

    private void displayScrollX(){
        scrollXText.text = "xPos: " + scrollX;
    }

    private void displayTimeOnScene(){
        timePassed += Time.deltaTime;
        string secondsPassed = ((int)timePassed%60).ToString();
        if(secondsPassed.Length == 1) secondsPassed = "0" + secondsPassed;
        int minutesPassed = (int)Mathf.Round(timePassed/60);
        timeText.text = "Time: " + minutesPassed + ":" + secondsPassed;
    }
}
