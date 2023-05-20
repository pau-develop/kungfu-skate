using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMovement : MonoBehaviour
{
    public Vector2 ninjaPos;
    public bool autoMove = true;
    public bool autoMoveCutscene = false;
    private Vector2 autoMoveDestPos;
    public bool movingUp = false;
    public bool movingDown =false;
    public bool movingLeft =false;
    public bool movingRight =false;
    public bool isShooting = false;
    public bool isSwinging =  false;
    public Vector2 playerPos;
    private Vector2 playerActualPos;
    public int playerSpeed = 100;

    private int leftLimit = -140;
    private int rightLimit = +140;
    private int topLimit = +50;
    public int botLimit = -90;
    private int latestBotLimit;
    public bool isGrounded = false;
    public bool isAlive = true;
    public bool isExploded = false;
    private GameObject playerLegs;
    private GameObject playerArms;
    private bool isPlayer;
    private bool hasCrashed = false;
    private float crashSpeed = 0;
    private AudioController audioFx;
    private BoxCollider2D charCollider;
    private CharacterData charData;
    private bool firstCrashCheck = true;
    private float detectionDelayAfterCrash = 0.5f;
    private float crashTimer = 0;
    private bool leftCrash;
    private Vector2 spriteSize = new Vector2(40,40);
    public bool onGrindableObject = false;
    public bool rampedUp = false;
    public bool rampedDown = false;
    // Start is called before the first frame update
    void Start()
    {
        charData = GetComponent<CharacterData>();
        audioFx = GameObject.Find("audio").GetComponent<AudioController>();
        latestBotLimit = botLimit;
        autoMoveDestPos = GameObject.Find("Stage").GetComponent<BackgroundEvents>().autoMoveDestPos;
        ninjaPos = transform.position;
        isPlayer = GetComponent<CharacterData>().isPlayer;
        playerPos = new Vector2(transform.position.x, transform.position.y);
        playerLegs = transform.GetChild(1).gameObject;
        playerArms = transform.GetChild(2).gameObject;    
    }

    // Update is called once per frame
    void Update()
    {
        checkForChangesInBotLimit();
        isGrounded = checkGrounded();
        if(isAlive){
            movePlayer();
            oscillate();
        } else {
            if(playerLegs != null) Destroy(playerLegs);
            if(playerArms != null) Destroy(playerArms);
            if(hasCrashed) moveCrashedPlayer();
            else moveDeadPlayer();
        }
    }

    private void moveCrashedPlayer(){
        crashTimer += Time.deltaTime;
        float backgroundScrollSpeed = GameObject.Find("Layer1").GetComponent<StageScrolling>().backgroundScrollSpeed;
        //horizontal movement right
        if(!leftCrash){
            if(crashSpeed > backgroundScrollSpeed) crashSpeed -= 120 * Time.deltaTime;
            else crashSpeed = backgroundScrollSpeed;
            playerPos.x -= crashSpeed * Time.deltaTime;
        } else {
            if(crashSpeed < backgroundScrollSpeed) crashSpeed += 360 * Time.deltaTime;
            else crashSpeed = backgroundScrollSpeed;
            playerPos.x -= crashSpeed * Time.deltaTime;
        }
        //vertical movement
        if(crashTimer > detectionDelayAfterCrash){
            if(!isGrounded && playerPos.y > botLimit) playerPos.y -= playerSpeed/2*Time.deltaTime;
            else playerPos.y = botLimit;
        }
        transform.position = playerPos;
    }

    private void checkForChangesInBotLimit(){
        if(latestBotLimit != botLimit) checkForCrash();
        latestBotLimit = botLimit;
    }

    private void checkForCrash(){
        if(firstCrashCheck){
            if(playerPos.y < botLimit) playerPos.y = botLimit;
            firstCrashCheck = false;
        }
        else{
            if(playerPos.y < botLimit){
                leftCrash = GetComponent<CharacterRaycasting>().leftCrash;
                audioFx.playSound(charData.hitObstacle);
                hasCrashed = true;
                isAlive = false;
                if(!leftCrash) crashSpeed =  GameObject.Find("Layer1").GetComponent<StageScrolling>().backgroundScrollSpeed * 2;
                else crashSpeed =  GameObject.Find("Layer1").GetComponent<StageScrolling>().backgroundScrollSpeed * -2;
            } 
        }
    }

    void destroyPlayer(){
        Destroy(this.gameObject);
    }

    void moveDeadPlayer(){
        float backgroundScrollSpeed = GameObject.Find("Layer1").GetComponent<StageScrolling>().backgroundScrollSpeed;
        if(!isGrounded && playerPos.y > botLimit) playerPos.y -= playerSpeed/2*Time.deltaTime;
        else {
            playerPos.y = botLimit;
            playerPos.x -= backgroundScrollSpeed * Time.deltaTime;
        }
        transform.position = playerPos;
    }  

    void oscillate(){
		float newY;
		if(!isGrounded) newY = Mathf.Sin(Time.time * 7);
		else newY = Mathf.Sin(Time.time * 0);
		playerActualPos = new Vector2(playerPos.x, playerPos.y+newY);
		transform.position = playerActualPos;
    }

    bool checkGrounded(){
        if(playerPos.y == botLimit) return true;
        if(rampedUp || rampedDown) return true;
        return false;
    }

    void movePlayer(){
       if(autoMove) autoMovePlayer();
       if(autoMoveCutscene) autoMoveToCutscenePos();
       else {
        if(rampedUp) autoMoveInRamp(1);
        else if(rampedDown) autoMoveInRamp(-1);
        else{
            if(isPlayer) controlPlayer();
            else controlEnemy();
        }
       }
    }

    void autoMoveInRamp(int direction){
        float backgroundSpeed = GameObject.Find("Layer1").GetComponent<StageScrolling>().backgroundScrollSpeed;
        if(GetComponent<CharacterData>().isPlayer){
            if(direction == -1){
                if(playerPos.y > botLimit) playerPos.y += ((backgroundSpeed/2) * direction) * Time.deltaTime;
                else playerPos.y = botLimit;
            } else playerPos.y += ((backgroundSpeed/2) * direction) * Time.deltaTime;
            transform.position = playerPos;
        }
        else {
            ninjaPos.y += ((backgroundSpeed/2) * direction) * Time.deltaTime;
            transform.position = ninjaPos;
            playerPos = ninjaPos;
        }
    }

    void autoMoveToCutscenePos(){
        Vector2 moveDirection = (autoMoveDestPos - playerPos).normalized;
        playerPos += moveDirection * playerSpeed *  Time.deltaTime;
        if((int)playerPos.x == autoMoveDestPos.x) playerPos.x = autoMoveDestPos.x;
        if((int)playerPos.y == autoMoveDestPos.y) playerPos.y = autoMoveDestPos.y;
    }

    void autoMovePlayer(){
        if(transform.position.x < -120) playerPos.x += (playerSpeed/2) * Time.deltaTime;
        else autoMove = false;
    }

    void controlEnemy(){
        // ninjaPos = GetComponent<NinjaCommands>().ninjaPos;
        playerPos = GetComponent<CharacterMovement>().ninjaPos;
        if(playerPos.y <= botLimit) playerPos.y = botLimit;
    }
    void controlPlayer(){
        if(movingUp) {
            if(playerPos.y <topLimit) playerPos.y+= playerSpeed*Time.deltaTime;
            else playerPos.y = topLimit;
       } 
        if(movingDown) {
            if(playerPos.y>botLimit) playerPos.y-= playerSpeed*Time.deltaTime;
            else playerPos.y = botLimit;
        }
        if(movingLeft) {
            if(playerPos.x > leftLimit) playerPos.x-= playerSpeed*Time.deltaTime;
            else playerPos.x = leftLimit;
        }
        if(movingRight) {
            if(playerPos.x < rightLimit) playerPos.x+= playerSpeed*Time.deltaTime;
            else playerPos.x = rightLimit;
        }
        transform.position = playerPos;
    }
}
