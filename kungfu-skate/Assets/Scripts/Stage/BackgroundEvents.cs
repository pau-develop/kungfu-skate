using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundEvents : MonoBehaviour
{
    public bool shouldSlowDown = false;
    public bool shouldTransition = false;
    public bool inBossFight = false;
    public bool inCutScene = false;
    private UI ui;
    private StageScrolling stage;
    private float groundLayerScrollSpeed;
    private float backgroundLayerScrollSpeed; 
    public int slowDownXPos;
    public int autoMoveXPos;
    public int colorTransitionPos;
    public Vector2 autoMoveDestPos;
    private float scrollX = 0;
    private int scrollXInt = 0;
    private int scrollXLatest = 0;
    public int[] speedEvents;
    private int currentSpeedEventIndex = 0;
    private bool shouldAccelerate =  false;
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
        checkForColorTransition();
        if(!inCutScene && !inBossFight && !shouldSlowDown) checkForSlowDownEvent();
        if(!inCutScene && !inBossFight) checkForAutoMoveEvent();
        if(!inCutScene) countXScroll();
        if(shouldSlowDown){
            if(!inCutScene){
                changePiecesSpeed(false);
                checkForCutSceneEvent();
            } 
        }
        if(shouldAccelerate){
            changePiecesSpeed(true);
            checkForEndOfAcceleration();
        } 
        if(inCutScene) checkForEndOfCutScene();
    }

    private void checkForColorTransition(){
        if(scrollXInt == colorTransitionPos) shouldTransition = true;
    }

    private void checkForAutoMoveEvent(){
         if(scrollXInt == autoMoveXPos) GameObject.FindWithTag("Player").GetComponent<CharacterMovement>().autoMoveCutscene = true;
    }

    private void checkForEndOfAcceleration(){
        StageScrolling[] children = transform.GetComponentsInChildren<StageScrolling>();
        for(int i = 0; i < children.Length; i++){
            if(children[i].currentBackgroundScrollSpeed != children[i].backgroundScrollSpeed) return;
            shouldAccelerate = false;
        }
    }

    private void changePiecesSpeed(bool accelerate){
        StageScrolling[] children = transform.GetComponentsInChildren<StageScrolling>();
        for(int i = 0; i < children.Length; i++){
            children[i].changeBackgroundSpeed(accelerate);
        }
    }

    private void checkForEndOfCutScene(){
        if(Input.GetKeyUp(KeyCode.E)) {
            inBossFight = true;
            shouldSlowDown = false;
            shouldAccelerate = true;
            inCutScene = false;
            GameObject.FindWithTag("Player").GetComponent<CharacterMovement>().autoMoveCutscene = false;
        }
    }

    private void checkForCutSceneEvent(){
        StageScrolling[] children = transform.GetComponentsInChildren<StageScrolling>();
        for(int i = 0; i < children.Length; i++){
            if(children[i].currentBackgroundScrollSpeed != 0) return;
            inCutScene = true;
        }
    }
    private void checkForSlowDownEvent(){
        if(scrollXInt == slowDownXPos) shouldSlowDown = true;
    }

    private void countXScroll(){
       scrollX += backgroundLayerScrollSpeed * Time.deltaTime;
       scrollXInt = ((int)scrollX/10);
       ui.scrollX = scrollXInt;
       if(scrollXLatest < scrollXInt) manageScrollSpeed();
       scrollXLatest = scrollXInt;
    }

    private void manageScrollSpeed(){
        float currentBackgroundScrollSpeed = transform.Find("Layer2").GetComponent<StageScrolling>().currentBackgroundScrollSpeed;
        if(speedEvents[currentSpeedEventIndex] == scrollXInt){
            if(currentBackgroundScrollSpeed == groundLayerScrollSpeed) currentBackgroundScrollSpeed = backgroundLayerScrollSpeed;
            else currentBackgroundScrollSpeed = groundLayerScrollSpeed;
            if(currentSpeedEventIndex < speedEvents.Length -1 ) currentSpeedEventIndex++;
            transform.Find("Layer2").GetComponent<StageScrolling>().currentBackgroundScrollSpeed = currentBackgroundScrollSpeed;  
        }
    } 
}
