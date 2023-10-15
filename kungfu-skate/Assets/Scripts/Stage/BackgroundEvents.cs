using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BackgroundEvents : MonoBehaviour
{
    public AudioClip stageMusic;
    public float timePassed = 0;
    public bool shouldSlowDown = false;
    public bool shouldTransition = false;
    public bool inBossFight = false;
    public bool inCutScene = false;
    private Debugger debugger;
    private StageScrolling stage;
    private float groundLayerScrollSpeed;
    private float[] backgroundLayerScrollSpeed; 
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
    private AudioController audioComp;
    public int endScrollXPos;
    [SerializeField] private GameObject sun;
    private bool spawnedSun = false;
    private int sunSpawnPos = 560;
    private int sunDestroyPos = 1160;
    private int currentScene;
    // Start is called before the first frame update
    void Start()
    {
        audioComp = GameObject.Find("audio").GetComponent<AudioController>();
        debugger = GameObject.Find("debugger").GetComponent<Debugger>();
        groundLayerScrollSpeed = transform.Find("Layer1").GetComponent<StageScrolling>().backgroundScrollSpeed;
        backgroundLayerScrollSpeed = getLayersScrollSpeed();
        playStageMusic();   
        currentScene = SceneManager.GetActiveScene().buildIndex;
    }

    private float[] getLayersScrollSpeed(){
        float[] tempArray = new float[transform.childCount];
        for(int i = 0; i < transform.childCount; i++){
            tempArray[i] = transform.GetChild(i).GetComponent<StageScrolling>().backgroundScrollSpeed;
        }
        return tempArray;
    }

    // Update is called once per frame
    void Update()
    {
        stageEvents(currentScene);
        countStageTime();
        checkForColorTransition();
        checkForStageEnd();
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

    private void checkForStageEnd(){
        if(scrollXInt == endScrollXPos){
           for(int i = 0; i < transform.childCount; i++)
                transform.GetChild(i).GetComponent<StageScrolling>().isEndStage = true;
                if(currentScene == 1) GameObject.Find("SUN(Clone)").GetComponent<SunMovement>().isEndStage = true;
        }
    }
    
    private void stageEvents(int currentScene){
        if(currentScene == 1 && !spawnedSun) spawnSun();
        if(currentScene == 1 && scrollXInt == sunDestroyPos) {
            sunDestroyPos = 0;
            Destroy(GameObject.Find("SUN(Clone)").gameObject);
        }
    }

    private void spawnSun(){
        if(scrollXInt == sunSpawnPos){
            Instantiate(sun, new Vector2(235, 14), Quaternion.identity);
            spawnedSun = true;
        }
    }
    public void playStageMusic(){
        audioComp.playMusic(stageMusic);
    }

    private void countStageTime(){
        timePassed += Time.deltaTime;
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
            if(currentScene == 1){
                SunMovement tempSun = GameObject.Find("SUN(Clone)").GetComponent<SunMovement>();
                tempSun.backgroundScrollSpeed = 2;
                if(tempSun.currentBackgroundScrollSpeed != tempSun.backgroundScrollSpeed) return;
            }
            shouldAccelerate = false;
        }
        
    }

    private void changePiecesSpeed(bool accelerate){
        StageScrolling[] children = transform.GetComponentsInChildren<StageScrolling>();
        for(int i = 0; i < children.Length; i++){
            children[i].changeBackgroundSpeed(accelerate);
            if(currentScene == 1) GameObject.Find("SUN(Clone)").GetComponent<SunMovement>().changeBackgroundSpeed(accelerate);
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
       scrollX += backgroundLayerScrollSpeed[2] * Time.deltaTime;
       scrollXInt = ((int)scrollX/10);
       debugger.scrollX = scrollXInt;
       if(scrollXLatest < scrollXInt) manageScrollSpeed();
       scrollXLatest = scrollXInt;
    }

    private void manageScrollSpeed(){
        if(speedEvents[currentSpeedEventIndex] == scrollXInt){
            for(int i = 0; i < backgroundLayerScrollSpeed.Length; i++){
                string objectString = "Layer"+ i.ToString();
                float currentBackgroundScrollSpeed = transform.Find(objectString).GetComponent<StageScrolling>().currentBackgroundScrollSpeed;
                if(currentBackgroundScrollSpeed == groundLayerScrollSpeed) currentBackgroundScrollSpeed = backgroundLayerScrollSpeed[i];
                    else currentBackgroundScrollSpeed = groundLayerScrollSpeed;
                    
                    transform.Find(objectString).GetComponent<StageScrolling>().currentBackgroundScrollSpeed = currentBackgroundScrollSpeed;  
            }
            if(currentSpeedEventIndex < speedEvents.Length -1 ) currentSpeedEventIndex++;
        }
    } 
}
