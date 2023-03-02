using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NinjaCommands : MonoBehaviour
{
    private CharacterMovement ninja;
    public Vector2 ninjaPos;
    private int initialMoveSpeed = 100;
    public Vector2 initialDestPos;
    public Vector2[] exitPos;
    private bool shouldLeave;
    private Vector2 initialMoveDir;
    public bool reachedInitialDestPos = false;
    public int moveType = 0;
    public float timeOnScreen = 5;
    private float actualTime = 0;
    public Vector2 exitScreenPosition = new Vector2(140,110);
    private int exitIndex = 0;
    private int direction = -1;
    private int ninjaSpeed = 80;
    private float latestXPos;
    public string exitType;
    // Start is called before the first frame update
    void Start()
    {
        ninja = GetComponent<CharacterMovement>();
        ninjaPos = transform.position;
        initialMoveDir = (initialDestPos - ninjaPos).normalized;
    }

    // Update is called once per frame
    void Update()
    {
        if(ninja.isAlive){
            if(!reachedInitialDestPos) moveToInitialDestPos();
            else {
                if(!shouldLeave) {
                    moveNinja();
                    countTimeOnScreen();
                }
                else moveToExitPos();
            }
            setNinjaDirections();
        }    
    }

    void countTimeOnScreen(){
        actualTime += 1 * Time.deltaTime;
        if(actualTime > timeOnScreen) {
            getExitPosition();
            shouldLeave = true;
        }
    }

    void getExitPosition(){
        switch(exitType){
            case "top":
                exitPos = new Vector2[1];
                exitPos[0] = new Vector2(transform.position.x, 110);
                break;
            case "left":
                exitPos = new Vector2[1];
                exitPos[0] = new Vector2(-181, transform.position.y);
                break;
            case "right":
                exitPos = new Vector2[1];
                exitPos[0] = new Vector2(+181, transform.position.y);
                break;
            case "top-right":
                exitPos = new Vector2[2];
                exitPos[0] = new Vector2(transform.position.x, 45);
                exitPos[1] = new Vector2(+181, 45);
                break;
            case "top-left":
                exitPos = new Vector2[2];
                exitPos[0] = new Vector2(transform.position.x, 45);
                exitPos[1] = new Vector2(-181, 45);
                break;
            case "bot-left":
                exitPos = new Vector2[2];
                exitPos[0] = new Vector2(transform.position.x, -90);
                exitPos[1] = new Vector2(-181, -90);
                break;
            case "bot-right":
                exitPos = new Vector2[2];
                exitPos[0] = new Vector2(transform.position.x, -90);
                exitPos[1] = new Vector2(181, -90);
                break;
        }
    }

    void setNinjaDirections(){
        if(transform.position.x > latestXPos){
            ninja.movingLeft = false;
            ninja.movingRight = true;
        }
        else if(transform.position.x < latestXPos){
            ninja.movingRight = false;
            ninja.movingLeft = true;
        } 
        else{
            ninja.movingLeft = false;
            ninja.movingRight = false;
        }
        latestXPos = transform.position.x;
    }

    void moveToInitialDestPos(){
        float step = initialMoveSpeed * Time.deltaTime;
        ninjaPos = Vector2.MoveTowards(ninjaPos, initialDestPos, step);
        if(ninjaPos == initialDestPos) reachedInitialDestPos = true;
    }

    void moveToExitPos(){
        float step = initialMoveSpeed * Time.deltaTime;
        ninjaPos = Vector2.MoveTowards(ninjaPos, exitPos[exitIndex], step);
        if(ninjaPos == exitPos[exitIndex]) exitIndex++;
    }

    void moveNinja(){
        switch(moveType){
            case 0:
                break;
            case 1:
                straightMovement();
                break;
            case 2:
                oscillationMovement();
                break;
        }
    }

    void oscillationMovement(){
        
    }

    void straightMovement(){
        ninjaPos.x += (ninjaSpeed * direction) * Time.deltaTime;
    }
}
