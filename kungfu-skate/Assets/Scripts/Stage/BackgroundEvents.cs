using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundEvents : MonoBehaviour
{
    public bool shouldSlowDown = false;
    public bool inBossFight = false;
    public bool inCutScene = false;
    private UI ui;
    private StageScrolling stage;
    private float groundLayerScrollSpeed;
    private float backgroundLayerScrollSpeed; 
    public int slowDownPos;
    private float scrollX = 0;
    private int scrollXInt = 0;
    private int scrollXLatest = 0;
    public int[] speedEvents;
    private int currentSpeedEventIndex = 0;
    // Start is called before the first frame update
    void Start()
    {
        ui = GameObject.Find("UI").GetComponent<UI>();
        groundLayerScrollSpeed = transform.Find("Layer1").GetComponent<StageScrolling>().backgroundScrollSpeed;
        backgroundLayerScrollSpeed = transform.Find("Layer2").GetComponent<StageScrolling>().backgroundScrollSpeed;

    }

    // Update is called once per frame
    void Update()
    {
        countXScroll();
        checkForSlowDownEvent();
    }

    private void checkForSlowDownEvent(){
        if(scrollXInt == slowDownPos) shouldSlowDown = true;
    }

    private void countXScroll(){
       scrollX += backgroundLayerScrollSpeed * Time.deltaTime;
       scrollXInt = ((int)scrollX/10);
       ui.scrollX = scrollXInt;
       if(scrollXLatest < scrollXInt) manageScrollSpeed();
       scrollXLatest = scrollXInt;
    }

    private void manageScrollSpeed(){
        float currentBackgroundScrollSpeed = transform.Find("Layer2").GetComponent<StageScrolling>().backgroundScrollSpeed;
        if(speedEvents[currentSpeedEventIndex] == scrollXInt){
            if(currentBackgroundScrollSpeed == groundLayerScrollSpeed) currentBackgroundScrollSpeed = backgroundLayerScrollSpeed;
            else currentBackgroundScrollSpeed = groundLayerScrollSpeed;
            if(currentSpeedEventIndex < speedEvents.Length -1 ) currentSpeedEventIndex++;
            transform.Find("Layer2").GetComponent<StageScrolling>().backgroundScrollSpeed = currentBackgroundScrollSpeed;  
        }
    } 
}
